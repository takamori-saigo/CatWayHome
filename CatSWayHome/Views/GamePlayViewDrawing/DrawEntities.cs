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
    private Texture2D _luk;
    
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
            switch (e)
            {
                case Rubbish:
                {
                    DrawCurrentEntity(e as Rubbish, 0.45f, _rubbish);
                    break;
                }
                case Cucumber:
                    DrawCurrentEntity(e as Cucumber, 0.07f, _cucumber);
                    break;
                case Bench:
                    DrawCurrentEntity(e as Bench,  0.4f, _bench);
                    break;
                case Candei:
                    DrawCurrentEntity(e as Candei,  0.15f, _candei);
                    break;
                case Fish:
                    var fish = e as Fish;
                    if (!fish.Take)
                        DrawCurrentEntity(fish, 0.07f, _fish);
                    break;
                case Luk:
                    DrawCurrentEntity(e as Luk, 0.25f, _luk);
                    break;
            }
        }
        
    }

    private void DrawCurrentEntity(BaseEntity entity, float scale, Texture2D texture)
    {

        if (entity.HeightTexture == 0)
        {
            entity.HeightTexture = texture.Height * scale;
            entity.WidthTexture = texture.Width * scale;
        }

        DebugClassHitbox.DrawHitBox(entity.Position, _spriteBatch, (int)entity.WidthTexture,
            (int)entity.HeightTexture);
        _spriteBatch.Draw(texture, entity.Position, null, Color.White, 0f, Vector2.Zero, scale,
            SpriteEffects.None, 0f);
    }
    
    public void LoadContent()
    {
        _rubbish = _content.Load<Texture2D>("Entities/rubish");
        _cucumber = _content.Load<Texture2D>("Entities/Cucomber");
        _bench = _content.Load<Texture2D>("Entities/Bench");
        _candei = _content.Load<Texture2D>("Entities/conditioner");
        _fish = _content.Load<Texture2D>("Entities/fish");
        _luk = _content.Load<Texture2D>("Entities/luk");
    }
}