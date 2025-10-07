using System;
using System.Collections.Generic;

public class Wiseman
{
    private Vector2 _position = new(400, 300);
    public Vector2 wisemanPosition => _position;
    private float _scale = 1.5f;
    private AnimationManager _anims = new();
    public Wiseman()
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

public void Update(GameTime gameTime)
{
    _anims.Update("Idle", gameTime);
}

public void Draw(SpriteBatch spriteBatch)
{
    _anims.Draw(spriteBatch, _position);
}
}
