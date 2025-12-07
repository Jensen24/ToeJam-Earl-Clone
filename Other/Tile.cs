using System;
using static GameObject;

public class Tile : GameObject
{
    public int TileIndex { get; set; }
    public TileType Type { get; set; }
    public Tile(Vector2 pos, int index, TileType type) : base(pos)
    {
        TileIndex = index;
        Type = type;
        IsCollidable = (type == TileType.Solid || type == TileType.EdgeWobble || type == TileType.LevelTransition || type == TileType.Water);
        ShapeType = CollisionShape.Rectangle;
        Width = 64;
        Height = 64;
    }
}
