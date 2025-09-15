using System;

public class GameManager
{
	private Present _present;
	private ToeJam _toeJam;
	private madDentist _dentist;
	private Wiseman _wiseman;
	private Elevator _elevator;
	private UI _ui;
	public void Init ()
	{
		_present = new();
		_toeJam = new();
        _dentist = new();
		_wiseman = new Wiseman();
		_elevator = new Elevator();
		_ui = new UI();
    }
	public void Update(GameTime gameTime)
	{
		InputManager.Update();
		_present.Update(gameTime);
		_toeJam.Update(gameTime);
        _dentist.Update(gameTime);
		_wiseman.Update(gameTime);
		_elevator.Update(gameTime);
		_ui.Update(gameTime);
    }
	public void Draw()
	{
		_present.Draw(Globals.SpriteBatch);
		_toeJam.Draw(Globals.SpriteBatch);
        _dentist.Draw(Globals.SpriteBatch);
		_wiseman.Draw(Globals.SpriteBatch);
		_elevator.Draw(Globals.SpriteBatch);
		_ui.Draw(Globals.SpriteBatch);
    }
}
