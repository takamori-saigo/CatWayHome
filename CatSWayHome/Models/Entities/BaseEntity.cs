using Microsoft.Xna.Framework;

namespace CatSWayHome.Models;

public abstract class BaseEntity
{
    public Vector2 Position { get; set; }
    public virtual int PositionGround { get; set; } = 690;
    public Vector2 WorldPosition{ get; set; }
    public float ParallaxFactor { get; set; } = 1.8f;
    public float WidthTexture { get; set; }
    public float HeightTexture { get; set; }
    public void Update(int deltaX)
    {
        Position = new Vector2(WorldPosition.X - deltaX * ParallaxFactor, PositionGround);
    }
}