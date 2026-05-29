using Microsoft.Xna.Framework;

namespace CatSWayHome.Models;

public class Cucumber: Entity
{
    private int YPositionOfGround = 795;
    public Vector2 WorldPosition{ get; set; }
    public Cucumber(int x)
    {
        WorldPosition = new Vector2(x, YPositionOfGround);
    }

    public override void Update(int deltaX)
    {
        Position = new Vector2(WorldPosition.X - deltaX * ParallaxFactor, YPositionOfGround);
    }
}