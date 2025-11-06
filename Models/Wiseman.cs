using System;
using System.Collections.Generic;
using static GameObject;

public class Wiseman : NPC
{
    private float _scale = 1.5f;
    private AnimationManager _anims = new();
    public Wiseman(Vector2 startPos) : base(startPos)
    {
        Texture2D wiseman = Globals.Content.Load<Texture2D>("Wiseman");

        var idle = new List<Rectangle>
        {
            new Rectangle(163, 65, 19, 47),
            new Rectangle(212, 65, 17, 47),
            new Rectangle(258, 65, 21, 47),
        };
        _anims.AddAnimation("Idle", new Animation(wiseman, idle, 0.85f, new Vector2(_scale, _scale)));
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
