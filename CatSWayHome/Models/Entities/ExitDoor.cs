using Microsoft.Xna.Framework;

namespace CatSWayHome.Models;

public class ExitDoor: BaseEntity
{
    public ExitDoor(int x)
    {
        WorldPosition = new Vector2(x, PositionGround);
    }

    public override int PositionGround { get; set; } = 390;
    public override string DialogMessage => "Trash is close";
}