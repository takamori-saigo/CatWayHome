using System;
using CatSWayHome.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace CatSWayHome.View.GamePlayViewDrawing;

public class DrawMap: IDrawElement
{
    private Texture2D _background;
    private GameModel _gameModel;
    private ContentManager _content;
    
    public DrawMap(GameModel gameModel, ContentManager contentManager)
    {
        _gameModel = gameModel;
        _content = contentManager;
    }
    
    public void Draw(SpriteBatch spriteBatch)
    {
        var viewport = spriteBatch.GraphicsDevice.Viewport;
        var bgWidth = _background.Width;
        var sourceWidth = 900;
        var scrollX = (_gameModel.Kitty.DeltaX % bgWidth + bgWidth) % bgWidth;
        var scale = (float)viewport.Width / sourceWidth;
        var firstWidth = Math.Min(sourceWidth, bgWidth-scrollX);
        var source1 = new Rectangle(scrollX, 800, firstWidth, 800);
        spriteBatch.Draw(_background, Vector2.Zero, source1, 
            Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
            
            
        if (firstWidth < sourceWidth)
        {
            var secondWidth = sourceWidth - firstWidth;
            var source2 = new Rectangle(0, 800, secondWidth, 800);
            var pos2 = new Vector2(firstWidth * scale, 0);
            spriteBatch.Draw(_background, pos2, source2, Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
        } 
    }

    public void LoadContent()
    {
        _background = _content.Load<Texture2D>("background/new_background");
    }
}