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
                    scale = 0.37f;
                    _spriteBatch.Draw(_rubbish, rub.Position, null, Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
                    break;
                }
                case Cucumber:
                    var cuc = e;
                    scale = 0.07f;
                    
                    if (cuc.HeightTexture == 0)
                    {
                        cuc.HeightTexture = _cucumber.Height * scale ;
                        cuc.WidthTexture = _cucumber.Width * scale ;
                    }
                        
                    /*
                    DebugClassHitbox.DrawHitBox(cuc.Position, _spriteBatch, (int)cuc.WidthTexture, (int)cuc.HeightTexture);
                    */

                    _spriteBatch.Draw(_cucumber, cuc.Position, null, Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
                    break;
                case Bench:
                    var bench = e;
                    scale = 0.4f;
                    if (bench.HeightTexture == 0)
                    {
                        bench.HeightTexture = _bench.Height * scale ;
                        bench.WidthTexture = _bench.Width * scale ;
                    }
                    DebugClassHitbox.DrawHitBox(bench.Position, _spriteBatch, (int)bench.WidthTexture, (int)bench.HeightTexture);
                    _spriteBatch.Draw(_bench, bench.Position, null, Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
                    break; 
            }
        }
    }

    public void LoadContent()
    {
        _rubbish = _content.Load<Texture2D>("rubish");
        _cucumber = _content.Load<Texture2D>("Cucomber");
        _bench = _content.Load<Texture2D>("Bench");
    }
}