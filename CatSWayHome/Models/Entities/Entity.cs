using Microsoft.Xna.Framework;

namespace CatSWayHome.Models;

public abstract class Entity
{
    public Vector2 Position { get; set; }
    public float ParallaxFactor { get; set; } = 1.8f;
    
    public abstract void Update(int deltaX);
}