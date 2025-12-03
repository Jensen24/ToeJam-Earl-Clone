using System;

public class AudioManager
{
	public MusicSystem Music {  get; private set; }
	public AudioManager()
	{
		Music = new MusicSystem();
	}
    // Pause Menu Stuff
    public void OnFirstOpen()
	{
		Music.ShuffleSongs();
		Music.Play();
	}
    public void PauseMenu()
    {
		Music.Pause();
    }
    public void Unpause()
    {
        Music.Resume();
    }
}
