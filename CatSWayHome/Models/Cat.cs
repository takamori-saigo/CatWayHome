namespace CatSWayHome.Models;

public class Cat
{
    public Cat()
    {
        Health = 2;
        Score = 55 ;
        IsJump = true;
    }
    
    public int Health { get; private set; }
    public int Score { get; private set; }
    
    public bool IsCalm { get; set; }
    public bool IsJump { get; set; }
    public bool IsMoving { get; set; }
}