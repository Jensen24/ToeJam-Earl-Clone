using System;

public class AudioManager
{
	public MusicSystem Music {  get; private set; }
	public QuipSystem Quips { get; private set; }
	public SFXSystem SFX { get; private set; }
	public AudioManager()
	{
		Music = new MusicSystem();
		Quips = new QuipSystem();
		SFX = new SFXSystem();
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
