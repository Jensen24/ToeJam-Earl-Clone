using System;

public class GameManager
{
	private Present _present;
	private ToeJam _toeJam;
	public void Init ()
	{
		_present = new();
		_toeJam = new();
	}
	public void Update(GameTime gameTime)
	{
		InputManager.Update();
		_present.Update();
		_toeJam.Update(gameTime);
	}
	public void Draw()
	{
		_present.Draw(Globals.SpriteBatch);
		_toeJam.Draw(Globals.SpriteBatch);
	}
}
