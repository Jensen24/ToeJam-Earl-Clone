using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using static GameObject;

public class UI
{
    private AnimationManager _tjHud = new();
    private AnimationManager _tjHP = new();
    private AnimationManager _eHud = new();
    private AnimationManager _eHP = new();
    private AnimationManager _midSec = new();
    private AnimationManager _vacay = new();
    private AnimationManager _midNumbers = new();
    private Player _player;
    private Vector2 _tjHudPos = new Vector2(230, 720);
    private Vector2 _tjHPPos = new Vector2(135, 740);
    private Vector2 _midSectionPos = new Vector2(512, 720);
    private Vector2 _eHudPos = new Vector2(790, 720);
    private Vector2 _eHPPos = new Vector2(697, 740);
    private Vector2 _vactionHudPos = new Vector2(512, 720);
    private Vector2 _midNumbersPos = new Vector2(512, 720);
    private int _shipPartsCollected = 0;
    private SpriteFont _font;
    public UI()
    {
        Texture2D ui = Globals.Content.Load<Texture2D>("HUD");
        _font = Globals.Content.Load<SpriteFont>("TJEFont");

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
        var tjHP = new List<Rectangle>
        {
            new Rectangle(40, 109, 24, 3),
        };
        var eHP = new List<Rectangle>
        {
            new Rectangle(210, 109, 32, 3),
        };
        //var zero = new List<Rectangle> { new Rectangle(9, 186, 14, 14), };
        //var one = new List<Rectangle> { new Rectangle(35, 186, 11, 14), };
        //var two = new List<Rectangle> { new Rectangle(58, 186, 13, 14), };
        //var three = new List<Rectangle> { new Rectangle(82, 186, 12, 14), };
        //var four = new List<Rectangle> { new Rectangle(106, 186, 13, 14), };
        //var five = new List<Rectangle> { new Rectangle(130, 186, 13, 14), };
        //var six = new List<Rectangle> { new Rectangle(154, 186, 13, 14), };
        //var seven = new List<Rectangle> { new Rectangle(177, 186, 13, 14), };
        //var eight = new List<Rectangle> { new Rectangle(202, 186, 12, 14), };
        //var nine = new List<Rectangle> { new Rectangle(225, 186, 13, 14), };
        //var ten = new List<Rectangle>{ new Rectangle(251, 186, 20, 14), };

        _tjHud.AddAnimation("ToeJamHud", new Animation(ui, tjHud, 0.15f, new Vector2(3.2f, 3.2f)));
        _tjHP.AddAnimation("ToeJamHP", new Animation(ui, tjHP, 0.15f, new Vector2(3.2f, 3.2f)));
        _eHud.AddAnimation("EarlHud", new Animation(ui, eHud, 0.15f, new Vector2(3.2f, 3.2f)));
        _eHP.AddAnimation("EarlHP", new Animation(ui, eHP, 0.15f, new Vector2(3.2f, 3.2f)));
        _midSec.AddAnimation("MidSection", new Animation(ui, midSection, 0.15f, new Vector2(3.2f, 3.2f)));
        _vacay.AddAnimation("VactionHud", new Animation(ui, vactionHud, 0.15f, new Vector2(3.2f, 3.2f)));
        // Numbers for ship parts collected
        //_midNumbers.AddAnimation("0", new Animation(ui, zero, 0.15f, new Vector2(3.2f, 3.2f)));
        //_midNumbers.AddAnimation("1", new Animation(ui, one, 0.15f, new Vector2(3.2f, 3.2f)));
        //_midNumbers.AddAnimation("2", new Animation(ui, two, 0.15f, new Vector2(3.2f, 3.2f)));
        //_midNumbers.AddAnimation("3", new Animation(ui, three, 0.15f, new Vector2(3.2f, 3.2f)));
        //_midNumbers.AddAnimation("4", new Animation(ui, four, 0.15f, new Vector2(3.2f, 3.2f)));
        //_midNumbers.AddAnimation("5", new Animation(ui, five, 0.15f, new Vector2(3.2f, 3.2f)));
        //_midNumbers.AddAnimation("6", new Animation(ui, six, 0.15f, new Vector2(3.2f, 3.2f)));
        //_midNumbers.AddAnimation("7", new Animation(ui, seven, 0.15f, new Vector2(3.2f, 3.2f)));
        //_midNumbers.AddAnimation("8", new Animation(ui, eight, 0.15f, new Vector2(3.2f, 3.2f)));
        //_midNumbers.AddAnimation("9", new Animation(ui, nine, 0.15f, new Vector2(3.2f, 3.2f)));
        //_midNumbers.AddAnimation("10", new Animation(ui, ten, 0.15f, new Vector2(3.2f, 3.2f)));
    }
    public void SetShipPartsCollected(int amount)
    {
        _shipPartsCollected = amount;
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
            _tjHP.Update("ToeJamHP", gameTime);
        }
        else if (_player is Earl)
        {
            _eHud.Update("EarlHud", gameTime);
            _eHP.Update("EarlHP", gameTime);
        }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        _vacay.Draw(spriteBatch, _vactionHudPos);
        _midSec.Draw(spriteBatch, _midSectionPos);
        string text = _shipPartsCollected.ToString();
        Vector2 textPos = _midSectionPos + new Vector2(-12, -15);
        spriteBatch.DrawString(_font, text, textPos, Color.White);

        if (_player is ToeJam)
        {
            _tjHud.Draw(spriteBatch, _tjHudPos);
            _tjHP.Draw(spriteBatch, _tjHPPos);
        }
        else if (_player is Earl)
        {
            _eHud.Draw(spriteBatch, _eHudPos);
            _eHP.Draw(spriteBatch, _eHPPos);
        }    
    }
}