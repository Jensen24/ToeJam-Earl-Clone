using System;
using System.Collections.Generic;

public class UI
{
    private Vector2 _position = new(512, 720);
    private AnimationManager _anims = new();
    public UI()
    {
        Texture2D ui = Globals.Content.Load<Texture2D>("HUD");

        var buffer = new List<Rectangle>
        {
            new Rectangle(336, 298, 320, 30),

        };
        _anims.AddAnimation("Buffer", new Animation(ui, buffer, 0.15f, new Vector2(3.2f, 3.2f)));
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