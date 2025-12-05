
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using static GameObject;
// Future reminder for collision boxes: Red = Wobble near edge // Yellow = Decorations so Solid collision // Green = transition to previous level // Pink = Water so maybe animation for swimming then swim??
// Indices = Green = 3 // Yellow = 1// Red = 0// Pink = 2
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
        private PauseMenu _pauseMenu;
        private UI _ui;
        private bool _inMainMenu = true;
        private bool _inCharacterMenu = false;
        private float _delayCounter = 0f;
        private const float _delayTimer = 0.5f;
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
            _pauseMenu = new PauseMenu();
            _ui = new UI();
            _tileManager = new TileManager();
            _gameManager = new();
            _gameManager.Init(_tileManager);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _mainMenu = new MainMenu();
            _characterMenu = new CharacterMenu();
            Globals.SpriteBatch = _spriteBatch;
            _tileManager.LoadContent(Content);
            // world 1 
            _tileManager.LoadTileLayer(
            "../../../Data/level1_collisions.csv",
            "../../../Data/level1_dl.csv",
            "../../../Data/level1_gl.csv",
            "../../../Data/level1_wl.csv",
            "../../../Data/level1_sl.csv");
            // world 2
            // world 3
            // world 4
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
                    _delayCounter = _delayTimer;
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
                // Slight delay to prevent accidental input carryover
                if (_delayCounter > 0f)
                {
                    _delayCounter -= Globals.TotalSeconds;
                    return;
                }
                string character = _characterMenu.Update();
                if (character == "ToeJam" || character == "Earl")
                {
                    _gameManager.StartGame(character);
                    _ui.SetPlayer(_gameManager.Player);
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
                return;
            }
            if (GameState.Paused)
            {
                string pauseResult = _pauseMenu.Update();

                if (pauseResult == "Resume")
                {
                    GameState.TogglePause();
                    _gameManager.ResumeAudio();
                }
                else if (pauseResult == "Quit")
                {
                    Exit();
                }
                return;
            }
            if (InputManager.TogglePresentsPressed)
            {
                GameState.TogglePresents();
                return;
            }
            if (GameState.PresentsOpen)
            {
                if (InputManager.ConfirmPressed)
                    System.Diagnostics.Debug.WriteLine("Selection Confirmed");

                if (InputManager.UsePresentPressed)
                    System.Diagnostics.Debug.WriteLine("Present Used");

                return;
            }
            if (InputManager.ToggleMapPressed)
            {
                GameState.ToggleMap();
                return;
            }
            if (GameState.MapOpen)
            {
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
            else if (_inCharacterMenu)
            {
                _characterMenu.Draw(_spriteBatch);
                _spriteBatch.End();
                return;
            }
            _tileManager.Draw(_spriteBatch);
            _gameManager.Draw();
            _spriteBatch.End();

            _spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            _ui.Draw(_spriteBatch);
            _spriteBatch.End();

            if (GameState.Paused)
            {
                _spriteBatch.Begin(samplerState: SamplerState.PointClamp);
                _pauseMenu.Draw(_spriteBatch, GraphicsDevice.Viewport);
                _spriteBatch.End();
            }
            if (GameState.PresentsOpen)
            {
                // Draw Presents Menu
            }
            if (GameState.MapOpen)
            {
                // Draw Map
            }
            base.Draw(gameTime);
        }
    }
}
