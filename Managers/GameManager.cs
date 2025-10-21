using System;

public class GameManager
{
	private Present _present;
	private ToeJam _toeJam;
	private madDentist _dentist;
	private Wiseman _wiseman;
	private Elevator _elevator;
	private UI _ui;
    private InteractionManager _interactionManager;
	private AudioManager _audioManager;
    public void Init ()
	{
		_present = new();
		_toeJam = new();
        _dentist = new();
		_wiseman = new Wiseman();
		_elevator = new Elevator();
		_ui = new UI();
        _interactionManager = new InteractionManager(_toeJam, _dentist, _present);
		_audioManager = new AudioManager();
		_audioManager.OnFirstOpen();
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
        _interactionManager.Update();
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
    public void PauseAudio()
    {
        _audioManager.PauseMenu();
    }

    public void ResumeAudio()
    {
        _audioManager.Unpause();
    }
}
