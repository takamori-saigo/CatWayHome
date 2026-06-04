using Microsoft.Xna.Framework;

namespace CatSWayHome.Models;

public class Candei: BaseEntity
{
    public override int PositionGround { get; set; } = 615;
    public override float HeightTexture { get => _heaightTexture / 2; set => _heaightTexture = value; }
    private float _heaightTexture { get; set; }
    public override bool IsSurface => true;

    public Candei(int x)
    {
        WorldPosition = new Vector2(x, PositionGround);
    }
}