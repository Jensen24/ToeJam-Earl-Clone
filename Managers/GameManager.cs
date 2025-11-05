using System;

public class GameManager
{
	private Present _item;
	private ToeJam _player;
	private madDentist _enemy;
	private Wiseman _npc;
	private Elevator _elevator;
	private UI _ui;
    private InteractionManager _interactionManager;
	private AudioManager _audioManager;
	private CollisionSystem collisionSystem;
    public void Init ()
	{
		_item = new();
		_player = new();
        _enemy = new();
		_npc = new Wiseman();
		_elevator = new Elevator();
		_ui = new UI();
        _interactionManager = new InteractionManager(_player, _enemy, _item);
		allObjects.Add(_player);
		allObjects.Add(_enemy);
		allObjects.Add(_npc);
		allObjects.Add(_item);
        collisionSystem = new CollisionSystem(new Rectangle(0, 0, 1024, 768));
        _audioManager = new AudioManager();
		_audioManager.OnFirstOpen();
    }
	public void Update(GameTime gameTime)
	{
		InputManager.Update();
		_item.Update(gameTime);
		_player.Update(gameTime);
        _enemy.Update(gameTime);
		_npc.Update(gameTime);
		_elevator.Update(gameTime);
		_ui.Update(gameTime);
        _interactionManager.Update();
    }
	public void Draw()
	{
		_item.Draw(Globals.SpriteBatch);
		_player.Draw(Globals.SpriteBatch);
        _enemy.Draw(Globals.SpriteBatch);
		_npc.Draw(Globals.SpriteBatch);
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
