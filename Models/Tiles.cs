using System;
using System.Runtime.CompilerServices;

public class Tiles
{
    private Texture2D _tileTexture;
    private Rectangle[] _rectangles;
    private int _numVaryingTiles;
    public Tiles(Texture2D texture)
	{
		_tileTexture = texture;
        _numVaryingTiles = 3;
        _rectangles = new Rectangle[_numVaryingTiles];

        _rectangles[0] = new Rectangle(0, 0, 8, 8);
        _rectangles[1] = new Rectangle(152, 24, 8, 8);
        _rectangles[2] = new Rectangle(16, 8, 8, 8);
    }
    public Texture2D Texture => _tileTexture;
    public Rectangle GetSourceRectangle(int tileType)
    {
        if (tileType < 0 || tileType >= _numVaryingTiles)
            return Rectangle.Empty;
        return _rectangles[tileType];
    }
}
