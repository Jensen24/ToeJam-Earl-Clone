using System;
using System.Collections.Generic;

public enum CollisionShape
{
    Rectangle,
    Circle
}
public abstract class GameObject
{
    protected Vector2 _position;
    public Vector2 Position { get => _position; set => _position = value; }
    public int Width { get; protected set; }
    public int Height { get; protected set; }
    public float Radius { get; protected set; }
    public bool IsCollidable = true;
    public bool IsActive = true;
    public CollisionShape ShapeType;
    public GameObject(Vector2 startPos)
    {
        _position = startPos;
    }
    public virtual Rectangle Bounds
    {
        get
        {
            if (ShapeType == CollisionShape.Circle)
            {
                return new Rectangle((int)(_position.X - Radius), (int)(_position.Y - Radius), (int)(Radius * 2), (int)(Radius * 2));
            }
            else
            {
                return new Rectangle((int)_position.X, (int)_position.Y, Width, Height);
            }
        }
    }
    public virtual void Update(GameTime gameTime) { }
    public virtual void Draw(SpriteBatch spriteBatch) { }
    public abstract class Entity : GameObject
    {
        public Vector2 Velocity { get; set; } = Vector2.Zero;
        public Entity(Vector2 startPos) : base(startPos)
        {
            ShapeType = CollisionShape.Circle;
            Radius = 16f;
        }
        public override void Update(GameTime gameTime)
        {
            //_position += Velocity * (float)Globals.TotalSeconds;
        }
    }
    public class NPC : Entity
    {
        protected NPC(Vector2 position) : base(position) { }
    }
    public class Enemy : Entity
    {
        public float Damage { get; protected set; } = 30f;
        protected Enemy(Vector2 position) : base(position) { }
    }
    public class Player : Entity
    {
        SoundEffect Hurt = Globals.Content.Load<SoundEffect>("Yeouch! (ToeJam)");
        public float MaxHealth { get; protected set; } = 100f;
        public float Health { get; protected set; } = 100f;
        public int Lives { get; protected set; } = 3;
        public bool InputLocked { get; set; } = false;
        public TileEffectState CurrentEffect { get; protected set; } = TileEffectState.None;
        public Vector2 FacingDirection { get; protected set; }
        private bool IsInvincible = false;
        private float InvincibilityTimer = 0f;
        private const float InvincibilityDuration = 1.5f;
        public Player(Vector2 position) : base(position) { Health = MaxHealth; }
        public virtual void TakeDamage(float damage)
        {
            if (IsInvincible) return;
            Health -= damage;
            if (Health < 0f)
                LoseLife();
            IsInvincible = true;
            InvincibilityTimer = InvincibilityDuration;
            IsCollidable = false;
            Hurt.Play();
        }
        public void Heal(float amount)
        {
            Health += amount;

            if (Health > MaxHealth)
                Health = MaxHealth;
        }
        public void LoseLife()
        {
            Lives--;
            if (Lives > 0)
            {
                Health = MaxHealth;
                Position = new Vector2(1000, 1000); // respawn position
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("You ran out of lives! Try again next time!");
                Environment.Exit(0);
            }
        }
        public virtual void ApplyTileEffect(TileEffectState effect, Vector2 direction)
        {
            CurrentEffect = effect;
            FacingDirection = direction;
        }

        // primarily for tornado displacement
        public void Relocate(Vector2 newPos)
        {
            Position = newPos;
        }

        public void StartTornadoCapture()
        {
            Velocity = Vector2.Zero;
            InputLocked = true;
            IsActive = false;
        }

        public void EndTornadoCapture()
        {
            // hp drain
            InputLocked = false;
            IsActive = true;
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
