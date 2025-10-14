using System;
using System.Net.Mime;
using Microsoft.Xna.Framework;

public class MusicSystem
{
	SongCollection playlist = new SongCollection();
	public MusicSystem()
	{
		Song ABD = Globals.Content.Load("Alien Break Down.mp3");
		Song BEB = Globals.Content.Load("Big Earl Bump.mp3");
		Song FB = Globals.Content.Load("Funkotronic Beat.mp3");
		Song RRR = Globals.Content.Load("Rapmaster Rocket Racket.mp3");
		Song TJ = Globals.Content.Load("ToeJam Jammin'.mp3");
		Song TS = Globals.Content.Load("ToeJam Slowjam.mp3");

		playlist.Add(ABD, BEB, FB, RRR, TJ, TS);
	}

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
}
