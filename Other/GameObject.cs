public enum CollisionShape
{
    Rectangle,
    Circle
}
public abstract class GameObject
{
	public Rectangle Bounds;
	public bool IsCollidable = true;
	public bool IsActive = true;
    public CollisionShape ShapeType;
	public virtual void Update(GameTime gameTime) { }
	public virtual void Draw(SpriteBatch spriteBatch) { }
    public class Entity : GameObject
    {
        public Vector2 Position;
        public Vector2 Velocity;
        public float Radius = 16f;
        public Entity(Vector2 position)
        {
            Position = position;
            ShapeType = CollisionShape.Circle;
            UpdateBounds();
        }
        public void UpdateBounds()
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
        // Add text for dialogue/interaction
        public bool CanInteract = true;
        public NPC(Vector2 position) : base(position){ }

        public override void Update(GameTime gameTime)
        {
            // add NPC State Machine
            base.Update(gameTime);
        }
    }
    public class Enemy : Entity
    {
        public Enemy(Vector2 position) : base(position) { }
        public override void Update(GameTime gameTime)
        {
            // add Enemy State Machine
            base.Update(gameTime);
        }
    }

    public class Item : GameObject
    {
        public Item(Rectangle rect)
        {
            Bounds = rect;
            ShapeType = CollisionShape.Rectangle;
        }

    }

    public class Tile : GameObject
    {
        public Tile(Rectangle bounds)
        {
            Bounds = bounds;
        }

    }

    public class Player : GameObject
    {
        public Vector2 Position;
        public float Radius = 16f;
        public Vector2 Velocity;
        public Player(Vector2 Position)
        {
            Position = position;
            ShapeType = CollisionShape.Circle;
            UpdateBounds();
        }
        
        public void UpdateBounds()
        {
            Bounds = new Rectangle((int)(Position.X - Radius), (int)(Position.Y - Radius), (int)(Radius * 2), (int)(Radius * 2));
        }
        public override void Update(GameTime gameTime)
        {
            var keyboardState = Keyboard.GetState();
            Velocity = Vector2.Zero;

            if (keyboardState.IsKeyDown(Keys.Up)) Velocity.Y = -2;
            if (keyboardState.IsKeyDown(Keys.Down)) Velocity.Y = 2;
            if (keyboardState.IsKeyDown(Keys.Left)) Velocity.X = -2;
            if (keyboardState.IsKeyDown(Keys.Right)) Velocity.X = 2;

            Position += Velocity;
            UpdateBounds();
        }   
    }
}
