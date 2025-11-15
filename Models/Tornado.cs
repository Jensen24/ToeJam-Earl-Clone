using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using static GameObject;
public enum TornadoState
{
    Patrol,
    Alert,
    Capture,
    Death
}
public class Tornado : Enemy
{
    private TornadoState _state = TornadoState.Patrol;
    private Player _capturedPlayer = null;
    private readonly Random rand = new Random();
    private float _alertTimer = 0f;
    private float _captureTimer = 0f;
    private readonly float _alertDuration = 2f;
    private readonly float _captureDuration = 2f;
    private float _speed = 90f;
    private float _scale = 1.5f;
    private AnimationManager _anims = new();

    public Tornado(Vector2 startPos) : base(startPos)
    {
        Texture2D tornado = Globals.Content.Load<Texture2D>("Tornado");

        var idle = new List<Rectangle>
        {
            new Rectangle(9, 58, 30, 30),
            new Rectangle(57, 58, 30, 30),
            new Rectangle(105, 58, 30, 30),
            new Rectangle(154, 58, 29, 30),
        };
        var ToeJam = new List<Rectangle>
        {
            new Rectangle(9, 103, 30, 33),
            new Rectangle(57, 100, 30, 36),
            new Rectangle(105, 103, 30, 33),
            new Rectangle(152, 106, 33, 30),
        };
        var Earl = new List<Rectangle>
        {
            new Rectangle(9, 150, 30, 34),
            new Rectangle(57, 149, 30, 35),
            new Rectangle(104, 152, 33, 32),
            new Rectangle(154, 153, 29, 31),
        };
        _anims.AddAnimation("Idle", new Animation(tornado, idle, 0.15f, new Vector2(_scale, _scale)));
        _anims.AddAnimation("Capture ToeJam", new Animation(tornado, ToeJam, 0.15f, new Vector2(_scale, _scale)));
        _anims.AddAnimation("Capture Earl", new Animation(tornado, Earl, 0.15f, new Vector2(_scale, _scale)));
    }

    public override void Update(GameTime gameTime)
    {
        float deltaTime = Globals.TotalSeconds;
        switch (_state)
        {
            case TornadoState.Patrol:
                PatrolBehavior(deltaTime,gameTime);
                break;

            case TornadoState.Alert:
                AlertBehavior(deltaTime, gameTime);
                break;
            
            case TornadoState.Capture:
                CaptureBehavior(deltaTime, gameTime);
                break;

            case TornadoState.Death:
                DeathBehavior(deltaTime, gameTime);
                break;
        }
    }

    private void PatrolBehavior(float deltaTime, GameTime gameTime)
    {
        _anims.Update("Idle", gameTime);

        Position.X += (float)Math.Sin(Globals.TotalSeconds * 1.5f) * _speed * deltaTime;
        Position.Y += (float)Math.Cos(Globals.TotalSeconds * 1f) * _speed * deltaTime;

        // Player in proxy --> Alert state
        var player = GameObject.Player;
        if (Vector2.Distance(Position, player.Position) < 100f)
            {
                _state = TornadoState.Alert;
                _alertTimer = _alertDuration;
            }
    }

    private void AlertBehavior(float deltaTime, GameTime gameTime)
    {
        _alertTimer -= deltaTime;
        Position.X += (float)(rand.NextDouble() - 0.5f) * 30f * deltaTime;
        Position.Y += (float)(rand.NextDouble() - 0.5f) * 30f * deltaTime;
        _anims.Update("Idle", gameTime);

        if (_alertTimer <= 0f)
        {
            _state = TornadoState.Patrol;
        }
    }
    public void StartCapture(Player player, GameTime gameTime)
    {
        if (player == null) return;
        _capturedPlayer = player;
        _state = TornadoState.Capture;
        _captureTimer = _captureDuration;

        if (player is ToeJam)
            // might need to switch to _anims.Play if glitchy
            _anims.Update("Capture ToeJam", gameTime);
        else
            _anims.Update("Capture Earl", gameTime);

        player.StartTornadoCapture();
    }

    private void CaptureBehavior(float deltaTime, GameTime gameTime)
    {
        if (_capturedPlayer != null)
        {
            float jitterX = rand.Next(-40, 41) * deltaTime;
            float jitterY = rand.Next(-40, 41) * deltaTime;
            _capturedPlayer.BeginTornadoOffset(new Vector2(jitterX, jitterY));
        }

        _captureTimer -= deltaTime;
        _anims.Update(null, gameTime);

        if (_captureTimer <= 0f)
        {
            if(_capturedPlayer != null)
            {
                _capturedPlayer.EndTornadoCapture();
                _capturedPlayer = null;
            }
            _state = TornadoState.Death;
        }
    }

    private void DeathBehavior(float deltaTime, GameTime gameTime)
    {
        IsActive = false;
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        _anims.Draw(spriteBatch, Position);
    }
}