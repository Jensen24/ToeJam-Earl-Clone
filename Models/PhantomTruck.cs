using System;
using System.Collections.Generic;
using static GameObject;

public class PhantomTruck : Enemy
{
    private float _scale = 1.5f;
    private AnimationManager _anims = new();
    public PhantomTruck(Vector2 startPos) : base(startPos)
    {
        Texture2D truck = Globals.Content.Load<Texture2D>("PhantomTruck");

        var walkUp = new List<Rectangle>
        {
            new Rectangle(8, 83, 32, 37),
            new Rectangle(54, 79, 37, 41),
            new Rectangle(102, 75, 36, 45),
            new Rectangle(152, 75, 32, 45),
            new Rectangle(199, 77, 34, 43),
            new Rectangle(247, 80, 34, 40),
        };
        var walkDown = new List<Rectangle>
        {
            new Rectangle(313, 79, 32, 41),
            new Rectangle(361, 77, 32, 43),
            new Rectangle(409, 73, 32, 46),
            new Rectangle(457, 72, 32, 48),
            new Rectangle(505, 75, 32, 45),
            new Rectangle(553, 78, 32, 42),
        };
        // SpriteEffects.FlipHorizontally in SpriteBatch Draw
        var walkLeft = new List<Rectangle>
        {
            new Rectangle(8, 10, 51, 46),
            new Rectangle(79, 13, 52, 43),
            new Rectangle(152, 18, 51, 38),
            new Rectangle(224, 14, 51, 42),
            new Rectangle(298, 8, 51, 48),
            new Rectangle(370, 9, 51, 47),
        };
        var walkRight = new List<Rectangle>
        {
            new Rectangle(8, 10, 51, 46),
            new Rectangle(79, 13, 52, 43),
            new Rectangle(152, 18, 51, 38),
            new Rectangle(224, 14, 51, 42),
            new Rectangle(298, 8, 51, 48),
            new Rectangle(370, 9, 51, 47),
        };
        var idle = new List<Rectangle>
        {
            new Rectangle(313, 79, 32, 41),
            new Rectangle(361, 77, 32, 43),
            new Rectangle(409, 73, 32, 46),
            new Rectangle(457, 72, 32, 48),
            new Rectangle(505, 75, 32, 45),
            new Rectangle(553, 78, 32, 42),
        };
        _anims.AddAnimation("Idle", new Animation(truck, idle, 0.30f, new Vector2(_scale, _scale)));
        _anims.AddAnimation("Down", new Animation(truck, walkDown, 0.20f, new Vector2(_scale, _scale)));
        _anims.AddAnimation("Up", new Animation(truck, walkUp, 0.20f, new Vector2(_scale, _scale)));
        _anims.AddAnimation("Left", new Animation(truck, walkLeft, 0.20f, new Vector2(_scale, _scale)));
        _anims.AddAnimation("Right", new Animation(truck, walkRight, 0.20f, new Vector2(_scale, _scale)));
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
