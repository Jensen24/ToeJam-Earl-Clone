using System;
using System.Collections.Generic;

public class ShipPieceManager
{
	private TileManager _tileManager;
	private GameManager _gameManager;
	private List<Vector2> _availableSpots = new();
	private ShipPiece _currentPiece;
	private int _totalPieces = 10;
	private int _collected = 0;
	private int _tileSize = 64;
	public ShipPieceManager(TileManager tileManager, GameManager gameManager)
	{
		_tileManager = tileManager;
		_gameManager = gameManager;

		// need a list of the indices within level1_gl as there is not distinct boxes
		// indices include 0 - 2026 i believe
		foreach (var bung in tileManager.gl1)
		{
			int index = bung.Value;
			if (index >= 0 && index <= 2026)
			{
				_availableSpots.Add(new Vector2(bung.Key.X * _tileSize, bung.Key.Y * _tileSize));
			}
		}
        SpawnNextPiece();
    }
	public void OnPieceCollected()
	{
		_collected++;

		if(_currentPiece != null)
			_currentPiece.IsActive = false;

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
		if (_availableSpots.Count == 0) return;
		Vector2 spawnPos = _availableSpots[Random.Shared.Next(_availableSpots.Count)];
		Rectangle bounds = new Rectangle((int)spawnPos.X, (int)spawnPos.Y, _tileSize, _tileSize);
		_currentPiece = new Item(bounds);

		_gameManager._allObjects.Add(_currentPiece);
	}
}
