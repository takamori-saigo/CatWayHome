using System;
using System.Threading.Tasks;
using CatSWayHome.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;


namespace CatSWayHome.Controllers;

public class MenuController: IController
{
    private GameModel _game;

    public MenuController(GameModel game)
    {
        _game = game;
    }
    public void Update()
    {
        var mouseState = Mouse.GetState();
        isHovering(mouseState.Position);
        isClicked(mouseState);
    }
    
    public void isHovering(Point positionOfCursor)
    {
        var buttonStart = _game.StartMenuButton;
        var buttonExit = _game.ExitMenuButton;
        var rectangleButtonStart = new Rectangle(buttonStart.Position.X, buttonStart.Position.Y, buttonStart.Width, buttonStart.Height);
        var rectangleButtonExit = new Rectangle(buttonExit.Position.X, buttonExit.Position.Y, buttonExit.Width, buttonExit.Height);
        buttonStart.IsHovered = rectangleButtonStart.Contains(positionOfCursor);
        buttonExit.IsHovered = rectangleButtonExit.Contains(positionOfCursor);
    }

    public async void isClicked(MouseState state)
    {
        var buttonStart = _game.StartMenuButton;
        var buttonExit = _game.ExitMenuButton;
        buttonExit.Clikced = buttonExit.IsHovered && state.LeftButton == ButtonState.Pressed;
        buttonStart.Clikced = buttonStart.IsHovered && state.LeftButton == ButtonState.Pressed;
        if (buttonStart.Clikced)
        {
            if (buttonStart.IsFirstClick)
                await Task.Delay(600);
            _game.State = GameState.Playing;
            buttonStart.IsFirstClick = false;
        }

        if (buttonExit.Clikced)
        {
            await Task.Delay(600);
            _game.State = GameState.Exit;
        }
    }
}