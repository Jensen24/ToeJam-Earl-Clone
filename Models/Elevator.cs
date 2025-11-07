using System;
using System.Collections.Generic;
using static GameObject;

// This is a reminder to create a new Object specifically for Scene transition stuff including: Elevators
public class Elevator : Item
{
    private float _scale = 1.5f;
    private AnimationManager _anims = new();
    public Elevator(Rectangle bounds) : base(bounds)
    {
        Texture2D elevator = Globals.Content.Load<Texture2D>("Elevator");

        var buffer = new List<Rectangle>
        {
            new Rectangle(4, 134, 32, 54),
            new Rectangle(52, 134, 32, 54),
            new Rectangle(100, 134, 32, 54),
            new Rectangle(148, 134, 32, 54),
            new Rectangle(196, 134, 32, 54),
            new Rectangle(244, 134, 32, 54),
            new Rectangle(292, 134, 32, 54),
            new Rectangle(340, 134, 32, 54),
        };
        _anims.AddAnimation("Buffer", new Animation(elevator, buffer, 0.15f, new Vector2(_scale, _scale)));
    }

    public override void Update(GameTime gameTime)
    {
        _anims.Update("Buffer", gameTime);
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        _anims.Draw(spriteBatch, Position);
    }
}
