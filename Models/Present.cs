﻿using System;
using System.Collections.Generic;

public class Present
{
    private Vector2 _position = new(300, 300);
    public Vector2 presentPosition => _position;
    private float _scale = 1.5f;
    private float _rotation = 0f;
    private float _rotationSpeed = 2f;
    private AnimationManager _anims = new();
    public Present()
    {
        Texture2D present = Globals.Content.Load<Texture2D>("Items");

        var idle = new List<Rectangle>
        {
            new Rectangle(5, 12, 22, 12),
            new Rectangle(106, 12, 12, 12),
            new Rectangle(69, 14, 24, 10),
        };
        _anims.AddAnimation("Idle", new Animation(present, idle, 0.85f, new Vector2(_scale, _scale)));
    }

    public void Update(GameTime gameTime)
    {
        _anims.Update("Idle", gameTime);
        _rotation += _rotationSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        _anims.Draw(spriteBatch, _position, _rotation);
    }
}
