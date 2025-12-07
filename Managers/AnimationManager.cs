using System;
using System.Collections.Generic;

public class AnimationManager
{
    private readonly Dictionary<object, Animation> _anims = new();
    private object _currentFrame = null;

    public void AddAnimation(object frame, Animation animation)
    {
        // null frame not allowed
        if (frame == null) throw new ArgumentNullException(nameof(frame));
        _anims[frame] = animation;

        if (_currentFrame == null)
            _currentFrame = frame;
    }
    // extenstion to support string frames (UI)
    public void AddAnimation(string frame, Animation animation) => AddAnimation((object)frame, animation);
    public void Update(object frame, GameTime gameTime)
    {
        // hopefully this fixes tornado collisin pls
        if (frame == null)
            return;

        if (_anims.TryGetValue(frame, out var anim))
        {
            // different frame than before, stop previous and start new
            if (!Equals(_currentFrame, frame))
            {
                if (_currentFrame != null && _anims.TryGetValue(_currentFrame, out var prev))
                {
                    prev.Stop();
                    prev.Reset();
                }
                _currentFrame = frame;
                anim.Start();
            }
            anim.Update();
        }
        else
        {
            // no animation for this frame, stop previous if any
            if (_currentFrame != null && _anims.TryGetValue(_currentFrame, out var prev))
            {
                prev.Stop();
                prev.Reset();
            }
            _currentFrame = null;
        }
    }
    // extenstion to support string frames (UI)
    public void Update(string frame, GameTime gameTime) => Update((object)frame, gameTime);
    public void Draw(SpriteBatch spriteBatch, Vector2 position, SpriteEffects effects = SpriteEffects.None)
    {
        if (_currentFrame == null)
            return;
        // if animation exists, store it // if not, return
        if (!_anims.TryGetValue(_currentFrame, out var anim))
            return;
        
        anim.Draw(position, effects);
    }
}