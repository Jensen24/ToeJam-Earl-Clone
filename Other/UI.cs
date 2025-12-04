using System;
using System.Collections.Generic;
using static GameObject;

public class UI
{
    private AnimationManager _anims = new();
    private Player _player;
    private Vector2 _tjHudPos = new Vector2(150, 720);
    private Vector2 _midSectionPos = new Vector2(512, 720);
    private Vector2 _eHudPos = new Vector2(874, 720);
    private Vector2 _vactionHudPos = new Vector2(512, 720);
    public UI()
    {
        Texture2D ui = Globals.Content.Load<Texture2D>("HUD");

        var tjHud = new List<Rectangle>
        {
            new Rectangle(8, 8, 146, 32),

        };
        var eHud = new List<Rectangle>
        {
            new Rectangle(182, 8, 146, 32),

        };
        var midSection = new List<Rectangle>
        {
            new Rectangle(155, 48, 26, 32),
        };
        var vactionHud = new List<Rectangle>
        {
            new Rectangle(8, 48, 320, 32),

        };
        _anims.AddAnimation("ToeJamHud", new Animation(ui, tjHud, 0.15f, new Vector2(3.2f, 3.2f)));
        _anims.AddAnimation("EarlHud", new Animation(ui, eHud, 0.15f, new Vector2(3.2f, 3.2f)));
        _anims.AddAnimation("MidSection", new Animation(ui, midSection, 0.15f, new Vector2(3.2f, 3.2f)));
        _anims.AddAnimation("VactionHud", new Animation(ui, vactionHud, 0.15f, new Vector2(3.2f, 3.2f)));
    }

    public void SetPlayer(Player player)
    {
        _player = player;
    }

    public void Update(GameTime gameTime)
    {
        _anims.Update("MidSection", gameTime);

        if (_player is ToeJam)
            _anims.Update("ToeJamHud", gameTime);
        else if (_player is Earl)
            _anims.Update("EarlHud", gameTime);
        else
            _anims.Update("VactionHud", gameTime);
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        _anims.Draw(_midSectionPos);

        if (_player is ToeJam)
            _anims.Draw(_tjHudPos);
        else if (_player is Earl)
            _anims.Draw(_eHudPos);
        else
            _anims.Draw(_vactionHudPos);
    }
}