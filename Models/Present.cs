using System;
using System.Collections.Generic;
using static GameObject;

public class Present : Item
{
    // delete this later
    private SoundEffectInstance HurtInstance;
    private float _scale = 1.5f;
    private float _rotation = 0f;
    private float _rotationSpeed = 2f;
    private AnimationManager _anims = new();
    public Present(Rectangle bounds) : base(bounds)
    {
        // delete this later
        SoundEffect Hurt = Globals.Content.Load<SoundEffect>("Yeouch! (ToeJam)");
        HurtInstance = Hurt.CreateInstance();
        HurtInstance.Volume = 1f;
        HurtInstance.Pitch = 0f;

        Texture2D present = Globals.Content.Load<Texture2D>("Items");
        ShapeType = CollisionShape.Rectangle;
        Width = bounds.Width;
        Height = bounds.Height;
        IsActive = true;

        var idle = new List<Rectangle>
        {
            new Rectangle(5, 12, 22, 12),
            new Rectangle(106, 12, 12, 12),
            new Rectangle(69, 14, 24, 10),
        };
        _anims.AddAnimation("Idle", new Animation(present, idle, 0.85f, new Vector2(_scale, _scale)));
    }

    public override void Update(GameTime gameTime)
    {
        if (!IsActive) return;
        _anims.Update("Idle", gameTime);
        _rotation += _rotationSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        if (!IsActive) return;
        _anims.Draw(spriteBatch, Position, _rotation);
    }
    public virtual void OnCollection(Player p)
    {
        // delete this later
        HurtInstance.Play();
        IsActive = false;
    }
}
