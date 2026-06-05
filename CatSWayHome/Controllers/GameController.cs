using System.Linq;
using CatSWayHome.Models;
using Microsoft.Xna.Framework;

namespace CatSWayHome.Controllers;

public class GameController
{
    private GameModel _gameModel;
    private MenuController _menuController;
    private GamePlayController _gamePlayController;
    private WinController _winController;
    private IController _currentController;
    
    public GameController(GameModel model)
    {
        _gameModel = model;
        _menuController = new MenuController(_gameModel);
        _gamePlayController = new GamePlayController(_gameModel);
        _winController = new WinController(_gameModel);
        _currentController = _menuController;
    }
    
    public void Update(GameTime gameTime)
    {
        
        if (_gameModel.State == GameState.Paused)
            _currentController = _menuController;
        if (_gameModel.State == GameState.Playing)
            _currentController = _gamePlayController;
        if (_gameModel.State == GameState.Lost)
            ResetGame();
        if (_gameModel.State == GameState.Won)
            _currentController = _winController;
        _currentController.Update(gameTime);
    }

    private void ResetGame()
    {
        var _cat = _gameModel.Kitty;
        _cat.DeltaX = 0;
        _cat.DeltaY = 0;
        _cat.Health = 4;
        _cat.IsFirstLaunch = true;
        _cat.IsMoving = false;
        _cat.IsJump = false;
        _cat.VelocityY = 0;
        _cat.IsKnockback = false;
        _cat.JustGotHit = false;
        _gameModel.StartMenuButton.IsFirstClick = true;
        _gameModel.StartMenuButton.Text = "НАЧАТЬ ЗАНОВО";
        _cat.TriggeredDialogTypes.Clear();
        foreach (var e in _gameModel.Entities)
        {
            if (e is Dog dog)
            {
                dog.IsRunning = false;
                dog.JustBarked = false;
                dog.HasRun = false;
                dog.WorldPosition = new Vector2(dog.SpawnX, dog.PositionGround);
                dog.RunWorldPosX = dog.SpawnX;
            }
        }
        _gameModel.State = GameState.Paused;
    }
}