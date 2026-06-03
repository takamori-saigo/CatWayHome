using System.Collections.Generic;
using CatSWayHome.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace CatSWayHome.View.GamePlayViewDrawing;

public class DrawHeart: IDrawElement
{
    private SpriteBatch _spriteBatch;
    private ContentManager _content;
    private Cat _cat;
    private Texture2D _heart;
    private Texture2D _emptyHeart;
    
    public DrawHeart(SpriteBatch spriteBatch, Cat cat,
        ContentManager content)
    {
        _spriteBatch = spriteBatch;
        _cat = cat;
        _content = content;
    }
    
    public void Draw(SpriteBatch spriteBatch)
    {
        var aliveHearts = _cat.Health;
        var countBreak = 4 - aliveHearts;
        var position = Vector2.Zero;
        for (var i = 0; i < aliveHearts; i++)
        {
            _spriteBatch.Draw(_heart, position, null , Color.White, 0f, Vector2.Zero, 2f, SpriteEffects.None, 0f);
            position+=new Vector2(60, 0);
        }
        for (var i = 0; i < countBreak; i++)
        {
            _spriteBatch.Draw(_emptyHeart, position, null , Color.White, 0f, Vector2.Zero, 2f, SpriteEffects.None, 0f);
            position+=new Vector2(60, 0);
        }
    }
   
    public void LoadContent()
    {
        _heart = _content.Load<Texture2D>("heart");
        _emptyHeart =  _content.Load<Texture2D>("emptyHeart");
    }
}