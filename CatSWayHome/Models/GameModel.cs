using CatSWayHome.Models.Buttons;
using Microsoft.Xna.Framework;


namespace CatSWayHome.Models;

public class GameModel
{
    public GameState State { get; set; }
    public Coin[] coins { get; private set; }
    
    public Cat Kitty { get; private set; }
    public MenuButton StartMenuButton { get; private set; }
    public MenuButton ExitMenuButton { get; private set; }
    public GameModel()
    {
        State = GameState.Paused;
        Kitty = new Cat();
        InitializeCoins();
        InitializeButtons();
    }

    public void InitializeButtons()
    {
        StartMenuButton = new MenuButton();
        ExitMenuButton = new MenuButton();
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