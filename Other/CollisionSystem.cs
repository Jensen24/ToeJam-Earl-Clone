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

	public void Update(List<GameObject> allObjects)
	{
		_quadtree.CleanUp();

		foreach (var obj in allObjects)
		{
			if (obj.IsActive)
				_quadtree.insert(obj);
		}

		foreach (var obj in allObjects)
		{
			if (!obj.IsCollidable) continue;

			List<GameObject> possibleCollisions = new List<GameObject>();
			_quadtree.Retrieve(possibleCollisions, obj);

			foreach (var other in possibleCollisions)
			{
				if (other == obj || other.IsCollidable) continue;

                if (CollisionHelper.CheckCollision(obj, other))
                {
					// player collide w tile
					if (obj is Player && other is Tile)
					{
						((Player)obj).Velocity = Vector2.Zero;
                        // add script here for audio and effects
                    }
                    // player pick up item
                    if (obj is Player && other is Item)
					{
						other.IsActive = false;
                        // add script here for adding item to inventory, audio, and effects
                    }
                    // player collide w enemy
                    if (obj is Player && other is Enemy)
                    {
                        // add script here for player damage, knockback, audio, and maybe displacement(? such a case like the tornado)
                    }
                }
			}
		}
	}
}
