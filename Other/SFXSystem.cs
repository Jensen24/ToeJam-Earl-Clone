using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Media;

public class SFXSystem
{
	public SoundEffect hurt;
	public SFXSystem()
	{
        SoundEffect hurt = Globals.Content.Load<SoundEffect>("Yeouch! (ToeJam)");
    }
}
