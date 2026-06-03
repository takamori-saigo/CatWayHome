using System;
using System.Collections.Generic;
using CatSWayHome.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace CatSWayHome.View.GamePlayViewDrawing;

public class DrawEntities: IDrawElement
{
    private SpriteBatch _spriteBatch;
    private ContentManager _content;
    private List<BaseEntity> _entities;
    private Texture2D _rubbish;
    private Texture2D _cucumber;
    private Texture2D _bench;
    private Texture2D _candei;
    private Texture2D _fish;
    
    public DrawEntities(SpriteBatch spriteBatch, List<BaseEntity> entity,
        ContentManager content)
    {
        _spriteBatch = spriteBatch;
        _content = content;
        _entities = entity;
    }
    
    public void Draw(SpriteBatch spriteBatch)
    {
        foreach (var e in _entities)
        {
            var scale = 1f;
            switch (e)
            {
                case Rubbish:
                {
                    var rub = e;
                    scale = 0.45f;
                    /*if (rub.HeightTexture == 0)
                    {
                        rub.HeightTexture = _rubbish.Height * scale;
                        rub.WidthTexture = _rubbish.Width * scale ;
                        rub.HitBoxPoisitionY = 100;
                    }*/
                    var temp = new Vector2(rub.Position.X, rub.Position.Y - rub.HitBoxPoisitionY);
                    DebugClassHitbox.DrawHitBox(temp, _spriteBatch, (int)rub.WidthTexture,
                        (int)rub.HeightTexture + rub.HitBoxPoisitionY);
                    _spriteBatch.Draw(_rubbish, rub.Position, null, Color.White, 0f, Vector2.Zero, scale,
                        SpriteEffects.None, 0f);
                    break;
                }
                case Cucumber:
                    var cuc = e;
                    scale = 0.07f;

                    if (cuc.HeightTexture == 0)
                    {
                        cuc.HeightTexture = _cucumber.Height * scale;
                        cuc.WidthTexture = _cucumber.Width * scale;
                    }

                    /*
                    DebugClassHitbox.DrawHitBox(cuc.Position, _spriteBatch, (int)cuc.WidthTexture, (int)cuc.HeightTexture);
                    */

                    _spriteBatch.Draw(_cucumber, cuc.Position, null, Color.White, 0f, Vector2.Zero, scale,
                        SpriteEffects.None, 0f);
                    break;
                case Bench:
                    var bench = e;
                    scale = 0.4f;
                    if (bench.HeightTexture == 0)
                    {
                        bench.HeightTexture = _bench.Height * scale;
                        bench.WidthTexture = _bench.Width * scale;
                    }

                    DebugClassHitbox.DrawHitBox(bench.Position, _spriteBatch, (int)bench.WidthTexture,
                        (int)bench.HeightTexture);
                    _spriteBatch.Draw(_bench, bench.Position, null, Color.White, 0f, Vector2.Zero, scale,
                        SpriteEffects.None, 0f);
                    break;
                case Candei:
                    var candei = e;
                    scale = 0.4f;
                    if (candei.HeightTexture == 0)
                    {
                        candei.HeightTexture = _candei.Height * scale;
                        candei.WidthTexture = _candei.Width * scale;
                    }

                    DebugClassHitbox.DrawHitBox(candei.Position, _spriteBatch, (int)candei.WidthTexture,
                        (int)candei.HeightTexture);
                    _spriteBatch.Draw(_candei, candei.Position, null, Color.White, 0f, Vector2.Zero, scale,
                        SpriteEffects.None, 0f);
                    break;
                case Fish:
                    var fish = e as Fish;
                    scale = 0.07f;
                    if (fish.HeightTexture == 0)
                    {
                        fish.HeightTexture = _candei.Height * scale;
                        fish.WidthTexture = _candei.Width * scale;
                    }
                    
                    if (!fish.Take)
                    {
                        DebugClassHitbox.DrawHitBox(fish.Position, _spriteBatch, (int)fish.WidthTexture,
                            (int)fish.HeightTexture);
                        _spriteBatch.Draw(_fish, fish.Position, null, Color.White, 0f, Vector2.Zero, scale,
                            SpriteEffects.None, 0f);
                    }
                    break;
            }
        }
        
    }
    
    public void LoadContent()
    {
        _rubbish = _content.Load<Texture2D>("Entities/rubish");
        _cucumber = _content.Load<Texture2D>("Entities/Cucomber");
        _bench = _content.Load<Texture2D>("Entities/Bench");
        _candei = _content.Load<Texture2D>("Entities/condei");
        _fish = _content.Load<Texture2D>("Entities/fish");
    }
}