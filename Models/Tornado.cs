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
                PatrolBehavior(deltaTime);
                break;

            case TornadoState.Alert:
                AlertBehavior(deltaTime);
                break;
            
            case TornadoState.Capture:
                CaptureBehavior(deltaTime);
                break;

            case TornadoState.Death:
                DeathBehavior(deltaTime);
                break;
        }
    }

    private void PatrolBehavior(float deltaTime)
    {
        _anims.Update("Idle", Globals.gametime);

        Position.X += (float)Math.Sin(Globals.TotalSeconds * 1.5f) * _speed * deltaTime;
        Position.Y += (float)Math.Cos(Globals.TotalSeconds * 1f) * _speed * deltaTime;

        // Player in proxy --> Alert state
        var player = Globals.Player;
        if (Vector2.Distance(Position, player.Position) < 100f)
            {
                _state = TornadoState.Alert;
                _alertTimer = _alertDuration;
            }
    }

    private void AlertBehavior(float deltaTime)
    {
        _alertTimer -= deltaTime;
        Position.X += (float)(rand.NextDouble() - 0.5f) * 30f * deltaTime;
        Position.Y += (float)(rand.NextDouble() - 0.5f) * 30f * deltaTime;
        _anims.Update("Idle", Globals.gameTime);

        if (_alertTimer <= 0f)
        {
            _state = TornadoState.Patrol;
            _captureTimer = _captureDuration;

            if (Globals.Player is ToeJam)
                // might need to switch to _anims.Play if glitchy
                _anims.Update("Capture ToeJam", Globals.gameTime);
            else 
                _anims.Update("Capture Earl", Globals.gameTime);

            Globals.Player.StartTornadoCapture();
        }
    }

    private void CaptureBehavior(float deltaTime)
    {
        _captureTimer -= deltaTime;
        Globals.Player.Position += new Vector2(Globals.Random.Next(-40, 40), Globals.Random.Next(-40, 40)) * dt;
        _anims.Update(null, Globals.gameTime);

        if (_captureTimer <= 0f)
        {
            _state = TornadoState.Death;
            Globals.Player.EndTornadoCapture();
        }
    }

    private void DeathBehavior(float deltaTime)
    {
        IsActive = false;
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        _anims.Draw(spriteBatch, Position);
    }
}