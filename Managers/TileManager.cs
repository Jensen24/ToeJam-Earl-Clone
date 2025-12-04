using Microsoft.Xna.Framework.Graphics;
using System;

//public class TileManager
//{
//    private int[] _tiles;
//    private int _numTiles;
//    private Tiles _tileset;
//    private int _tileSize;
//    private int _mapHeight;
//    private int _mapWidth;
//    //this vector is for showcase purposes
//    private Vector2 _position = new(500, 300);
//    public TileManager(Tiles tileset, int tileSize)
//    {
//        _tileset = tileset;
//        _tileSize = tileSize;

//        _mapHeight = 3;
//        _mapWidth = 3;
//        _numTiles = _mapHeight * _mapWidth;
//        _tiles = new int[_numTiles];

//        _tiles[0] = 0;
//        _tiles[1] = 0;
//        _tiles[2] = 0;
//        _tiles[3] = 1;
//        _tiles[4] = 1;
//        _tiles[5] = 1;
//        _tiles[6] = 2;
//        _tiles[7] = 2;
//        _tiles[8] = 2;
//    }
//    public void Draw(SpriteBatch spriteBatch)
//    {
//        // code below is altered code provided from teacher: DrawingFromSpriteandTileMaps
//        for (int y = 0; y < _mapHeight; y++)
//        {
//            for (int x = 0; x < _mapWidth; x++)
//            {
//                int index = y * _mapWidth + x;
//                int tileType = _tiles[index];

//                Rectangle source = _tileset.GetSourceRectangle(tileType);
//                // _position is for showcase purposes
//                Vector2 position = new Vector2(x * _tileSize, y * _tileSize) + _position;

//                Globals.SpriteBatch.Draw(_tileset.Texture, position, source, Color.White);
//            }
//        }
//    }
//}
