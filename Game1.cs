
using System;

namespace ToeJam_Earl
{
    public class Game1 : Game
    {
        private GameManager _gameManager;
        private TileManager _tileManager;
        private readonly GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
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

            _gameManager = new();
            _gameManager.Init();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
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

            // Land of 'If' 
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
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            _tileManager.Draw(_spriteBatch);
            _gameManager.Draw();
            _spriteBatch.End();

            base.Draw(gameTime);

        }
    }
}
