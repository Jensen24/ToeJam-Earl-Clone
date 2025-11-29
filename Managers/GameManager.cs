using System;
using System.Collections.Generic;

public class GameManager
{
	private Present _item;
	private ToeJam _player;
    private madDentist _enemy;
	private Tornado _enemy1;
	private Wiseman _npc;
	private Elevator _elevator;
	private UI _ui;
    //private InteractionManager _interactionManager;
	private AudioManager _audioManager;
	private CameraSystem _camera;
    //private SFXSystem SFX;
    private CollisionSystem _collisionSystem;
    private List<GameObject> _allObjects = new List<GameObject>();
    public void Init ()
	{
        _audioManager = new AudioManager();
		_camera = new CameraSystem(Vector2.Zero);
        //SFX = new SFXSystem();
        _item = new Present(new Rectangle(300, 300, 0, 0));
		_player = new ToeJam(new Vector2(100, 100));
        _enemy = new madDentist(new Vector2(350, 300));
		_enemy1 = new Tornado(new Vector2(500, 300), _player);
        _npc = new Wiseman(new Vector2(400, 300));
		_elevator = new Elevator(new Rectangle(450,300, 0, 0));
		_ui = new UI();
        //_interactionManager = new InteractionManager(_player, _enemy, _item);

        _allObjects.Add(_player);
		_allObjects.Add(_enemy);
		_allObjects.Add(_enemy1);
        _allObjects.Add(_npc);
		_allObjects.Add(_item);
        _collisionSystem = new CollisionSystem(new Rectangle(0, 0, 1024, 768));
		_audioManager.OnFirstOpen();
    }
	public void Update(GameTime gameTime)
	{
		InputManager.Update();
		_camera.Trail(_player.Bounds, new Vector2(1024, 768));

        if (!GameState.Paused)
		{
			foreach (var obj in _allObjects)
			{
				if (!obj.IsActive) continue;
				obj.Update(gameTime);
			}
            _collisionSystem.Update(_allObjects);
            _elevator.Update(gameTime);
           // _interactionManager.Update();
        }
        _ui.Update(gameTime);
    }
	public void Draw()
	{
		Globals.SpriteBatch.Begin(transformMatrix: _camera.GetViewMatrix(), samplerState: SamplerState.PointClamp);
        _item.Draw(Globals.SpriteBatch);
		_player.Draw(Globals.SpriteBatch);
        _enemy.Draw(Globals.SpriteBatch);
		_enemy1.Draw(Globals.SpriteBatch);
        _npc.Draw(Globals.SpriteBatch);
		_elevator.Draw(Globals.SpriteBatch);
        Globals.SpriteBatch.End();

        Globals.SpriteBatch.Begin();
        _ui.Draw(Globals.SpriteBatch);
        Globals.SpriteBatch.End();
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
