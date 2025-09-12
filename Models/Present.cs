using System;

public class Present
{
    private Vector2 _position = new(200, 200);
    private AnimationManager _anims = new();
    public ToeJam()
    {
        Texture2D present = Globals.Content.Load<Texture2D>("Items");

        var idle = new List<Rectangle>
        {
            new Rectangle(137, 10, 15, 14),
        };
        _anims.AddAnimation("Idle", new Animation(present, idle, 0.15f));

    public void Update()
	{
		_anim.Update();
	}

	public void Draw(SpriteBatch spriteBatch)
	{
		_anim.Draw(spriteBatch, _position);
	}
}
