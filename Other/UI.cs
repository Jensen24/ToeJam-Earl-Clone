using System;
using System.Collections.Generic;
using static GameObject;

public class UI
{
    private AnimationManager _tjHud = new();
    private AnimationManager _eHud = new();
    private AnimationManager _midSec = new();
    private AnimationManager _vacay = new();
    private Player _player;
    private Vector2 _tjHudPos = new Vector2(230, 720);
    private Vector2 _midSectionPos = new Vector2(512, 720);
    private Vector2 _eHudPos = new Vector2(790, 720);
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
            new Rectangle(155, 8, 26, 32),
        };
        var vactionHud = new List<Rectangle>
        {
            new Rectangle(8, 48, 320, 32),

        };
        _tjHud.AddAnimation("ToeJamHud", new Animation(ui, tjHud, 0.15f, new Vector2(3.2f, 3.2f)));
        _eHud.AddAnimation("EarlHud", new Animation(ui, eHud, 0.15f, new Vector2(3.2f, 3.2f)));
        _midSec.AddAnimation("MidSection", new Animation(ui, midSection, 0.15f, new Vector2(3.2f, 3.2f)));
        _vacay.AddAnimation("VactionHud", new Animation(ui, vactionHud, 0.15f, new Vector2(3.2f, 3.2f)));
    }

    public void SetPlayer(Player player)
    {
        _player = player;
    }

    public void Update(GameTime gameTime)
    {
        _vacay.Update("VactionHud", gameTime);
        _midSec.Update("MidSection", gameTime);

        if (_player is ToeJam)
        {
            _tjHud.Update("ToeJamHud", gameTime);
        }
        else if (_player is Earl)
            _eHud.Update("EarlHud", gameTime);
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        _vacay.Draw(spriteBatch, _vactionHudPos);
        _midSec.Draw(spriteBatch, _midSectionPos);

        if (_player is ToeJam)
        {
            _tjHud.Draw(spriteBatch, _tjHudPos);
        }
        else if (_player is Earl)
        {
            _eHud.Draw(spriteBatch, _eHudPos);
        }

    }
}