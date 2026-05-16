using CatSWayHome.Models;

namespace CatSWayHome.Controllers;

public class GameController
{
    private GameModel _gameModel;
    private MenuController _menuController;
    private GamePlayController _gamePlayController;
    private IController _currentController;
    public GameController(GameModel model)
    {
        _gameModel = model;
        _menuController = new MenuController(_gameModel);
        _gamePlayController = new GamePlayController(_gameModel);
        _currentController = _menuController;
    }
    public void Update()
    {
        
        if (_gameModel.State == GameState.Paused)
            _currentController = _menuController;
        if (_gameModel.State == GameState.Playing)
            _currentController = _gamePlayController;
        _currentController.Update();
    }
}