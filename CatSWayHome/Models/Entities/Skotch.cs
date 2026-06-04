using Microsoft.Xna.Framework;

namespace CatSWayHome.Models;

public class Skotch: BaseEntity
{
    public override int PositionGround { get; set; } = 790;
    
    public Skotch(int x)
    {
        WorldPosition = new Vector2(x, PositionGround);
    }

    public override string DialogMessage => "SKKOOOOTCH";
}