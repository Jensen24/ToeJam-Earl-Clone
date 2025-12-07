using System;
using System.Collections.Generic;
using static GameObject;

public class Earl : Player
{
    // Player stats
    private float _speed = 170f;
    private float SubmergedMulti = 0.8f;

    private float _scale = 1.5f;
    private AnimationManager _anims = new();
    private TileEffectState _currentEffect = TileEffectState.None;
    public Earl(Vector2 startPos) : base(startPos)
    {
        MaxHealth = 230f;
        Health = MaxHealth;
        Radius = 16f;
        ShapeType = CollisionShape.Circle;
        Texture2D earl = Globals.Content.Load<Texture2D>("Earl");
        var idle = new List<Rectangle>
        {
            new Rectangle(9, 13, 23, 33),
            new Rectangle(37, 16, 24, 30),
            new Rectangle(65, 13, 23, 33),
        };
        var walkDown = new List<Rectangle>
        {
            new Rectangle(5, 87, 22, 34),
            new Rectangle(37, 85, 22, 36),
            new Rectangle(70, 82, 21, 39),
            new Rectangle(105, 86, 21, 34),
            new Rectangle(137, 85, 24, 34),
            new Rectangle(173, 86, 19, 34),
        };
        var walkUp = new List<Rectangle>
        {
            new Rectangle(5, 127, 21, 36),
            new Rectangle(37, 124, 22, 39),
            new Rectangle(68, 128, 21, 35),
            new Rectangle(95, 130, 24, 33),
            new Rectangle(125, 130, 19, 33),
            new Rectangle(149, 129, 23, 34),
            new Rectangle(178, 129, 21, 34),
        };
        var walkLeft = new List<Rectangle>
        {
            new Rectangle(208, 85, 26, 34),
            new Rectangle(239, 85, 32, 34),
            new Rectangle(274, 87, 32, 32),
            new Rectangle(317, 84, 22, 36),
            new Rectangle(346, 81, 25, 39),
            new Rectangle(373, 84, 37, 34),
            new Rectangle(412, 84, 32, 34),
        };
        var walkRight = new List<Rectangle>
        {
            new Rectangle(208, 127, 25, 34),
            new Rectangle(238, 127, 32, 34),
            new Rectangle(274, 129, 32, 32),
            new Rectangle(316, 126, 22, 35),
            new Rectangle(343, 123, 25, 39),
            new Rectangle(373, 126, 37, 34),
            new Rectangle(412, 126, 32, 33),
        };
        var sneakUp = new List<Rectangle>
        {
            new Rectangle(3, 246, 28, 39),
            new Rectangle(35, 251, 26, 32),
            new Rectangle(66, 250, 32, 32),
            new Rectangle(102, 253, 28, 29),
        };
        var sneakDown = new List<Rectangle>
        {
            new Rectangle(2, 201, 30, 39),
            new Rectangle(35, 206, 26, 34),
            new Rectangle(65, 205, 32, 35),
            new Rectangle(101, 211, 30, 29),
        };
        var sneakRight = new List<Rectangle>
        {
            new Rectangle(134, 215, 40, 25),
            new Rectangle(176, 204, 24, 36),
            new Rectangle(204, 206, 31, 34),
            new Rectangle(236, 210, 32, 30),
        };
        var sneakLeft = new List<Rectangle>
        {
            new Rectangle(235, 250, 32, 30),
            new Rectangle(202, 246, 31, 34),
            new Rectangle(176, 244, 24, 36),
            new Rectangle(133, 255, 40, 25),
        };
        var edgeWobbleLeft = new List<Rectangle>
        {
            new Rectangle(353, 922, 38, 35),
            new Rectangle(395, 922, 33, 35),

        };
        var edgeWobbleRight = new List<Rectangle>
        {
            new Rectangle(355, 960, 38, 35),
            new Rectangle(393, 961, 33, 35),

        };
        var edgeWobbleDown = new List<Rectangle>
        {
            new Rectangle(277, 924, 32, 32),
            new Rectangle(311, 959, 40, 35),

        };
        var edgeWobbleUp = new List<Rectangle>
        {
            new Rectangle(278, 961, 32, 32),
            new Rectangle(315, 961, 25, 24),

        };
        var swimDown = new List<Rectangle>
        {
            new Rectangle(200, 918, 28, 20),
        };
        var swimUp = new List<Rectangle>
        {
            new Rectangle(201, 967, 23, 18),
        };
        var swimLeft = new List<Rectangle>
        {
            new Rectangle(231, 918, 26, 21),
        };
        var swimRight = new List<Rectangle>
        {
            new Rectangle(231, 964, 26, 21),
        };
        _anims.AddAnimation("Idle", new Animation(earl, idle, 0.30f, new Vector2(_scale, _scale)));
        _anims.AddAnimation("Down", new Animation(earl, walkDown, 0.20f, new Vector2(_scale, _scale)));
        _anims.AddAnimation("Up", new Animation(earl, walkUp, 0.20f, new Vector2(_scale, _scale)));
        _anims.AddAnimation("Left", new Animation(earl, walkLeft, 0.20f, new Vector2(_scale, _scale)));
        _anims.AddAnimation("Right", new Animation(earl, walkRight, 0.20f, new Vector2(_scale, _scale)));
        // Sneak
        _anims.AddAnimation("sneakDown", new Animation(earl, sneakDown, 0.20f, new Vector2(_scale, _scale)));
        _anims.AddAnimation("sneakUp", new Animation(earl, sneakUp, 0.20f, new Vector2(_scale, _scale)));
        _anims.AddAnimation("sneakLeft", new Animation(earl, sneakLeft, 0.20f, new Vector2(_scale, _scale)));
        _anims.AddAnimation("sneakRight", new Animation(earl, sneakRight, 0.20f, new Vector2(_scale, _scale)));
        // Edge Wobble
        _anims.AddAnimation("Wobble Down", new Animation(earl, edgeWobbleDown, 0.30f, new Vector2(_scale, _scale)));
        _anims.AddAnimation("Wobble Up", new Animation(earl, edgeWobbleUp, 0.30f, new Vector2(_scale, _scale)));
        _anims.AddAnimation("Wobble Left", new Animation(earl, edgeWobbleLeft, 0.30f, new Vector2(_scale, _scale)));
        _anims.AddAnimation("Wobble Right", new Animation(earl, edgeWobbleRight, 0.30f, new Vector2(_scale, _scale)));
        // Swim
        _anims.AddAnimation("Swim Down", new Animation(earl, swimDown, 0.30f, new Vector2(_scale, _scale)));
        _anims.AddAnimation("Swim Up", new Animation(earl, swimUp, 0.30f, new Vector2(_scale, _scale)));
        _anims.AddAnimation("Swim Left", new Animation(earl, swimLeft, 0.30f, new Vector2(_scale, _scale)));
        _anims.AddAnimation("Swim Right", new Animation(earl, swimRight, 0.30f, new Vector2(_scale, _scale)));

    }
    private string GetAnimKeyFromDirection(Vector2 direction, bool sneaking)
    {
        if (sneaking)
        {
            if (direction.Y > 0) return "sneakDown";
            if (direction.Y < 0) return "sneakUp";
            if (direction.X < 0) return "sneakLeft";
            if (direction.X > 0) return "sneakRight";
            return "Idle";
        }
        else
        {
            if (direction.Y > 0) return "Down";
            if (direction.Y < 0) return "Up";
            if (direction.X < 0) return "Left";
            if (direction.X > 0) return "Right";
            return "Idle";
        }
    }
    private string GetEffectAnimKey(TileEffectState effect, Vector2 dir)
    {
        switch (effect)
        {
            case TileEffectState.EdgeWobble:
                if (dir.Y > 0) return "Wobble Down";
                if (dir.Y < 0) return "Wobble Up";
                if (dir.X < 0) return "Wobble Left";
                if (dir.X > 0) return "Wobble Right";
                break;

            case TileEffectState.WaterSwim:
                if (dir.Y > 0) return "Swim Down";
                if (dir.Y < 0) return "Swim Up";
                if (dir.X < 0) return "Swim Left";
                if (dir.X > 0) return "Swim Right";
                break;
        }
        return "Idle";
    }

