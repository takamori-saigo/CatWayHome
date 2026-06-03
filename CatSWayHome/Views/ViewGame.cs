using Microsoft.Xna.Framework.Graphics;
using CatSWayHome.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace CatSWayHome.View;

public class ViewGame
{
    private GameModel _gameModel;
    private IViewGame _currentView; 
    private SpriteBatch _spriteBatch;
    private MenuView _menuView;
    private GamePlayView _gamePlayView;
    
    public ViewGame(SpriteBatch spriteBatch, GameModel gameModel, ContentManager contentManager)
    {
        _spriteBatch = spriteBatch;
        _gameModel = gameModel;
        _menuView = new MenuView(_spriteBatch, _gameModel,  contentManager);
        _gamePlayView = new GamePlayView(_spriteBatch, _gameModel, contentManager);
        _currentView = _menuView;
    }
    
    public void Draw()
    {
        if (_gameModel.State == GameState.Paused)
        {
            _currentView = _menuView;
            _menuView.PlayMusic();
        }
        if (_gameModel.State == GameState.Playing)
        {
            _currentView = _gamePlayView;
            _menuView.StopMusic();
        }
        
        _currentView.Draw();
    }

    public void LoadContent()
    {
        _menuView.LoadContent();
        _gamePlayView.LoadContent();

    }
}