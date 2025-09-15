using System;
using System.Collections.Generic;

public class Elevator
{
    private Vector2 _position = new(450, 300);
    private AnimationManager _anims = new();
    public Elevator()
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
        _anims.AddAnimation("Buffer", new Animation(elevator, buffer, 0.15f));
    }

    public void Update(GameTime gameTime)
    {
        _anims.Update("Buffer", gameTime);
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        _anims.Draw(spriteBatch, _position);
    }
}
