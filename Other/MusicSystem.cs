using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Media;

public class MusicSystem
{
	public List<Song> playlist = new List<Song>();
	private int currentIndex = 0;
	public MusicSystem()
	{
		Song ABD = Globals.Content.Load<Song>("Alien Break Down");
		Song BEB = Globals.Content.Load<Song>("Big Earl Bump");
		Song FB = Globals.Content.Load<Song>("Funkotronic Beat");
		Song RRR = Globals.Content.Load<Song>("Rapmaster Rocket Racket");
		Song TJ = Globals.Content.Load<Song>("ToeJam Jammin'");
		Song TS = Globals.Content.Load<Song>("ToeJam Slowjam");

		playlist.AddRange([ABD, BEB, FB, RRR, TJ, TS]);
	}
	// Shuffle song order
	public void ShuffleSongs()
	{
		Random random = new Random();
		int n = playlist.Count;
		while (n > 1)
		{
			n--;
			int k = random.Next(n + 1);
			Song value = playlist[k];
			playlist[k] = playlist[n];
			playlist[n] = value;
		}
	}
	// Starts playlist
	public void Play()
	{
		if (playlist.Count == 0)
			return;
		currentIndex = 0;
		CurrentSong();
	}
    // Loop feature, also plays @ start
    public void CurrentSong()
    {
		if (currentIndex >= playlist.Count)
			currentIndex = 0;

		MediaPlayer.Stop();
		MediaPlayer.IsRepeating = false;
		MediaPlayer.Play(playlist[currentIndex]);
    }
    // Stops Music
    public void Stop()
	{
		MediaPlayer.Stop();
	}
	// Pauses music (alter in game1 so pause menu = pause music)
	public void Pause()
	{
		if (MediaPlayer.State == MediaState.Playing)
			MediaPlayer.Pause();
	}
	// Continues Music
	public void Resume()
	{
		if (MediaPlayer.State == MediaState.Playing)
			MediaPlayer.Resume();
    }
    // Volume 
    public void SetVolume(float volume)
	{
		MediaPlayer.Volume = MathHelper.Clamp(volume, 0f, 1f);
	}
	// +1 in playlist index, moves to new song
	public void NextSong ()
	{
		currentIndex++;
		if (currentIndex >= playlist.Count)
			currentIndex = 0;

		CurrentSong();
	}
	// Moves to next track
	public void MediaStateChanged()
	{
		if (MediaPlayer.State == MediaState.Stopped)
			NextSong();
	}
}
