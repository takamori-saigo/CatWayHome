using Microsoft.Xna.Framework;

namespace CatSWayHome.Models;

public class Luk: BaseEntity
{
    public override int PositionGround { get; set; } = 795;

    public Luk(int x)
    {
        WorldPosition = new Vector2(x, PositionGround);
    }

    public override string DialogMessage => "Luk skywaker";
    
}