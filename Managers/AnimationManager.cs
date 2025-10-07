using System;
using System.Collections.Generic;

public class AnimationManager
{
    private Dictionary<object, Animation> _anims = new();
    private object _lastFrame;

    public void AddAnimation(object frame, Animation animation)
    {
        _anims.Add(frame, animation);
        _lastFrame ??= frame;
    }
    public void Update(object frame, GameTime gameTime)
    {
        if (_anims.TryGetValue(frame, out Animation anim))
        {
            anim.Start();
            anim.Update();
            _lastFrame = frame;
        }
        else if (_lastFrame != null)
        {
            _anims[_lastFrame].Stop();
            _anims[_lastFrame].Reset();
        }
    }
    public void Draw(SpriteBatch spriteBatch, Vector2 position, float rotation = 0f)
    {
        if (_lastFrame != null)
            _anims[_lastFrame].Draw(position, rotation);
    }
}