using CatSWayHome.Controllers;
using CatSWayHome.Models;
using Microsoft.Xna.Framework.Graphics;

namespace CatSWayHome.View;

public class GamePlayView: IViewGame
{
    private GameModel _gameModel;
    private SpriteBatch _spriteBatch;
    public GamePlayView(SpriteBatch spriteBatch, GameModel gameModel)
    {
        _spriteBatch = spriteBatch;
        _gameModel = gameModel;
    }
    public void Draw()
    {
        
    }
}