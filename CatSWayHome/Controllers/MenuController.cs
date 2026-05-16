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
        var button = _game.StartMenuButton;
        var rectangle = new Rectangle(button.Position.X, button.Position.Y, button.Width, button.Height);
        button.IsHovered = rectangle.Contains(positionOfCursor);
    }

    public async void isClicked(MouseState state)
    {
        var button = _game.StartMenuButton;
        button.Clikced = button.IsHovered && state.LeftButton == ButtonState.Pressed;
        if (button.Clikced)
        {
            if (button.IsFirstClick)
                await Task.Delay(1000);
            _game.State = GameState.Playing;
            button.IsFirstClick = false;
        }
    }
}