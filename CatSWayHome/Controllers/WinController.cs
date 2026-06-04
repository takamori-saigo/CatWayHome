using CatSWayHome.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace CatSWayHome.Controllers;

public class WinController: IController
{
    private GameModel _gameModel;
    private const float FadeSpeed = 0.5f;

    public WinController(GameModel gameModel)
    {
        _gameModel = gameModel;
        _gameModel.WinAlpha = 0f;
    }

    public void Update(GameTime gameTime)
    {
        if (_gameModel.WinAlpha < 1f)
        {
            _gameModel.WinAlpha += (float)gameTime.ElapsedGameTime.TotalSeconds * FadeSpeed;
            if (_gameModel.WinAlpha > 1f)
                _gameModel.WinAlpha = 1f;
        }
        else
        {
            var keyboard = Keyboard.GetState();
            var mouse = Mouse.GetState();

            if (keyboard.GetPressedKeys().Length > 0 ||
                mouse.LeftButton == ButtonState.Pressed ||
                mouse.RightButton == ButtonState.Pressed)
            {
                _gameModel.State = GameState.Exit;
            }
        }
    }
}
