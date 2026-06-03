using Microsoft.Xna.Framework.Graphics;

namespace CatSWayHome.View;

public interface IDrawElement
{
    void Draw(SpriteBatch spriteBatch);
    void LoadContent();
}