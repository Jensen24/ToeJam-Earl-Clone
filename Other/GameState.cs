using System;

public static class GameState
{
	public static bool Paused { get; private set; }
	public static bool PresentsOpen { get; private set; }
    public static bool MapOpen { get; private set; }
    public static bool InMainMenu { get; private set; } = true;
    public static bool QuitRequest { get; private set; }

    public static void StartGame()
    {
        InMainMenu = false;
        Paused = false;
    }
    public static void ExitGame()
    {
        QuitRequest = true;
    }

    public static void TogglePause()
	{
        if (InMainMenu) return;
        Paused =!Paused;
        System.Diagnostics.Debug.WriteLine(Paused ? "Game Paused" : "Game Unpaused");
    }
	public static void TogglePresents()
	{
        if (InMainMenu) return;
        PresentsOpen = !PresentsOpen;
        Paused = PresentsOpen;
        System.Diagnostics.Debug.WriteLine(PresentsOpen ? "Presents Menu Opened" : "Presents Menu Closed");
    }
    public static void ToggleMap()
    {
        if (InMainMenu) return;
        MapOpen = !MapOpen;
        Paused = MapOpen;
        System.Diagnostics.Debug.WriteLine(MapOpen ? "Map Opened" : "Map Closed");
    }
}
