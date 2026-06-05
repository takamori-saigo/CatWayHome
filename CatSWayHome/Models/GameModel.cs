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
        State = GameState.Paused;
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
        var positionsBenches = new[] { 5130 };
        var positionsOfCondei = new[] { 1850, 9000 };
        var positionsOfFishes = new[] { 3700, 4800, 6400, 7400, 11200 };
        var positionsOfLuk = new[] { 2000, 6850 };
        var positionsSkotch = new[] { 5600, 8000 };
        var positionsDog = new[] { 4700, 11900};
        var triggerDog = 1000f;
        var positionExitDoor = 11900;
        var positionOfCar = 10000;
        var positionOfBarrel = 6720;
        Entities.AddRange(positionsRubish.Select(x => new Rubbish(x)));
        Entities.AddRange(positionsCucumbers.Select(x => new Cucumber(x)));
        Entities.AddRange(positionsBenches.Select(x => new Bench(x)));
        Entities.AddRange(positionsOfCondei.Select(x => new Candei(x)));
        Entities.AddRange(positionsOfFishes.Select(x => new Fish(x)));
        Entities.AddRange(positionsOfLuk.Select(x => new Luk(x)));
        Entities.AddRange(positionsSkotch.Select(x => new Skotch(x)));
        Entities.Add(new Car(positionOfCar));
        Entities.AddRange(positionsDog.Select(x => new Dog(x, triggerDog)));
        Entities.Add(new Barrel(positionOfBarrel));
        Entities.Add(new ExitDoor(positionExitDoor));
    }
}