using Microsoft.Xna.Framework;

namespace CatSWayHome.Models;

public class Cucumber: BaseEntity
{
    public override int PositionGround { get; set; } = 795;
    
    public Cucumber(int x)
    {
        WorldPosition = new Vector2(x, PositionGround);
    }

    public override string DialogMessage => "Cucumber yuck";
}