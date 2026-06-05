using Microsoft.Xna.Framework;

namespace CatSWayHome.Models;

public class Cucumber: BaseEntity
{
    public override int PositionGround { get; set; } = 798;
    
    public Cucumber(int x)
    {
        WorldPosition = new Vector2(x, PositionGround);
    }

    public override string DialogMessage => "Это что, ОГУРЕЦ?!";
}