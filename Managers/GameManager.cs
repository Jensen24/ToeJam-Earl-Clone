using System;
using System.Collections.Generic;
using static GameObject;

public class GameManager
{
	private Present _item;
    private Player _player;
    public Player Player => _player;
    private madDentist _enemy;
	private Tornado _enemy1;
	private Wiseman _npc;
    private ShipPiece _shipPiece;
	private Elevator _elevator;
	private UI _ui;
	private AudioManager _audioManager;
    private ShipPieceManager _shipPieceManager;
    private CollisionSystem _collisionSystem;
    private TileManager _tileManager;
    private List<GameObject> _addObjects = new();
    private List<GameObject> _removeObjects = new();
    public List<GameObject> _allObjects = new List<GameObject>();
    public void Init (TileManager tileManager, UI ui)
	{
        _tileManager = tileManager;
        _audioManager = new AudioManager();
		_ui = ui;
        _collisionSystem = new CollisionSystem(new Rectangle(0, 0, 1024, 768));
		_audioManager.OnFirstOpen();
    }

    public void StartGame(string character)
    {
        Vector2 spawnPos = new Vector2(1000, 1000);
        // defense
        _allObjects.Clear();

        if (character == "ToeJam")
        {
            _player = new ToeJam(spawnPos);
        }
        else
        {
            _player = new Earl(spawnPos);
        }

        _allObjects.Add(_player);
        _ui.SetPlayer(_player);
        _item = new Present(new Rectangle(300, 300, 0, 0));
        _enemy = new madDentist(new Vector2(350, 300));
        _enemy1 = new Tornado(new Vector2(500, 300), _player);
        _npc = new Wiseman(new Vector2(400, 300));
        _elevator = new Elevator(new Rectangle(450, 300, 0, 0));
        _shipPieceManager = new ShipPieceManager(_tileManager, this);

        _allObjects.Add(_shipPiece);
        _allObjects.Add(_enemy);
        _allObjects.Add(_enemy1);
        _allObjects.Add(_npc);
        _allObjects.Add(_item);
        _allObjects.Add(_elevator);
    }
	public void Update(GameTime gameTime)
	{
		InputManager.Update();
        if (!GameState.Paused)
		{
			foreach (var obj in _allObjects)
			{
                // establish defense check to stop crash on CharMenu
				if (obj == null || !obj.IsActive) 
                    continue;
				obj.Update(gameTime);
			}
            _collisionSystem.Update(_allObjects, _tileManager);
            _elevator.Update(gameTime);
        }
        if (_addObjects.Count > 0)
        {
            foreach (var obj in _addObjects)
                _allObjects.Add(obj);

            _addObjects.Clear();
        }
        if (_removeObjects.Count > 0)
        {
            foreach (var obj in _removeObjects)
                _allObjects.Remove(obj);
            _removeObjects.Clear();
        }
        _ui.SetShipPartsCollected(_shipPieceManager.Collected);
        _ui.Update(gameTime);
    }
	public void Draw()
	{
        _item.Draw(Globals.SpriteBatch);
		_player.Draw(Globals.SpriteBatch);
        _enemy.Draw(Globals.SpriteBatch);
		_enemy1.Draw(Globals.SpriteBatch);
        _npc.Draw(Globals.SpriteBatch);
		_elevator.Draw(Globals.SpriteBatch);

        foreach (var obj in _allObjects)
        {
            if (obj == null || !obj.IsActive) 
                continue;
            obj.Draw(Globals.SpriteBatch);
        }
    }
    // necessary for safely adding/removing ship pieces
    public void AddObject(GameObject obj)
    {
        _addObjects.Add(obj);
    }
    public void RemoveObject(GameObject obj)
    {
        _removeObjects.Add(obj);
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
