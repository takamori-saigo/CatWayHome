using Microsoft.Xna.Framework;

namespace CatSWayHome.Models;

public class ExitDoor: BaseEntity
{
    public ExitDoor(int x)
    {
        WorldPosition = new Vector2(x, PositionGround);
    }

    public override int PositionGround { get; set; } = 520;
    public override string DialogMessage => "Я дома^^";
}