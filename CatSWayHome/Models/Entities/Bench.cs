using Microsoft.Xna.Framework;

namespace CatSWayHome.Models;

public class Bench: BaseEntity
{
    public override int PositionGround { get; set; } = 617;
    public override bool IsSurface => true;

    public Bench(int x)
    {
        WorldPosition = new Vector2(x, PositionGround);
    }
}