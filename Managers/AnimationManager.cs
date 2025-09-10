using System;
using System.Collections.Generic;

public class AnimationManager
{
    private Dictionary<object, Animation> _anims = new();
    private object _lastKey;

    public void AddAnimation(object key, Animation animation)
    {
        _anims.Add(key, animation);
        _lastKey ??= key;
    }
    public void Update(object key, GameTime gameTime)
    {
        if (_anims.TryGetValue(key, out Animation anim))
        {
            anim.Start();
            anim.Update();
            _lastKey = key;
        }
        else if (_lastKey != null)
        {
            _anims[_lastKey].Stop();
            _anims[_lastKey].Reset();
        }
    }
    public void Draw(SpriteBatch spriteBatch, Vector2 position)
    {
        if (_lastKey != null)
            _anims[_lastKey].Draw(position);
    }
}