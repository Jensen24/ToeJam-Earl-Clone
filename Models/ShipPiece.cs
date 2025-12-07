using System;
using System.Collections.Generic;
using static GameObject;



public class ShipPiece : Item
{
    private SoundEffectInstance YoinkInstance;
    private float _scale = 1.5f;
    private AnimationManager _anims = new();
    private ShipPieceManager _shipPiecemanager;
	public ShipPiece(Rectangle bounds, ShipPieceManager shipPieceManager) : base(bounds)
    {
        SoundEffect Yoink = Globals.Content.Load<SoundEffect>("PickUp");
        YoinkInstance = Yoink.CreateInstance();
        YoinkInstance.Volume = 1f;
        YoinkInstance.Pitch = -0.5f;
        _shipPiecemanager = shipPieceManager;

        Texture2D shipPiece = Globals.Content.Load<Texture2D>("Items");
        ShapeType = CollisionShape.Rectangle;
        Width = bounds.Width;
        Height = bounds.Height;
        IsActive = true;

        var idle = new List<Rectangle>
        {
            new Rectangle(16, 149, 64, 51),
            new Rectangle(96, 149, 64, 51),
        };
        _anims.AddAnimation("Idle", new Animation(shipPiece, idle, 0.85f, new Vector2(_scale, _scale)));
    }
	public virtual void OnCollected(Player p)
    {
		YoinkInstance.Play();
		IsActive = false;
        _shipPiecemanager.OnPieceCollected();
        _shipPiecemanager._gameManager.RemoveObject(this);
    }
    public override void Update(GameTime gameTime)
    {
        if (!IsActive) return;
        _anims.Update("Idle", gameTime);
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        if (!IsActive) return;
        _anims.Draw(spriteBatch, Position);
    }
}
