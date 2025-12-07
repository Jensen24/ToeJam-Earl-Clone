using System;
using System.Collections.Generic;

public class ShipPieceManager
{
	private TileManager _tileManager;
	public GameManager _gameManager;
	private List<Vector2> _availableSpots = new();
	private ShipPiece _currentPiece;
	private int _totalPieces = 10;
	private int _collected = 0;
	public int Collected => _collected;
    private int _tileSize = 64;
	public ShipPieceManager(TileManager tileManager, GameManager gameManager)
	{
		_tileManager = tileManager;
		_gameManager = gameManager;

        // need a list of the indices within level1_gl as there is not distinct boxes
        // indices include 0 - 2026 i believe
        foreach (var bung in tileManager.groundLayer)
		{
			int index = bung.Value;
			if (index >= 0 && index <= 2026)
			{
				_availableSpots.Add(new Vector2(bung.Key.X * _tileSize, bung.Key.Y * _tileSize));
			};
		}
        SpawnNextPiece();
    }
	public void OnPieceCollected()
	{
        System.Diagnostics.Debug.WriteLine("Piece Collected!");
        _collected++;

		if(_currentPiece != null)
			_gameManager.RemoveObject(_currentPiece);

		_currentPiece = null;
		if (_collected >= _totalPieces)
		{
            System.Diagnostics.Debug.WriteLine("All Pieces Collected! Congrats Nerd!");
            // Make victory screen or something
        }
        else
		{
			SpawnNextPiece();
		}
	}

	private void SpawnNextPiece()
	{
		// safeguard
		if (_availableSpots.Count == 0) return;
		Vector2 spawnPos = _availableSpots[Random.Shared.Next(_availableSpots.Count)];
		Rectangle bounds = new Rectangle((int)spawnPos.X, (int)spawnPos.Y, _tileSize, _tileSize);
		_currentPiece = new ShipPiece(bounds, this);

		_gameManager.AddObject(_currentPiece);
	}
}
