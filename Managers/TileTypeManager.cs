using System;

public enum TileType
{
    EdgeWobble = 0,
    Solid = 1,
    Water = 2,
	LevelTransition = 3,
    None = 4
}

public static class TileTypeManager
{
	public static TileType GetCollisionTypeFromIndex(int index)
	{
		return index switch
		{
			0 => TileType.EdgeWobble,
			1 => TileType.Solid,
			2 => TileType.Water,
			3 => TileType.LevelTransition,
			_ => TileType.None
		};
	}
}
