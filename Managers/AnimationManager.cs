using System;
using System.Collections.Generic;

public class AnimationManager
{
    private Dictionary<string, Animation> _anims = new();
    private Animation _currentAnimation;

    public void AddAnimation(string key, Animation anim)
    {
        _anims[key] = anim;
    }
    public void Update(string key, GameTime gameTime)
    {
        if (_currentAnimation != _anims[key])
        {
            _currentAnimation = _anims[key];
            _currentAnimation.Reset();
        }
        _currentAnimation.Update();
    }
    public void Draw(Vector2 position)
    {
        _currentAnimation?.Draw(position);
    }
}