    private string GetCurrentAnimKey(Vector2 direction, bool sneaking)
    {
        if (_currentEffect != TileEffectState.None)
            return GetEffectAnimKey(_currentEffect, direction);

        return GetAnimKeyFromDirection(direction, sneaking);
    }
    public override void ApplyTileEffect(TileEffectState effect, Vector2 direction)
    {
        _currentEffect = effect;
    }
    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        if (InputLocked) return;

        bool sneaking = InputManager.SneakHeld;
        Vector2 inputDir = InputManager.Direction;
        Velocity = Vector2.Zero;

        // Update facing direction only when moving
        if (inputDir != Vector2.Zero)
            FacingDirection = inputDir;

        if (inputDir != Vector2.Zero)
        {
            float moveSpeed = sneaking ? _speed * 0.5f : _speed;
            //if (_currentEffect == TileEffectState.RoadBoost)
            //    moveSpeed *= RoadBoostMulti;
            if (_currentEffect == TileEffectState.WaterSwim)
                moveSpeed *= SubmergedMulti;

            Vector2 moveDir = Vector2.Normalize(inputDir);
            Velocity = moveDir * moveSpeed;
        }

        Position += Velocity * Globals.TotalSeconds;
        string animKey;

        if (inputDir == Vector2.Zero && _currentEffect == TileEffectState.None)
            animKey = "Idle";
        else
            animKey = GetCurrentAnimKey(FacingDirection, sneaking);
        _anims.Update(animKey, gameTime);
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        if (!IsActive) return;
        _anims.Draw(spriteBatch, Position);
    }
}