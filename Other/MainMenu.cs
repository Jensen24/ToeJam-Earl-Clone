using System;
using static System.Net.Mime.MediaTypeNames;

public class MainMenu
{
	private int _selectedIndex = 0;
	private KeyboardState _prevKeyboard;
	private SpriteFont _font;
    private Texture2D _mainMenu;
    private Rectangle _finger;
    private Rectangle _bg;
    private string _playtxt = "Play";
    private string _quittxt = "Quit";
    private Vector2 _playPos = new Vector2(400, 300);
    private Vector2 _quitPos = new Vector2(400, 360);

    public MainMenu()
	{
        _font = Globals.Content.Load<SpriteFont>("TJEFont");
        _mainMenu = Globals.Content.Load<Texture2D>("MainMenu");
        _finger = new Rectangle(336, 24, 32, 15);
        _bg = new Rectangle(8, 8, 320, 224);
    }

	public string? Update()
	{
        KeyboardState keyboard = Keyboard.GetState();

        if (keyboard.IsKeyDown(Keys.Down) && _prevKeyboard.IsKeyUp(Keys.Down))
        {
            _selectedIndex++;
            if (_selectedIndex > 1)
                _selectedIndex = 0;
        }
        if (keyboard.IsKeyDown(Keys.Up) && _prevKeyboard.IsKeyUp(Keys.Up))
        {
            _selectedIndex--;
            if (_selectedIndex < 0)
                _selectedIndex = 1;
        }
        if (keyboard.IsKeyDown(Keys.Enter) && _prevKeyboard.IsKeyUp(Keys.Enter))
        {
            if (_selectedIndex == 0)
                return "Play";
            else
                return "Quit";
        }
        _prevKeyboard = keyboard;
        return null;
    }


	public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(_mainMenu, destinationRectangle: new Rectangle(0, 0, 1024, 768), sourceRectangle: _bg, color: Color.White);
        spriteBatch.DrawString(_font, _playtxt, _playPos, Color.White);
        spriteBatch.DrawString(_font, _quittxt, _quitPos, Color.White);
        Vector2 fingerPos = ((_selectedIndex == 0) ? _playPos + new Vector2(-60, -10) : _quitPos + new Vector2(-60, -10));
        spriteBatch.Draw(_mainMenu, position: fingerPos, sourceRectangle: _finger, color: Color.White);

    }
}
