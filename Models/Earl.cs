using System;
using System.Collections.Generic;
using static GameObject;


public class Earl : Player
{
    // lower speed by 15% // raise health by %15
    private float _speed = 200f;
    private float _scale = 1.5f;
    private AnimationManager _anims = new();
    public Earl(Vector2 startPos) : base(startPos)
    {
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
    public override void Update(GameTime gameTime)
    {
        // Arrow Keys: Movement
        bool sneaking = InputManager.SneakHeld;
        base.Update(gameTime);
        if (InputManager.Moving)
        {
            float moveSpeed = sneaking ? _speed * 0.5f : _speed; // If sneaking is held, speed is halfed then returned
            Position += Vector2.Normalize(InputManager.Direction) * moveSpeed * Globals.TotalSeconds;
        }

        string key = GetAnimKeyFromDirection(InputManager.Direction, sneaking);
        _anims.Update(key, gameTime);
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        if (!IsActive) return;
        _anims.Draw(spriteBatch, Position);
    }
}
