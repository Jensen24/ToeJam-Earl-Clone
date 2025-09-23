
public static class InputManager
{
    private static KeyboardState _prevKeyboardState;
    private static Vector2 _direction;

    public static Vector2 Direction => _direction;
    public static bool Moving => _direction != Vector2.Zero;

    public static bool PausePressed { get; private set; }
    public static bool ConfirmPressed { get; private set; }
    public static bool SneakHeld { get; private set; }
    public static bool UsePresentPressed { get; private set; }
    public static bool TogglePresentsPressed { get; private set; }
    public static bool ToggleMapPressed { get; private set; }

    public static void Update()
    {
        var keyboardState = Keyboard.GetState();

        // Menu keys always enabled
        PausePressed = keyboardState.IsKeyDown(Keys.Z) && !_prevKeyboardState.IsKeyDown(Keys.Z);
        ToggleMapPressed = keyboardState.IsKeyDown(Keys.V) && !_prevKeyboardState.IsKeyDown(Keys.V);

        if (GameState.MapOpen)
        {
            _direction = Vector2.Zero;
            SneakHeld = false;
            UsePresentPressed = false;
            ConfirmPressed = false;
            TogglePresentsPressed = false;
            ToggleMapPressed = keyboardState.IsKeyDown(Keys.V) && !_prevKeyboardState.IsKeyDown(Keys.V);

            _prevKeyboardState = keyboardState;
            return;
        }

        if (GameState.PresentsOpen)
        {
            _direction = Vector2.Zero;
            SneakHeld = false;
            UsePresentPressed = keyboardState.IsKeyDown(Keys.X) && !_prevKeyboardState.IsKeyDown(Keys.X); ;
            ConfirmPressed = keyboardState.IsKeyDown(Keys.X) && !_prevKeyboardState.IsKeyDown(Keys.X);
            TogglePresentsPressed = keyboardState.IsKeyDown(Keys.C) && !_prevKeyboardState.IsKeyDown(Keys.C);
            ToggleMapPressed = false;

            _prevKeyboardState = keyboardState;
            return;
        }

        // If game is paused, disable all controls
        if (GameState.Paused)
        {
            _direction = Vector2.Zero;
            SneakHeld = false;
            UsePresentPressed = false;
            ConfirmPressed = false;
            TogglePresentsPressed = false;
            ToggleMapPressed = false;
        }
        // Normal Gameplay
        else
        {
            _direction = Vector2.Zero;
            if (keyboardState.IsKeyDown(Keys.Left)) _direction.X--;
            if (keyboardState.IsKeyDown(Keys.Right)) _direction.X++;
            if (keyboardState.IsKeyDown(Keys.Up)) _direction.Y--;
            if (keyboardState.IsKeyDown(Keys.Down)) _direction.Y++;

            ToggleMapPressed = keyboardState.IsKeyDown(Keys.V) && !_prevKeyboardState.IsKeyDown(Keys.V);
            TogglePresentsPressed = keyboardState.IsKeyDown(Keys.C) && !_prevKeyboardState.IsKeyDown(Keys.C);
            SneakHeld = keyboardState.IsKeyDown(Keys.X);
            UsePresentPressed = false;
            ConfirmPressed = false;
        }
        _prevKeyboardState = keyboardState;
    }
}

