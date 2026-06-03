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

    public const float Gravity = 1300f;
    public const float JumpVelocity = -800f;     
    
    public int Health { get; set; }
    
    public Vector2 InitialPosition { get; set; } 
    public int DeltaX { get; set; }
    public int VelocityX { get; set; }

    public float DeltaY { get; set; }
    public float VelocityY { get; set; }
    
    public bool IsCalm { get; set; }
    public bool IsJump { get; set; }
    public bool IsMoving { get; set; }
    public bool IsGoingBack { get; set; }
    public bool IsKnockback { get; set; }
    public bool CatWasMoving { get; set; }
    public bool CatWasJumping { get; set; }
    public bool OnTheObject { get; set; }

    public bool isFirstStart { get; set; }
    
    public bool ShowDialog { get; set; }
    public float DialogTimeLeft { get; set; }
    public string DialogText { get; set; }
    public bool IsFirstLaunch { get; set; } = true;
    public int DialogCharIndex { get; set; }
    public float DialogCharTimer { get; set; }
    /*
    public int Score { get; private set; }
    */
    
    public float WidthTexture { get; set; }
    public float HeightTexture { get; set; }
}