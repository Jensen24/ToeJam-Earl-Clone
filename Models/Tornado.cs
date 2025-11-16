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
    private Player _player;
    private readonly Random rand = new Random();
    private float _alertTimer = 0f;
    private float _captureTimer = 0f;
    private readonly float _alertDuration = 6f;
    private readonly float _captureDuration = 5f;
    private float _speed = 50f;
    private float _scale = 1.5f;
    private AnimationManager _anims = new();

    public Tornado(Vector2 startPos, Player player) : base(startPos)
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
        _player = player;
    }

    public override void Update(GameTime gameTime)
    {
        float deltaTime = Globals.TotalSeconds;
        switch (_state)
        {
            case TornadoState.Patrol:
                PatrolBehavior(deltaTime,gameTime);
                _anims.Update("Idle", gameTime);
                break;

            case TornadoState.Alert:
                AlertBehavior(deltaTime, gameTime);
                _anims.Update("Idle", gameTime);
                break;
            
            case TornadoState.Capture:
                CaptureBehavior(deltaTime, gameTime);
                _anims.Update(_capturedPlayer is ToeJam ? "Capture ToeJam" : "Capture Earl", gameTime);
                break;

            case TornadoState.Death:
                DeathBehavior(deltaTime, gameTime);
                break;
        }
    }

    private void PatrolBehavior(float deltaTime, GameTime gameTime)
    {
        _anims.Update("Idle", gameTime);

        _position += new Vector2((float)Math.Sin(Globals.TotalSeconds * 1f) * _speed * deltaTime, (float)Math.Cos(Globals.TotalSeconds * 1.5f) * _speed * deltaTime);

        // Player in proxy --> Alert state
        var player = _player;
        if (Vector2.Distance(Position, player.Position) < 100f)
            {
                _state = TornadoState.Alert;
                _alertTimer = _alertDuration;
            }
    }

    private void AlertBehavior(float deltaTime, GameTime gameTime)
    {
        _alertTimer -= deltaTime;
        _position += new Vector2((float)(rand.NextDouble() - 0.5f) * 30f * deltaTime, (float)(rand.NextDouble() - 0.5f) * 30f * deltaTime);
        _anims.Update("Idle", gameTime);

        if (_alertTimer <= 0f)
        {
            _state = TornadoState.Patrol;
        }
    }
    public void StartCapture(Player player)
    {
        if (_player == null) return;
        _capturedPlayer = _player;
        _state = TornadoState.Capture;
        _captureTimer = _captureDuration;

        if (_player is ToeJam)
            // might need to switch to _anims.Play if glitchy
            _anims.Update("Capture ToeJam", new GameTime());
        else
            _anims.Update("Capture Earl", new GameTime());

        _player.StartTornadoCapture();
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
        if (!IsActive) return;
        _anims.Draw(spriteBatch, Position);
    }
}