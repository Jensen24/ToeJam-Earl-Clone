using System;
using System.Collections.Generic;

public enum CollisionShape
{
    Rectangle,
    Circle
}
public abstract class GameObject
{
    public Vector2 Position { get; protected set; }
    public int Width { get; protected set; }
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
        SoundEffect Hurt = Globals.Content.Load<SoundEffect>("Yeouch! (ToeJam)");
        private bool _isbound = false;
        private bool MovementLocked = false;
        private bool IsInvincible = false;
        private float InvincibilityTimer = 0f;
        private const float InvincibilityDuration = 1.5f;
        public Player(Vector2 position) : base(position) { }
        public virtual void OnCollision(Enemy e)
        {
            if (IsInvincible) return;
            IsInvincible = true;
            InvincibilityTimer = InvincibilityDuration;
            IsCollidable = false;
            Hurt.Play();
            // Add health reduction // displacement
        }

        public void StartTornadoCapture()
        {
            Velocity = Vector2.Zero;
            _isbound = true;
            // MovementLocked = true;
        }

        public void BeginTornadoOffset(Vector2 offset)
        {
            Position += offset;
        }

        public void EndTornadoCapture()
        {
            // hp drain
            //MovementLocked = false;
            _isbound = false;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (IsInvincible)
            {
                InvincibilityTimer -= Globals.TotalSeconds;
                System.Diagnostics.Debug.WriteLine($"IFrame timer: {InvincibilityTimer:F2}");
                if (InvincibilityTimer <= 0f)
                {
                    IsInvincible = false;
                    IsCollidable = true;
                    System.Diagnostics.Debug.WriteLine($"IFrames ended! Collidable = {IsCollidable}");
                }
            }
        }
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
