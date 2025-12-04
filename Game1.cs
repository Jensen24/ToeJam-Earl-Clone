
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
        // map start
        private Dictionary<Vector2, int> collisions1;
        private Dictionary<Vector2, int> dl1;
        private Dictionary<Vector2, int> gl1;
        private Dictionary<Vector2, int> wl1;
        private Dictionary<Vector2, int> sl1;
        private Texture2D WaterTexture;
        private Texture2D RoadTexture;
        private Texture2D AssetTexture;
        private Texture2D SkyboxTexture;
        private Texture2D DecosTexture;
        private Texture2D CollisionTexture;
        // map end

        private GameManager _gameManager;
        //private TileManager _tileManager;
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
            collisions1 = LoadMap("../../../Data/level1_collisions.csv");
            dl1 = LoadMap("../../../Data/level1_dl.csv");
            gl1 = LoadMap("../../../Data/level1_gl.csv");
            wl1 = LoadMap("../../../Data/level1_wl.csv");
            sl1 = LoadMap("../../../Data/level1_sl.csv"); ;
        }

        private Dictionary<Vector2, int> LoadMap(string filepath)
        {
            Dictionary<Vector2, int> result = new();

            StreamReader reader = new(filepath);

            int y = 0;
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                string[] items = line.Split(',');

                for (int x = 0; x < items.Length; x++)
                {
                    if (int.TryParse(items[x], out int value))
                    {
                        if (value > -1)
                        {
                            result[new Vector2(x, y)] = value;
                        }
                    }
                }
                y++;
            }
            return result;
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
            // Load map textures
            WaterTexture = Content.Load<Texture2D>("Water");
            RoadTexture = Content.Load<Texture2D>("Roads");
            AssetTexture = Content.Load<Texture2D>("Assets");
            SkyboxTexture = Content.Load<Texture2D>("Skybox");
            DecosTexture = Content.Load<Texture2D>("IslandDecos");
            CollisionTexture = Content.Load<Texture2D>("CollisionBoxes");
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
            _gameManager.Draw();

            int display_tile_size = 64;
            int pixel_tile_size = 16;
            int num_tiles_per_row;
            num_tiles_per_row = SkyboxTexture.Width / pixel_tile_size;
            foreach (var item in sl1)
            {
                var drect = new Rectangle(
                    (int)item.Key.X * display_tile_size,
                    (int)item.Key.Y * display_tile_size,
                    display_tile_size,
                    display_tile_size
                    );
                int index = item.Value;
                int x = index % num_tiles_per_row;
                int y = index / num_tiles_per_row;

                var src = new Rectangle(
                    x * pixel_tile_size,
                    y * pixel_tile_size,
                    pixel_tile_size,
                    pixel_tile_size
                    );
                _spriteBatch.Draw(SkyboxTexture, drect, src, Color.White);
            }

            num_tiles_per_row = WaterTexture.Width / pixel_tile_size;
            foreach (var item in wl1)
            {
                var drect = new Rectangle(
                    (int)item.Key.X * display_tile_size,
                    (int)item.Key.Y * display_tile_size,
                    display_tile_size,
                    display_tile_size
                    );
                int index = item.Value;
                int x = index % num_tiles_per_row;
                int y = index / num_tiles_per_row;

                var src = new Rectangle(
                    x * pixel_tile_size,
                    y * pixel_tile_size,
                    pixel_tile_size,
                    pixel_tile_size
                    );

                _spriteBatch.Draw(WaterTexture, drect, src, Color.White);
            }

            num_tiles_per_row = AssetTexture.Width / pixel_tile_size;
            foreach (var item in gl1)
            {
                var drect = new Rectangle(
                    (int)item.Key.X * display_tile_size,
                    (int)item.Key.Y * display_tile_size,
                    display_tile_size,
                    display_tile_size
                    );
                int index = item.Value;
                int x = index % num_tiles_per_row;
                int y = index / num_tiles_per_row;

                var src = new Rectangle(
                    x * pixel_tile_size,
                    y * pixel_tile_size,
                    pixel_tile_size,
                    pixel_tile_size
                    );

                _spriteBatch.Draw(AssetTexture, drect, src, Color.White);
            }

            num_tiles_per_row = DecosTexture.Width / pixel_tile_size;
            foreach (var item in dl1)
            {
                var drect = new Rectangle(
                    (int)item.Key.X * display_tile_size,
                    (int)item.Key.Y * display_tile_size,
                    display_tile_size,
                    display_tile_size
                    );
                int index = item.Value;
                int x = index % num_tiles_per_row;
                int y = index / num_tiles_per_row;

                var src = new Rectangle(
                    x * pixel_tile_size,
                    y * pixel_tile_size,
                    pixel_tile_size,
                    pixel_tile_size
                    );
                _spriteBatch.Draw(DecosTexture, drect, src, Color.White);
            }

            num_tiles_per_row = CollisionTexture.Width / pixel_tile_size;
            foreach (var item in collisions1)
            {
                var drect = new Rectangle(
                    (int)item.Key.X * display_tile_size,
                    (int)item.Key.Y * display_tile_size,
                    display_tile_size,
                    display_tile_size
                    );
                int tileIndex = item.Value;
                int x = tileIndex % num_tiles_per_row;
                int y = tileIndex / num_tiles_per_row;

                var src = new Rectangle(
                    x * pixel_tile_size,
                    y * pixel_tile_size,
                    pixel_tile_size,
                    pixel_tile_size
                    );
                _spriteBatch.Draw(CollisionTexture, drect, src, Color.White * 0.2f);
            }
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
