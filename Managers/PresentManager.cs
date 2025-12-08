using System;
using System.Collections.Generic;
using static GameObject;

public class PresentManager
{
    private TileManager _tileManager;
    public GameManager _gameManager;
    private List<Vector2> _availableSpots = new();
    private Present _currentPresent;
    private int _tileSize = 64;
    private float _healAmount = 65f;
    public PresentManager(TileManager tileManager, GameManager gameManager)
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
        player.Heal(_healAmount);

        if (_currentPresent != null)
            _gameManager.RemoveObject(_currentPresent);

        _currentPresent = null;
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

