using CatSWayHome.Models;
using CatSWayHome.View;
using Microsoft.Xna.Framework.Input;

namespace CatSWayHome.Controllers;

public class GamePlayController: IController
{
    private GameModel _game;
    public GamePlayController(GameModel gameModel)
    {
        _game = gameModel;
    }
    public void Update()
    {
        var keyBoard = Keyboard.GetState();
        if (keyBoard.IsKeyDown(Keys.Escape))
            _game.State = GameState.Paused;
        
    }
}