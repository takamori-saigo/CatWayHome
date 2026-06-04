using System.Collections.Generic;
using System.Linq;
using CatSWayHome.Models.Buttons;
using Microsoft.Xna.Framework;


namespace CatSWayHome.Models;

public class GameModel
{
    public GameState State { get; set; }
    public float WinAlpha { get; set; }
    public Cat Kitty { get; private set; }
    public MenuButton StartMenuButton { get; private set; }
    public MenuButton ExitMenuButton { get; private set; }
    public List<BaseEntity> Entities { get; private set; }
    
    public GameModel()
    {
        State = GameState.Playing;
        InitializeEntities();
    }

    private void InitializeButtons()
    {
        StartMenuButton = new MenuButton();
        ExitMenuButton = new MenuButton();
    }

    private void InitializeEntities()
    {
        InitializeButtons();
        Kitty = new Cat();
        Entities = new List<BaseEntity>();
        InitializeStaticEntities();
    }

    private void InitializeStaticEntities()
    {
        var positionsRubish = new[] { 3000}; 
        var positionsCucumbers = new[] { 4200, 9200};
        var positionsBenches = new[] { 5100 };
        var positionsOfCondei = new[] { 1850, 9000 };
        var positionsOfFishes = new[] { 3700, 4800, 6000 };
        var positionsOfLuk = new[] { 2000, 6850 };
        var positionsSkotch = new[] { 5600 };
        var positionsDog = new[] { 5600,  18500};
        var triggerDog = 300;
        var positionExitDoor = 11500;
        Entities.AddRange(positionsRubish.Select(x => new Rubbish(x)));
        Entities.AddRange(positionsCucumbers.Select(x => new Cucumber(x)));
        Entities.AddRange(positionsBenches.Select(x => new Bench(x)));
        Entities.AddRange(positionsOfCondei.Select(x => new Candei(x)));
        Entities.AddRange(positionsOfFishes.Select(x => new Fish(x)));
        Entities.AddRange(positionsOfLuk.Select(x => new Luk(x)));
        Entities.AddRange(positionsSkotch.Select(x => new Skotch(x)));
        Entities.Add(new Car(10000));
        Entities.AddRange(positionsDog.Select(x => new Dog(x, triggerDog)));
        Entities.Add(new Barrel(6720));
        Entities.Add(new ExitDoor(positionExitDoor));
    }
    
    private void InitializeCoins()
    {
        /*var positions = new Vector2[4] { new Vector2(50, 70), new Vector2(50, 140), new Vector2(50, 200), new Vector2(50, 300) };
        coins = new Coin[positions.Length];
        for (int i = 0; i < positions.Length; i++)
        {
            coins[i] = new Coin(positions[i], false);
        }*/
    }
}