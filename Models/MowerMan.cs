using System;
using System.Collections.Generic;
using static GameObject;

public class MowerMan : Enemy
{
    private float _scale = 1.5f;
    private AnimationManager _anims = new();
    public MowerMan(Vector2 startPos) : base(startPos)
    {
        Texture2D mowMan = Globals.Content.Load<Texture2D>("MowerMan");

        var walkUp = new List<Rectangle>
        {
            new Rectangle(19, 11, 26, 37),
            new Rectangle(68, 7, 25, 41),
            new Rectangle(115, 6, 27, 42),
            new Rectangle(163, 12, 26, 36),
            new Rectangle(197, 142, 22, 34),
            new Rectangle(211, 9, 26, 39),
            new Rectangle(259, 12, 26, 36),
        };
        var walkDown = new List<Rectangle>
        {
            new Rectangle(20, 127, 25, 49),
            new Rectangle(68, 129, 25, 47),
            new Rectangle(115, 124, 26, 52),
            new Rectangle(163, 128, 27, 48),
            new Rectangle(211, 130, 27, 46),
            new Rectangle(259, 135, 27, 41),
            new Rectangle(259, 12, 26, 36),
        };
        // SpriteEffects.FlipHorizontally in SpriteBatch Draw
        var walkLeft = new List<Rectangle>
        {
            new Rectangle(11, 73, 58, 39),
            new Rectangle(74, 71, 60, 41),
            new Rectangle(138, 70, 61, 42),
            new Rectangle(202, 72, 60, 40),
            new Rectangle(268, 70, 56, 42),
            new Rectangle(330, 69, 60, 43),
        };
        var idle = new List<Rectangle>
        {
            new Rectangle(20, 127, 25, 49),
            new Rectangle(68, 129, 25, 47),
            new Rectangle(115, 124, 26, 52),
            new Rectangle(163, 128, 27, 48),
            new Rectangle(211, 130, 27, 46),
            new Rectangle(259, 135, 27, 41),
            new Rectangle(259, 12, 26, 36),
        };
        _anims.AddAnimation("Idle", new Animation(mowMan, idle, 0.30f, new Vector2(_scale, _scale)));
        _anims.AddAnimation("Down", new Animation(mowMan, walkDown, 0.20f, new Vector2(_scale, _scale)));
        _anims.AddAnimation("Up", new Animation(mowMan, walkUp, 0.20f, new Vector2(_scale, _scale)));
        _anims.AddAnimation("Left", new Animation(mowMan, walkLeft, 0.20f, new Vector2(_scale, _scale)));
    }

    public override void Update(GameTime gameTime)
    {
        _anims.Update("Idle", gameTime);
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        _anims.Draw(spriteBatch, Position);
    }
}
