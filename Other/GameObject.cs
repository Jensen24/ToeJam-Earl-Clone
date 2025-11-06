public enum CollisionShape
{
    Rectangle,
    Circle
}
public abstract class GameObject
{
    public Vector2 Position { get; protected set; }
    public Rectangle Bounds;
	public bool IsCollidable = true;
	public bool IsActive = true;
    public CollisionShape ShapeType;
    public GameObject(Vector2 startPos)
    {
        Position = startPos;
    }
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
    public abstract class Entity : GameObject
    {
        public Vector2 Velocity;
        public float Radius = 16f;
        public Entity(Vector2 startPos) : base(startPos)
        {
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
        public Item(Rectangle bounds) : base(new Vector2(bounds.X, bounds.Y))
        {
            Bounds = bounds;
            ShapeType = CollisionShape.Rectangle;
        }
    }

    public class Tile : GameObject
    {
        public Tile(Rectangle bounds, bool isCollidable = true) : base(new Vector2(bounds.X, bounds.Y))
        {
            Bounds = bounds;
            IsCollidable = isCollidable;
            ShapeType = CollisionShape.Rectangle;
        }
    }
}
