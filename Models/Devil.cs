using System;
using System.Collections.Generic;
using static GameObject;

public class Devil : Enemy
{
    private float _scale = 1.5f;
    private AnimationManager _anims = new();
    public Devil(Vector2 startPos) : base(startPos)
    {
        Texture2D devil = Globals.Content.Load<Texture2D>("Devil");

        var walkDown = new List<Rectangle>
        {
            new Rectangle(12, 8, 22, 22),
            new Rectangle(58, 8, 26, 22),
            new Rectangle(107, 8, 25, 22),
        }; 
        var walkUp = new List<Rectangle>
        {
            new Rectangle(13, 104, 25, 22),
            new Rectangle(59, 104, 28, 22),
            new Rectangle(109, 104, 24, 22),
        };
        // SpriteEffects.FlipHorizontally in SpriteBatch Draw
        var walkLeft = new List<Rectangle>
        {
            new Rectangle(8, 54, 32, 24),
            new Rectangle(57, 52, 31, 26),
            new Rectangle(104, 56, 32, 22),
            new Rectangle(152, 54, 32, 24),
        };
        var walkRight = new List<Rectangle>
        {
            new Rectangle(8, 54, 32, 24),
            new Rectangle(57, 52, 31, 26),
            new Rectangle(104, 56, 32, 22),
            new Rectangle(152, 54, 32, 24),
        };
        var idle = new List<Rectangle>
        {
            new Rectangle(11, 149, 26, 25),
            new Rectangle(58, 147, 28, 21),
            new Rectangle(107, 149, 26, 25),
        };
        _anims.AddAnimation("Idle", new Animation(devil, idle, 0.30f, new Vector2(_scale, _scale)));
        _anims.AddAnimation("Down", new Animation(devil, walkDown, 0.20f, new Vector2(_scale, _scale)));
        _anims.AddAnimation("Up", new Animation(devil, walkUp, 0.20f, new Vector2(_scale, _scale)));
        _anims.AddAnimation("Left", new Animation(devil, walkLeft, 0.20f, new Vector2(_scale, _scale)));
        _anims.AddAnimation("Right", new Animation(devil, walkRight, 0.20f, new Vector2(_scale, _scale)));
    }

    public override void Update(GameTime gameTime)
    {
        // need to implement movement later when ai is done
        _anims.Update("Down", gameTime);
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        _anims.Draw(spriteBatch, Position);
    }
}
