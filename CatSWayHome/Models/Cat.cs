using System.Numerics;

namespace CatSWayHome.Models;

public class Cat
{
    public Cat()
    {
        Health = 4;
        VelocityX = 2;
        /*
        Score = 0;
        */
        InitialPosition = new Vector2(650, 680);
    }

    public const float Gravity = 1800f;
    public const float JumpVelocity = -800f;     
    
    public int Health { get; set; }
    
    public Vector2 InitialPosition { get; private set; } 
    public int DeltaX { get; set; }
    public int VelocityX { get; private set; }

    public float DeltaY { get; set; }
    public float VelocityY { get; set; }
    
    public bool IsCalm { get; set; }
    public bool IsJump { get; set; }
    public bool IsMoving { get; set; }
    public bool IsGoingBack { get; set; }
    /*
    public int Score { get; private set; }
    */
    
    public float WidthTexture { get; set; }
    public float HeightTexture { get; set; }
}