namespace CatSWayHome.Models;

public class Cat
{
    public Cat()
    {
        Health = 4;
        Score = 0;
    }
    
    public int Health { get; private set; }
    public int Score { get; private set; }
}