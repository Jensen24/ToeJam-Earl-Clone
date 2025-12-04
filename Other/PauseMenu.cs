using System;

public class PauseMenu
{
    private int _selectedIndex = 0;
    private KeyboardState _prevKeyboard;
    private SpriteFont _font;
    private Texture2D _pauseMenu;
    private Rectangle _finger;
    private Rectangle _bg;
    private string _resumetxt = "Resume";
    private string _quittxt = "Quit";

    public PauseMenu()
    {
        _font = Globals.Content.Load<SpriteFont>("TJEFont");
        _pauseMenu = Globals.Content.Load<Texture2D>("MainMenu");
        _finger = new Rectangle(336, 24, 32, 15);
        _bg = new Rectangle(8, 8, 320, 224);
    }

    public string Update()
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
                return "Resume";
            else
                return "Quit";
        }
        _prevKeyboard = keyboard;
        return "";
    }


    public void Draw(SpriteBatch spriteBatch, Viewport viewport)
    {
        spriteBatch.Draw(_pauseMenu, destinationRectangle: new Rectangle(0, 0, viewport.Width, viewport.Height), sourceRectangle: _bg, color: Color.White);
        Vector2 menuCenter = new Vector2(viewport.Width / 2, viewport.Height / 2);
        Vector2 _resumePos = menuCenter + new Vector2(-70, -20);
        Vector2 _quitPos = menuCenter + new Vector2(-70, 40);
        spriteBatch.DrawString(_font, _resumetxt, _resumePos, Color.White);
        spriteBatch.DrawString(_font, _quittxt, _quitPos, Color.White);
        Vector2 fingerPos = (_selectedIndex == 0) ? _resumePos + new Vector2(-45, 12) : _quitPos + new Vector2(-45, 12);
        spriteBatch.Draw(_pauseMenu, position: fingerPos, sourceRectangle: _finger, color: Color.White);

    }
}
