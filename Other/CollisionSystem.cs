using System;
using System.Collections.Generic;
using static GameObject;

public class CollisionSystem
{
	private Quadtree _quadtree;
	private Rectangle _worldBounds;
	public CollisionSystem(Rectangle worldBounds)
	{
		_worldBounds = worldBounds;
		_quadtree = new Quadtree(0, _worldBounds);
	}

	public void Update(List<GameObject> allObjects, TileManager tileManager)
	{
		_quadtree.Clear();

		foreach (var obj in allObjects)
		{
            // Active objects are registered
            if (obj != null && obj.IsActive)
				_quadtree.insert(obj);
		}
		// Active tiles are registered
		foreach (var tile in tileManager.AllCollisionTiles)
				_quadtree.insert(tile);

        foreach (var obj in allObjects)
		{
			if (obj == null || !obj.IsActive || !obj.IsCollidable) continue;
            bool touchedEffectTile = false;
            if (obj is Player player)
            {
                List<GameObject> nearbyObjects = new List<GameObject>();
                _quadtree.Retrieve(nearbyObjects, player);

                foreach (var other in nearbyObjects)
                {
                    if (other == null) continue;
                    if (player == null) continue;
                    if (!other.IsCollidable || !other.IsActive) continue;

                    if (CheckCollision(player, other))
                    {
                        if (other is Tile tile)
                        {
                            if (HandleTileCollision(player, tile))
                                touchedEffectTile = true;
                        }
                        else
                        {
                            HandleCollision(player, other);
                        }
                    }
                }
                if (!touchedEffectTile)
                {
                    player.ApplyTileEffect(TileEffectState.None, player.FacingDirection);
                }
                else
                {
                    List<GameObject> possibleCollisions = new List<GameObject>();
                    _quadtree.Retrieve(possibleCollisions, obj);

                    foreach (var other in possibleCollisions)
                    {
                        if (other == null) continue;
                        if (other == obj) continue;
                        if (!other.IsActive || !other.IsCollidable) continue;
                        if (CheckCollision(obj, other))
                        {
                            HandleCollision(obj, other);
                        }
                    }
                }
            }
		}
	}

	// Collision Checks Happen Here
	private bool CheckCollision(GameObject a, GameObject b)
	{
		if (a.ShapeType == CollisionShape.Circle && b.ShapeType == CollisionShape.Circle)
			return CircleVsCircle(a, b);

		if (a.ShapeType == CollisionShape.Rectangle && b.ShapeType == CollisionShape.Rectangle)
			return a.Bounds.Intersects(b.Bounds);

		return CircleVsRect(a, b);
    }

	private bool CircleVsCircle(GameObject a, GameObject b)
	{
        // Object Math here: Object A Center // Object B Center // Radius A // Radius B
        var ac = new Vector2(a.Bounds.Center.X, a.Bounds.Center.Y);
        var bc = new Vector2(b.Bounds.Center.X, b.Bounds.Center.Y);
		float ra = a.Bounds.Width * 0.5f;
		float rb = b.Bounds.Width * 0.5f;
        return Vector2.DistanceSquared(ac, bc) < (ra + rb) * (ra + rb);
    }
	private bool CircleVsRect(GameObject a, GameObject b)
	{
        // Object Math here: Circle // Rectangle // Distance X // Distance Y //
        GameObject c = a.ShapeType == CollisionShape.Circle ? a : b;
		GameObject r = a.ShapeType == CollisionShape.Rectangle ? a : b;
		
		Vector2 center = new Vector2(c.Bounds.Center.X, c.Bounds.Center.Y);
		float radius = c.Bounds.Width * 0.5f;

        float closestX = MathHelper.Clamp(center.X, r.Bounds.Left, r.Bounds.Right);
        float closestY = MathHelper.Clamp(center.Y, r.Bounds.Top, r.Bounds.Bottom);

        float dx = center.X - closestX;
        float dy = center.Y - closestY;

        return (dx * dx + dy * dy) < (radius * radius);
    }
	private bool HandleTileCollision(Player player, Tile tile)
	{
        if (tile.Type == TileType.Solid)
        {
            Rectangle playerBounds = player.Bounds;
            Rectangle tileBounds = tile.Bounds;
            Vector2 halt = Vector2.Zero;

            float overlapX = Math.Min(playerBounds.Right - tileBounds.Left, tileBounds.Right - playerBounds.Left);
            float overlapY = Math.Min(playerBounds.Bottom - tileBounds.Top, tileBounds.Bottom - playerBounds.Top);
            if (overlapX < overlapY)
                halt.X = player.Position.X < tile.Position.X ? -overlapX : overlapX;
            else 
                halt.Y = player.Position.Y < tile.Position.Y ? -overlapY : overlapY;
            player.Position += halt;
            player.Velocity = Vector2.Zero;
            return true;
        }
        if (tile.Type == TileType.EdgeWobble)
        {
            player.ApplyTileEffect(TileEffectState.EdgeWobble, player.FacingDirection);
            return true;
        }
        if (tile.Type == TileType.Water)
        {
            player.ApplyTileEffect(TileEffectState.WaterSwim, player.FacingDirection);
            return true;
        }
        if (tile.Type == TileType.LevelTransition)
        {
            // if you step out of bounds, the game force closes lol
            System.Diagnostics.Debug.WriteLine("You Fell out of Bounds! Try Again Next Playthrough!");
            Environment.Exit(0);
        }
        //if (tile.Type == TileType.RoadBoost)
        //{
        //    player.ApplyTileEffect(TileEffectState.RoadBoost, player.FacingDirection);
        //    return true;
        //}
        return false;
    }
    private void HandleCollision(GameObject a, GameObject b)
	{
		if (a is Player && b is Tile)
		{
            HandleTileCollision((Player)a, (Tile)b);
            return;
        }
        if (a is Player && b is Item)
		{
            System.Diagnostics.Debug.WriteLine($"{a.GetType().Name} collided with {b.GetType().Name}");
			if (b is Present present)
			{
                System.Diagnostics.Debug.WriteLine($"{a.GetType().Name} collected a {b.GetType().Name}!");
                present.OnCollection((Player)a);
            }
            else if (b is ShipPiece shipPiece)
            {
                System.Diagnostics.Debug.WriteLine($"{a.GetType().Name} collected a {b.GetType().Name}!");
                shipPiece.OnCollected((Player)a);
            }
            // add to inventory 
            return;
        }
        if (a is Player && b is Enemy)
		{
            Player player = (Player)a;
            Enemy enemy = (Enemy)b;

            System.Diagnostics.Debug.WriteLine($"{player.GetType().Name} collided with {enemy.GetType().Name}");
            player.TakeDamage(50f);

            if (enemy is Tornado tornado)
            {
                System.Diagnostics.Debug.WriteLine($"{player.GetType().Name} is being captured by Tornado!");
                tornado.StartCapture(player);
            }
            else if (enemy is madDentist dentist)
            {
                dentist.StartCooldown();
            }
        }
        if (b is Player && a is Enemy)
        {
            System.Diagnostics.Debug.WriteLine($"{a.GetType().Name} collided with {b.GetType().Name}");
            // consider adding two distinct enemies, one for charged v. one for grabs
            // reduce health // play sfx // displace (if any)
            return;
        }
        if (a is Player && b is NPC)
		{
			// some enemies you can interact with via button press. This would open dialogue system // shop
			// trigger dialogue system // play sfx (if any)
			return;

        }
        // Expand on this: enemy v tile, npc v tile
    }
}
