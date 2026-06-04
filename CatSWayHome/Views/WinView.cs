using CatSWayHome.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace CatSWayHome.View;

public class WinView: IViewGame
{
    private SpriteBatch _spriteBatch;
    private Texture2D _wonBackground;
    private GameModel _gameModel;

    public WinView(SpriteBatch spriteBatch, GameModel gameModel, ContentManager content)
    {
        _spriteBatch = spriteBatch;
        _gameModel = gameModel;
        _wonBackground = content.Load<Texture2D>("background/WonBackground");
    }

    public void Draw()
    {
        var viewport = _spriteBatch.GraphicsDevice.Viewport;
        var rect = new Rectangle(0, 0, viewport.Width, viewport.Height);
        _spriteBatch.Draw(_wonBackground, rect, Color.White * _gameModel.WinAlpha);
    }
}
