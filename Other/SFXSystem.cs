using System;
using System.Net.Mime;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;

public class SFXSystem
{
	public SoundEffect hurt;
	public SFXSystem()
	{
        SoundEffect hurt = Globals.Content.Load("hurt(ToeJam).wav");
    }
}
