
using System.Collections.Generic;

public class ToeJam
{
	private Vector2 _position = new(100, 100);
	private float _speed = 200f;
	private AnimationManager _anims = new();
	public ToeJam()
	{
        Texture2D toeJam = Globals.Content.Load<Texture2D>("ToeJam");

		var idle = new List<Rectangle>
		{
			new Rectangle(15, 14, 22, 25),
            new Rectangle(46, 12, 22, 27),
            new Rectangle(77, 14, 23, 25),
        };
		var walkDown = new List<Rectangle> 
		{
            new Rectangle(20, 81, 24, 32),
            new Rectangle(55, 82, 21, 30),
            new Rectangle(86, 82, 27, 31),
            new Rectangle(123, 82, 22, 30),
            new Rectangle(157, 82, 21, 30),
            new Rectangle(186, 81, 27, 32),
        };
        var walkUp = new List<Rectangle>
        {
            new Rectangle(22, 132, 27, 31),
            new Rectangle(62, 133, 21, 30),
            new Rectangle(93, 131, 24, 32),
            new Rectangle(125, 131, 27, 32),
            new Rectangle(160, 132, 22, 31),
            new Rectangle(190, 133, 22, 30),
        };
        var walkLeft = new List<Rectangle>
        {
            new Rectangle(239, 84, 20, 28),
            new Rectangle(271, 83, 25, 29),
            new Rectangle(304, 82, 24, 30),
            new Rectangle(336, 83, 23, 29),
            new Rectangle(372, 85, 20, 27),
            new Rectangle(403, 85, 22, 27),
        };
        var walkRight = new List<Rectangle>
        {
            new Rectangle(241, 133, 22, 27),
            new Rectangle(274, 133, 20, 27),
            new Rectangle(307, 131, 23, 29),
            new Rectangle(338, 130, 24, 30),
            new Rectangle(370, 131, 25, 29),
            new Rectangle(407, 132, 20, 28),
        };
        _anims.AddAnimation("Idle", new Animation(toeJam, idle, 0.30f));
        _anims.AddAnimation("Down", new Animation(toeJam, walkDown, 0.20f));
        _anims.AddAnimation("Up", new Animation(toeJam, walkUp, 0.20f));
        _anims.AddAnimation("Left", new Animation(toeJam, walkLeft, 0.20f));
        _anims.AddAnimation("Right", new Animation(toeJam, walkRight, 0.20f));
    }
    private string GetAnimKeyFromDirection(Vector2 direction)
    {
        if (direction.Y > 0) return "Down";
        if (direction.Y < 0) return "Up";
        if (direction.X < 0) return "Left";
        if (direction.X > 0) return "Right";
        return "Idle";

    }
	public void Update(GameTime gameTime)
	{
		if (InputManager.Moving)
		{
			_position += Vector2.Normalize(InputManager.Direction) * _speed * Globals.TotalSeconds;
		}
        string key = GetAnimKeyFromDirection(InputManager.Direction);
		_anims.Update(key, gameTime);
	}

	public void Draw(SpriteBatch spriteBatch)
	{
		_anims.Draw(spriteBatch, _position);
	}
}
