using Microsoft.Xna.Framework;

namespace CatSWayHome.Models;

public class Rubbish: Entity
{
    
    private int YPositionOfGround = 690;
    public Vector2 WorldPosition{ get; set; }
    
    public Rubbish(int x)
    {
        WorldPosition = new Vector2(x, YPositionOfGround);
    }
    
    public override void Update(int deltaX)
    {
        Position = new Vector2(WorldPosition.X - deltaX * ParallaxFactor, YPositionOfGround);
    }
}