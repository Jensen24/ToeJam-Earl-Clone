
using System;
using static GameObject;

namespace ToeJam_Earl
{
    public class Game1 : Game
    {
        private GameManager _gameManager;
        private TileManager _tileManager;
        private readonly GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private CameraSystem _camera;
        private MainMenu _mainMenu;
        private CharacterMenu _characterMenu;
        private bool _inMainMenu = true;
        private bool _inCharacterMenu = false;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {

            _graphics.PreferredBackBufferWidth = 1024;
            _graphics.PreferredBackBufferHeight = 768;
            _graphics.ApplyChanges();

            Globals.Content = Content;
            _camera = new CameraSystem(Vector2.Zero);
            _gameManager = new();
            _gameManager.Init();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _mainMenu = new MainMenu();
            _characterMenu = new CharacterMenu();
            Globals.SpriteBatch = _spriteBatch;
            Texture2D tileset = Content.Load<Texture2D>("Island");
            Tiles tiles = new Tiles(tileset);
            _tileManager = new TileManager(tiles, 8);

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            Globals.Update(gameTime);
            InputManager.Update();

            // Main Menu
            if (_inMainMenu)
            {
                string result = _mainMenu.Update();

                if (result == "Play")
                {
                    _inMainMenu = false;
                    _inCharacterMenu = true;
                    return;
                }
                else if (result == "Quit")
                {
                    Exit();
                    return;
                }
                return;
            }
            if (_inCharacterMenu)
            {
                string character = _characterMenu.Update();

                if (character == "ToeJam" || character == "Earl")
                {
                    GameManager.StartGame(character);
                    _inCharacterMenu = false;
                    return;
                }
                return;
            }
            // Normal Gameplay
            if (InputManager.PausePressed)
            {
                GameState.TogglePause();

                if (GameState.Paused)
                {
                    _gameManager.PauseAudio();
                }
                else
                {
                    _gameManager.ResumeAudio();
                }
            }
            if (GameState.PresentsOpen)
            {
                if (InputManager.ConfirmPressed)
                    System.Diagnostics.Debug.WriteLine("Selection Confirmed");

                if (InputManager.UsePresentPressed)
                    System.Diagnostics.Debug.WriteLine("Present Used");

                if (InputManager.TogglePresentsPressed)
                    GameState.TogglePresents();

                    return;
            }
            if (GameState.MapOpen)
            {
                if (InputManager.ToggleMapPressed)
                    GameState.ToggleMap();

                    return;
            }

            if (InputManager.TogglePresentsPressed)
            {
                GameState.TogglePresents();
            }
            if (InputManager.ToggleMapPressed)
            {
                GameState.ToggleMap();
            }
            if (InputManager.SneakHeld)
            {
                System.Diagnostics.Debug.WriteLine("Sneak Activated");
            }
            _gameManager.Update(gameTime);
            _camera.Trail(_gameManager.Player.Bounds, new Vector2(1024, 768));
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin(transformMatrix: _camera.GetViewMatrix(), samplerState: SamplerState.PointClamp);
            if (_inMainMenu)
            {
                _mainMenu.Draw(_spriteBatch);
                _spriteBatch.End();
                return;
            }

            if (_inCharacterMenu)
            {
                _characterMenu.Draw(_spriteBatch);
                _spriteBatch.End();
                return;
            }

            _tileManager.Draw(_spriteBatch);
            _gameManager.Draw();
            _spriteBatch.End();
            base.Draw(gameTime);

        }
    }
}
