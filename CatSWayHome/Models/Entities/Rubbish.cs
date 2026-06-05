using Microsoft.Xna.Framework;

namespace CatSWayHome.Models;

public class Rubbish: BaseEntity
{
    public Rubbish(int x)
    {
        WorldPosition = new Vector2(x, PositionGround);
    }

    public override int PositionGround { get; set; } = 670;
    public override string DialogMessage => "Кажется тут можно спрятаться";
}