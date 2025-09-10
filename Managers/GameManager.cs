using System;

public class GameManager
{
	//private Food _food;
	private ToeJam _toeJam;
	public void Init ()
	{
		//_food = new(new(300,300));
		_toeJam = new();
	}
	public void Update(GameTime gameTime)
	{
		InputManager.Update();
		//_food.Update();
		_toeJam.Update(gameTime);
	}
	public void Draw()
	{
		//_food.Draw();
        _toeJam.Draw(Globals.SpriteBatch);
    }
}
