using System;
using System.Collections.Generic;

public class PresentManager
{
    private TileManager _tileManager;
    public GameManager _gameManager;
    private List<Vector2> _availableSpots = new();
    private ShipPiece _currentPresent;
    private int _tileSize = 64;
    private float _healAmount = 65f;
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
            }
            ;
        }
        SpawnNextPresent();
    }
    public void OnPresentCollected(Player player)
    {
        player.Health += _healAmount;
        if (player.Health > player.MaxHealth)
            player.Health = player.MaxHealth;

        if (_currentPiece != null)
            _gameManager.RemoveObject(_currentPiece);

        _currentPiece = null;
        SpawnNextPresent();
    }

    private void SpawnNextPresent()
    {
        // safeguard
        if (_availableSpots.Count == 0) return;
        Vector2 spawnPos = _availableSpots[Random.Shared.Next(_availableSpots.Count)];
        Rectangle bounds = new Rectangle((int)spawnPos.X, (int)spawnPos.Y, _tileSize, _tileSize);
        _currentPresent = new Present(bounds, this);
        _gameManager.AddObject(_currentPresent);
    }
}

