//using System;

//public class Food
//{
//	private static Texture2D _texture;
//	private Vector2 _position;
//	private Animation _anim;

//	public Food(Vector2 pos)
//	{
//		_texture ??= Globals.Content.Load<Texture2D>("HUD_Display");
//		// (_texture, (frames), (RunTime)) --> I chose 100 miliseconds
//		_anim = new(_texture, 138, 168, 0.1f);
//		_position = pos;
//	}

//	public void Update()
//	{
//		_anim.Update();
//	}

//	public void Draw()
//	{
//		_anim.Draw(_position);
//	}
//}
