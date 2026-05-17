using CatSWayHome.View.Buttons;
using Microsoft.Xna.Framework;


namespace CatSWayHome.Models;

public class GameModel
{
    public GameState State { get; set; }
    public  MenuButton StartMenuButton {get; private set;}
    public  MenuButton ExitMenuButton {get; private set;}
    public Game CatGame { get; private set; }
    public Coin[] coins { get; private set; }
    
    public Cat Kitty { get; private set; }

    public GameModel(Game game)
    {
        CatGame = game;
        State = GameState.Playing;
        Kitty = new Cat();
        InitializeButtons();
        InitializeCoins();
    }

    public void InitializeButtons()
    {
        StartMenuButton = new MenuButton(new Point(CatGame.GraphicsDevice.Viewport.Width / 2 - 200, 
            CatGame.GraphicsDevice.Viewport.Height / 2 - 60), "START", 400, 120);
        ExitMenuButton = new MenuButton(new Point(CatGame.GraphicsDevice.Viewport.Width / 2 - 200, 
            CatGame.GraphicsDevice.Viewport.Height / 2 - 60 + 150) , "EXIT", 400, 120);
    }

    public void InitializeCoins()
    {
        var positions = new Vector2[4] { new Vector2(50, 70), new Vector2(50, 140), new Vector2(50, 200), new Vector2(50, 300) };
        coins = new Coin[positions.Length];
        for (int i = 0; i < positions.Length; i++)
        {
            coins[i] = new Coin(positions[i], false);
        }
    }
}