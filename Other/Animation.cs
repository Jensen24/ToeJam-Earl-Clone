using System;
using System.Collections.Generic;

public class Animation
{
    private readonly Texture2D _texture;
    private readonly List<Rectangle> _sourceRectangles = new();
    private readonly int _frames;
    private int _frame;
    private readonly float _frameTime;
    private float _frameTimeLeft;
    private Vector2 _scale = Vector2.One;
    private bool _active = true;

	public Animation(Texture2D texture, List<Rectangle> sourceRectangles, float frameTime, Vector2? scale = null)
    {
        _texture = texture;
        _sourceRectangles = sourceRectangles;
        _frames = sourceRectangles.Count;
        _frameTime = frameTime;
        _scale = scale ?? Vector2.One;
    }

    public void Stop()
    {
        _active = false;
    }
    public void Start() 
    {
        _active |= true;
    }
    public void Reset()
    {
        _frame = 0;
        _frameTimeLeft = _frameTime;
    }
    public void Update() 
    {
        if (!_active) return;
        _frameTimeLeft -= Globals.TotalSeconds;

        if (_frameTimeLeft <= 0)
        {
            _frameTimeLeft += _frameTime;
            _frame = (_frame + 1) % _frames;
        }
    }
    public void Draw(Vector2 pos, float rotation = 0f)
    {
        Rectangle src = _sourceRectangles[_frame];
        Vector2 origin = new(src.Width / 2f, src.Height / 2f);
        Globals.SpriteBatch.Draw(_texture, pos, _sourceRectangles[_frame], Color.White, rotation, origin, _scale, SpriteEffects.None, 1);
    }
}
