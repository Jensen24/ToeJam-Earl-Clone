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
	public void PlayFreeRoamMusic()
	{
		Music.ShuffleSongs();
		Music.Play();
	}
}
