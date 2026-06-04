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
    private Texture2D _pixelTexture;
    private float _transitionAlpha;
    private GameState _previousState;
    
    public ViewGame(SpriteBatch spriteBatch, GameModel gameModel, ContentManager contentManager)
    {
        _spriteBatch = spriteBatch;
        _gameModel = gameModel;
        _menuView = new MenuView(_spriteBatch, _gameModel,  contentManager);
        _gamePlayView = new GamePlayView(_spriteBatch, _gameModel, contentManager);
        _currentView = _menuView;
    }

    public void Update(GameTime gameTime)
    {
        if (_transitionAlpha > 0)
        {
            _transitionAlpha -= (float)gameTime.ElapsedGameTime.TotalSeconds * 1.5f;
            if (_transitionAlpha < 0) _transitionAlpha = 0;
        }

        if (_previousState == GameState.Paused && _gameModel.State == GameState.Playing && _gameModel.Kitty.IsFirstLaunch)
            _transitionAlpha = 1f;

        _previousState = _gameModel.State;
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

        if (_transitionAlpha > 0)
        {
            var viewport = _spriteBatch.GraphicsDevice.Viewport;
            _spriteBatch.Draw(_pixelTexture, new Rectangle(0, 0, viewport.Width, viewport.Height), 
                Color.Black * _transitionAlpha);
        }
    }

    public void LoadContent()
    {
        _menuView.LoadContent();
        _gamePlayView.LoadContent();
        _pixelTexture = new Texture2D(_spriteBatch.GraphicsDevice, 1, 1);
        _pixelTexture.SetData(new[] { Color.White });
    }
}