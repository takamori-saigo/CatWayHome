using Microsoft.Xna.Framework;

namespace CatSWayHome.Models;

public class Candei: BaseEntity
{
    public override int PositionGround { get; set; } = 800;
    
    public override bool IsSurface => true;

    public Candei(int x)
    {
        WorldPosition = new Vector2(x, PositionGround);
    }
    
    public override string DialogMessage => "Candei!!!!!!!!!!!!!!";
}