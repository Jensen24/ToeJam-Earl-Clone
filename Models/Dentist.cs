using System;
using System.Collections.Generic;

public class madDentist
{
    private Vector2 _position = new(350, 300);
    private AnimationManager _anims = new();
    public madDentist()
    {
        Texture2D dentist = Globals.Content.Load<Texture2D>("Dentist");

        var walkDown = new List<Rectangle>
        {
            new Rectangle(5, 142, 22, 34),
            new Rectangle(53, 144, 22, 32),
            new Rectangle(97, 141, 31, 35),
            new Rectangle(147, 141, 27, 35),
            new Rectangle(197, 142, 22, 34),
            new Rectangle(247, 143, 19, 33),
            new Rectangle(291, 141, 25, 35),
            new Rectangle(340, 141, 25, 35),
        };
        _anims.AddAnimation("Down", new Animation(dentist, walkDown, 0.15f));
    }

    public void Update(GameTime gameTime)
    {
        _anims.Update("Down", gameTime);
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        _anims.Draw(spriteBatch, _position);
    }
}
