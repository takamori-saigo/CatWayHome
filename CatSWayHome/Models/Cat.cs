namespace CatSWayHome.Models;

public class Cat
{
    public Cat()
    {
        Health = 2;
        Score = 55;
    }
    
    public int Health { get; private set; }
    public int Score { get; private set; }
}