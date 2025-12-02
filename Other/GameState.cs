using System;

public static class GameState
{
	public static bool Paused { get; private set; }
	public static bool PresentsOpen { get; private set; }
    public static bool MapOpen { get; private set; }

    public static void TogglePause()
	{
        Paused =!Paused;
        System.Diagnostics.Debug.WriteLine(Paused ? "Game Paused" : "Game Unpaused");
    }
	public static void TogglePresents()
	{
        PresentsOpen = !PresentsOpen;
        Paused = PresentsOpen;
        System.Diagnostics.Debug.WriteLine(PresentsOpen ? "Presents Menu Opened" : "Presents Menu Closed");
    }
    public static void ToggleMap()
    {
        MapOpen = !MapOpen;
        Paused = MapOpen;
        System.Diagnostics.Debug.WriteLine(MapOpen ? "Map Opened" : "Map Closed");
    }
}
