public enum CollisionShape
{
    Rectangle,
    Circle
}
public abstract class GameObject
{
    public Vector2 Position { get; protected set; }
    public int Width {  get; protected set; }
    public int Height { get; protected set; }
    public float Radius { get; protected set; }
    public bool IsCollidable = true;
	public bool IsActive = true;
    public CollisionShape ShapeType;
    public GameObject(Vector2 startPos)
    {
        Position = startPos;
    }
    public virtual Rectangle Bounds
    {
        get
        {
            if (ShapeType == CollisionShape.Circle)
            {
                return new Rectangle((int)(Position.X - Radius), (int)(Position.Y - Radius), (int)(Radius * 2), (int)(Radius * 2));
            }
            else
            {
                return new Rectangle((int)Position.X, (int)Position.Y, Width, Height);
            }
        }
    }
    public virtual void Update(GameTime gameTime) { }
	public virtual void Draw(SpriteBatch spriteBatch) { }
    public abstract class Entity : GameObject
    {
        public Vector2 Velocity;
        public Entity(Vector2 startPos) : base(startPos)
        {
            Position = startPos;
            ShapeType = CollisionShape.Circle;
            Radius = 16f;
        }
        public override void Update(GameTime gameTime)
        {
            Position += Velocity * (float)Globals.TotalSeconds;
        }
        // Test to visualize collision bounds, delete when done
        public override void Draw(SpriteBatch spriteBatch)
        {
            Texture2D pixel = new Texture2D(spriteBatch.GraphicsDevice, 1, 1);
            pixel.SetData(new[] { Color.Red });
            Rectangle bounds = Bounds;
            spriteBatch.Draw(pixel, bounds, Color.Red * 0.2f);
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
        public Item(Rectangle bounds, bool isActive = true) : base(new Vector2(bounds.X, bounds.Y))
        {
            IsActive = isActive;
            ShapeType = CollisionShape.Rectangle;
        }
    }

    public class Tile : GameObject
    {
        public Tile(Rectangle bounds, bool isCollidable = true) : base(new Vector2(bounds.X, bounds.Y))
        {
            IsCollidable = isCollidable;
            ShapeType = CollisionShape.Rectangle;
        }
    }
}
