using Microsoft.Xna.Framework;

namespace CatSWayHome.Models;

public class Fish: BaseEntity
{
    public override int PositionGround { get; set; } = 790;

    public bool Take { get; set; }
    public Fish(int x)
    {
        WorldPosition = new Vector2(x, PositionGround);
    }

    public override string DialogMessage => "FISSSSH";
}