using Microsoft.Xna.Framework;

namespace CatSWayHome.Models;

public class Luk: BaseEntity
{
    public override int PositionGround { get; set; } = 810;

    public Luk(int x)
    {
        WorldPosition = new Vector2(x, PositionGround);
    }

    public override string DialogMessage => "В крайний раз, когда я провалился в эту неизвестную пещеру, было больно...";
    
}