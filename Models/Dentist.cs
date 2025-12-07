using System;
using System.Collections.Generic;
using System.IO;
using static GameObject;

public enum DentistState
{
    Patrol,
    Alert,
    Cooldown,
}
public class madDentist : Enemy
{
    private float _scale = 1.5f;
    private float _speed = 125f;
    private DentistState _state = DentistState.Patrol;
    private List<Vector2> ZPath;
    private int _currentPathIndex = 0;
    private float _pathMax = 10f;
    private Player _player;
    private readonly Random rand = new Random();
    private float _alertTimer = 0f;
    private SpriteEffects _flip = SpriteEffects.None;
    private readonly float _alertDuration = 3f;
    private float _CDTimer = 0f;
    private readonly float _CDDuration = 2.5f;
    private AnimationManager _anims = new();
    public madDentist(Vector2 startPos, Player player) : base(startPos)
    {
        Texture2D dentist = Globals.Content.Load<Texture2D>("Dentist");

        var walkDown = new List<Rectangle>
        {
            new Rectangle(5, 142, 22, 34),
            new Rectangle(53, 144, 22, 32),
            new Rectangle(97, 141, 31, 35),
            new Rectangle(147, 141, 27, 35),
            new Rectangle(197, 142, 22, 34),
            new Rectangle(247, 143, 19, 33),
            new Rectangle(291, 141, 25, 35),
            new Rectangle(340, 141, 25, 35),
        };
        var walkUp = new List<Rectangle>
        {
            new Rectangle(6, 14, 22, 34),
            new Rectangle(55, 16, 20, 32),
            new Rectangle(99, 13, 29, 35),
            new Rectangle(148, 13, 26, 35),
            new Rectangle(198, 14, 22, 34),
            new Rectangle(248, 15, 19, 33),
            new Rectangle(293, 13, 25, 35),
            new Rectangle(341, 13, 24, 35),
        };
        // SpriteEffects.FlipHorizontally in SpriteBatch Draw
        var walkLeft = new List<Rectangle>
        {
            new Rectangle(5, 77, 22, 35),
            new Rectangle(52, 73, 24, 36),
            new Rectangle(100, 68, 58, 36),
            new Rectangle(175, 73, 34, 39),
            new Rectangle(229, 77, 22, 35),
            new Rectangle(274, 70, 27, 36),
            new Rectangle(325, 69, 54, 36),
            new Rectangle(399, 79, 35, 33),
        };
        var cooldown = new List<Rectangle>
        {
            new Rectangle(5, 77, 22, 35),
            new Rectangle(52, 73, 24, 36),
            new Rectangle(147, 205, 59, 35),
            new Rectangle(222, 207, 51, 33),
            new Rectangle(286, 207, 51, 33),
            new Rectangle(222, 207, 51, 33),
            new Rectangle(286, 207, 51, 33),
            new Rectangle(222, 207, 51, 33),
            new Rectangle(286, 207, 51, 33),
            new Rectangle(147, 205, 59, 35),
        };
        _anims.AddAnimation("Down", new Animation(dentist, walkDown, 0.15f, new Vector2(_scale, _scale)));
        _anims.AddAnimation("Up", new Animation(dentist, walkUp, 0.15f, new Vector2(_scale, _scale)));
        _anims.AddAnimation("Left", new Animation(dentist, walkLeft, 0.15f, new Vector2(_scale, _scale)));
        _anims.AddAnimation("Cooldown", new Animation(dentist, cooldown, 0.20f, new Vector2(_scale, _scale)));
        _player = player;

        Vector2 spawnPoint = new Vector2(3000, 1700);
        Vector2 rightPoint = new Vector2(3150, 2450);
        Vector2 LeftPoint = new Vector2(3000, 1400);
        Vector2 EndPoint = new Vector2(3000, 3200);

        ZPath = new List<Vector2>()
        {
            spawnPoint,
            rightPoint,
            LeftPoint,
            EndPoint
        };
    }    

    public override void Update(GameTime gameTime)
    {
        float deltaTime = Globals.TotalSeconds;
        switch (_state)
        {
            case DentistState.Patrol:
                PatrolBehavior(deltaTime, gameTime);
                break;

            case DentistState.Alert:
                AlertBehavior(deltaTime, gameTime);
                break;

            case DentistState.Cooldown:
                CDBehavior(deltaTime, gameTime);
                break;
        }
    }
    public void StartCooldown()
    {
        _state = DentistState.Cooldown;
        _CDTimer = _CDDuration;
        
    }
    private void UpdateFacingDirection(Vector2 direction, GameTime gameTime)
    {
        if (Math.Abs(direction.X) > Math.Abs(direction.Y))
        {
            if (direction.X > 0) 
            {
                _flip = SpriteEffects.FlipHorizontally;
                _anims.Update("Left", gameTime);
            }
            else
            {
                _flip = SpriteEffects.None;
                _anims.Update("Left", gameTime);
            }
        }
        else
        {
            _flip = SpriteEffects.None;
            if (direction.Y > 0)
                _anims.Update("Down", gameTime); 
            else
                _anims.Update("Up", gameTime); 
        }
    }

    private Vector2 FollowPath(float deltaTime, GameTime gameTime)
    {
        Vector2 target = ZPath[_currentPathIndex];
        Vector2 direction = target - Position;
        if (direction != Vector2.Zero)
            direction.Normalize();

        _position += direction * _speed * deltaTime;

        UpdateFacingDirection(direction, gameTime);

        if (Vector2.Distance(_position, target) < _pathMax)
        {
            _currentPathIndex++;
            if (_currentPathIndex >= ZPath.Count)
            {
                ZPath.Reverse();
                _currentPathIndex = 1;
            }
        }
        return direction;
    }

    private void PatrolBehavior(float deltaTime, GameTime gameTime)
    {
        if (_state == DentistState.Cooldown)
            return;

        FollowPath(deltaTime, gameTime);

        // Player in proxy --> Alert state
        if (Vector2.Distance(Position, _player.Position) < 220f)
        {
            _state = DentistState.Alert;
            _alertTimer = _alertDuration;
        }
    }

    private void AlertBehavior(float deltaTime, GameTime gameTime)
    {
        if (_state == DentistState.Cooldown)
            return;

        _alertTimer -= deltaTime;
        Vector2 direction = _player.Position - Position;
        if (direction != Vector2.Zero)
            direction.Normalize();

        _position += direction * _speed * 1.5f * deltaTime;
        UpdateFacingDirection(direction, gameTime);

        if (_alertTimer <= 0f)
        {
            _state = DentistState.Patrol;
        }
    }
    public void CDBehavior(float deltaTime, GameTime gameTime)
    {
        _anims.Update("Cooldown", gameTime);
        _CDTimer -= deltaTime;

        if (_CDTimer <= 0f)
        {
            _state = DentistState.Patrol;
        }

    }
    public override void Draw(SpriteBatch spriteBatch)
    {
        _anims.Draw(spriteBatch, Position, _flip);
    }
}
