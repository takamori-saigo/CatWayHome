using Microsoft.Xna.Framework;

namespace CatSWayHome.Models;

public class Coin
{
    public Vector2 Position { get; private set; }
    public bool IsTook { get; set; }

    public Coin(Vector2 position, bool isTook)
    {
        Position = position;
        IsTook = isTook;
    }
}