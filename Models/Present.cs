using System;
using System.Collections.Generic;
using static GameObject;

public class Present : Item
{
    private SoundEffectInstance YoinkInstance;
    private float _scale = 1.5f;
    private PresentManager _pManager;
    private AnimationManager _anims = new();
    public Present(Rectangle bounds, PresentManager pManager) : base(bounds)
    {
        _pManager = pManager;
        SoundEffect Yoink = Globals.Content.Load<SoundEffect>("PickUp");
        YoinkInstance = Yoink.CreateInstance();
        YoinkInstance.Volume = 1f;
        YoinkInstance.Pitch = -0.5f;

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
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        if (!IsActive) return;
        _anims.Draw(spriteBatch, Position);
    }
    public virtual void OnCollection(Player p)
    {
        YoinkInstance.Play();
        IsActive = false;
        _pManager.OnPresentCollected(p);
    }
}
