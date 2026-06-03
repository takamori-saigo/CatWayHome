using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CatSWayHome.View.GamePlayViewDrawing;

public class DebugClassHitbox
{
    public static void DrawHitBox(Vector2 position, SpriteBatch _spriteBatch, int width, int height)
    {
        var _pixelTexture = new Texture2D(_spriteBatch.GraphicsDevice, 1, 1);
        _pixelTexture.SetData(new[] { Color.White });
        _spriteBatch.Draw(_pixelTexture,position,new Rectangle(0,0,
                width, height),
            Color.LightPink * 0.5f);
    }
}