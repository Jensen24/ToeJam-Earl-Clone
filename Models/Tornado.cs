using System;
using System.Collections.Generic;
using static GameObject;

public class Tornado : Enemy
{
    private float _scale = 1.5f;
    private AnimationManager _anims = new();
    public Tornado(Vector2 startPos) : base(startPos)
    {
        Texture2D tornado = Globals.Content.Load<Texture2D>("Tornado");

        var idle = new List<Rectangle>
        {
            new Rectangle(9, 58, 30, 30),
            new Rectangle(57, 58, 30, 30),
            new Rectangle(105, 58, 30, 30),
            new Rectangle(154, 58, 29, 30),
        };
        var ToeJam = new List<Rectangle>
        {
            new Rectangle(9, 103, 30, 33),
            new Rectangle(57, 100, 30, 36),
            new Rectangle(105, 103, 30, 33),
            new Rectangle(152, 106, 33, 30),
        };
        var Earl = new List<Rectangle>
        {
            new Rectangle(9, 150, 30, 34),
            new Rectangle(57, 149, 30, 35),
            new Rectangle(104, 152, 33, 32),
            new Rectangle(154, 153, 29, 31),
        };
        _anims.AddAnimation("Idle", new Animation(tornado, idle, 0.15f, new Vector2(_scale, _scale)));
        _anims.AddAnimation("Capture ToeJam", new Animation(tornado, ToeJam, 0.15f, new Vector2(_scale, _scale)));
        _anims.AddAnimation("Capture Earl", new Animation(tornado, Earl, 0.15f, new Vector2(_scale, _scale)));
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