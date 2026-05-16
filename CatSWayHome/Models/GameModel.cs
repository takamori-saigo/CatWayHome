using CatSWayHome.View.Buttons;
using Microsoft.Xna.Framework;


namespace CatSWayHome.Models;

public class GameModel
{
    public GameState State { get; set; }
    public  MenuButton StartMenuButton {get; private set;}
    public Game CatGame { get; private set; }

    public GameModel(Game game)
    {
        CatGame = game;
        State = GameState.Paused;
        StartMenuButton = new MenuButton(new Point(CatGame.GraphicsDevice.Viewport.Width / 2 - 200, 
            CatGame.GraphicsDevice.Viewport.Height / 2 - 60), "START", 400, 120);
    }
}