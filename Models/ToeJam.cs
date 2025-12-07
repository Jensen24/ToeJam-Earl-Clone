using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using static GameObject;

public class ToeJam : Player
{
    //private float health 170f;
    private float _speed = 230f;
    //private float RoadBoostMulti = 1.1f;
    private float SubmergedMulti = 0.9f;
    private float _scale = 1.5f;
    private AnimationManager _anims = new();
    private TileEffectState _currentEffect = TileEffectState.None;
	public ToeJam(Vector2 startPos) : base(startPos)
    {
        Radius = 16f;
        ShapeType = CollisionShape.Circle;
        Texture2D toeJam = Globals.Content.Load<Texture2D>("ToeJam");
        var idle = new List<Rectangle>
		{
			new Rectangle(15, 14, 22, 25),
            new Rectangle(46, 12, 22, 27),
            new Rectangle(77, 14, 23, 25),
        };
		var walkDown = new List<Rectangle> 
		{
            new Rectangle(20, 81, 24, 32),
            new Rectangle(55, 82, 21, 30),
            new Rectangle(86, 82, 27, 31),
            new Rectangle(123, 82, 22, 30),
            new Rectangle(157, 82, 21, 30),
            new Rectangle(186, 81, 27, 32),
        };
        var walkUp = new List<Rectangle>
        {
            new Rectangle(22, 132, 27, 31),
            new Rectangle(62, 133, 21, 30),
            new Rectangle(93, 131, 24, 32),
            new Rectangle(125, 131, 27, 32),
            new Rectangle(160, 132, 22, 31),
            new Rectangle(190, 133, 22, 30),
        };
        var walkLeft = new List<Rectangle>
        {
            new Rectangle(239, 84, 20, 28),
            new Rectangle(271, 83, 25, 29),
            new Rectangle(304, 82, 24, 30),
            new Rectangle(336, 83, 23, 29),
            new Rectangle(372, 85, 20, 27),
            new Rectangle(403, 85, 22, 27),
        };
        var walkRight = new List<Rectangle>
        {
            new Rectangle(241, 133, 22, 27),
            new Rectangle(274, 133, 20, 27),
            new Rectangle(307, 131, 23, 29),
            new Rectangle(338, 130, 24, 30),
            new Rectangle(370, 131, 25, 29),
            new Rectangle(407, 132, 20, 28),
        };
        var sneakUp = new List<Rectangle>
        {
            new Rectangle(24, 249, 17, 24),
            new Rectangle(52, 247, 17, 28),
            new Rectangle(82, 247, 17, 25),

        };
        var sneakDown = new List<Rectangle>
        {
            new Rectangle(26, 204, 18, 28),
            new Rectangle(54, 204, 18, 25),
            new Rectangle(85, 204, 18, 24),

        };
        var sneakRight = new List<Rectangle>
        {
            new Rectangle(132, 247, 14, 21),
            new Rectangle(157, 246, 14, 23),
            new Rectangle(183, 244, 14, 24),

        };
        var sneakLeft = new List<Rectangle>
        {
            new Rectangle(133, 202, 14, 25),
            new Rectangle(158, 204, 15, 23),
            new Rectangle(184, 205, 15, 21),

        };
        var edgeWobbleLeft = new List<Rectangle>
        {
            new Rectangle(351, 926, 26, 24),
            new Rectangle(383, 924, 27, 26),

        };
        var edgeWobbleRight = new List<Rectangle>
        {
            new Rectangle(350, 959, 26, 24),
            new Rectangle(379, 957, 27, 26),

        };
        var edgeWobbleDown = new List<Rectangle>
        {
            new Rectangle(283, 926, 26, 24),
            new Rectangle(314, 926, 26, 24),

        };
        var edgeWobbleUp = new List<Rectangle>
        {
            new Rectangle(285, 961, 25, 24),
            new Rectangle(315, 961, 25, 24),

        };
        var swimDown = new List<Rectangle>
        {
            new Rectangle(202, 920, 21, 17),
        };
        var swimUp = new List<Rectangle>
        {
            new Rectangle(202, 967, 19, 15),
        };
        var swimLeft = new List<Rectangle>
        {
            new Rectangle(233, 922, 22, 14),
        };
        var swimRight = new List<Rectangle>
        {
            new Rectangle(232, 969, 22, 14),
        };

        _anims.AddAnimation("Idle", new Animation(toeJam, idle, 0.30f, new Vector2(_scale, _scale)));
        _anims.AddAnimation("Down", new Animation(toeJam, walkDown, 0.20f, new Vector2(_scale, _scale)));
        _anims.AddAnimation("Up", new Animation(toeJam, walkUp, 0.20f, new Vector2(_scale, _scale)));
        _anims.AddAnimation("Left", new Animation(toeJam, walkLeft, 0.20f, new Vector2(_scale, _scale)));
        _anims.AddAnimation("Right", new Animation(toeJam, walkRight, 0.20f, new Vector2(_scale, _scale)));
        // Sneak
        _anims.AddAnimation("sneakDown", new Animation(toeJam, sneakDown, 0.20f, new Vector2(_scale, _scale)));
        _anims.AddAnimation("sneakUp", new Animation(toeJam, sneakUp, 0.20f, new Vector2(_scale, _scale)));
        _anims.AddAnimation("sneakLeft", new Animation(toeJam, sneakLeft, 0.20f, new Vector2(_scale, _scale)));
        _anims.AddAnimation("sneakRight", new Animation(toeJam, sneakRight, 0.20f, new Vector2(_scale, _scale)));
        // Edge Wobble
        _anims.AddAnimation("Wobble Down", new Animation(toeJam, edgeWobbleDown, 0.30f, new Vector2(_scale, _scale)));
        _anims.AddAnimation("Wobble Up", new Animation(toeJam, edgeWobbleUp, 0.30f, new Vector2(_scale, _scale)));
        _anims.AddAnimation("Wobble Left", new Animation(toeJam, edgeWobbleLeft, 0.30f, new Vector2(_scale, _scale)));
        _anims.AddAnimation("Wobble Right", new Animation(toeJam, edgeWobbleRight, 0.30f, new Vector2(_scale, _scale)));
        // Swim
        _anims.AddAnimation("Swim Down", new Animation(toeJam, swimDown, 0.30f, new Vector2(_scale, _scale)));
        _anims.AddAnimation("Swim Up", new Animation(toeJam, swimUp, 0.30f, new Vector2(_scale, _scale)));
        _anims.AddAnimation("Swim Left", new Animation(toeJam, swimLeft, 0.30f, new Vector2(_scale, _scale)));
        _anims.AddAnimation("Swim Right", new Animation(toeJam, swimRight, 0.30f, new Vector2(_scale, _scale)));

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
