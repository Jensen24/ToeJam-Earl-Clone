public enum CollisionShape
{
    Rectangle,
    Circle
}
public abstract class GameObject
{
    public Vector2 Position;
    public Rectangle Bounds;
	public bool IsCollidable = true;
	public bool IsActive = true;
    public CollisionShape ShapeType = CollisionShape.Rectangle;
	public virtual void Update(GameTime gameTime) { }
	public virtual void Draw(SpriteBatch spriteBatch) { }
    public virtual void UpdateBounds()
    {
        if (ShapeType == CollisionShape.Circle)
        {
            int radius = Bounds.Width / 2;
            Bounds = new Rectangle((int)(Position.X - radius), (int)(Position.Y - radius), (radius * 2), (radius * 2));
        }
    }
    public class Entity : GameObject
    {
        public Vector2 Velocity;
        public float Radius = 16f;
        public Entity(Vector2 position)
        {
            Position = position;
            ShapeType = CollisionShape.Circle;
            UpdateBounds();
        }
        public override void UpdateBounds()
        {
            Bounds = new Rectangle((int)(Position.X - Radius), (int)(Position.Y - Radius), (int)(Radius * 2), (int)(Radius * 2));
        }
        public override void Update(GameTime gameTime)
        {
            Position += Velocity;
            UpdateBounds();
        }
    }
    public class NPC : Entity
	{
       protected NPC(Vector2 position) : base(position) { }
    }
    public class Enemy : Entity
    {
       protected Enemy(Vector2 position) : base(position) { }
    }
    public class Player : Entity
    {
        public Player(Vector2 position) : base(position) { }
    }

    public class Item : GameObject
    {
        public Item(Rectangle bounds)
        {
            Bounds = bounds;
            ShapeType = CollisionShape.Rectangle;
        }
    }

    public class Tile : GameObject
    {
        public Tile(Rectangle bounds, bool isCollidable = true)
        {
            Bounds = bounds;
            IsCollidable = isCollidable;
            ShapeType = CollisionShape.Rectangle;
        }
    }
}
