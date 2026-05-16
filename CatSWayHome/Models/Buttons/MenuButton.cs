using Microsoft.Xna.Framework;


namespace CatSWayHome.View.Buttons;

public class MenuButton
{
    public Point Position { get; private set; }
    public string Text { get; private set; }
    public int Width { get; private set; }
    public int Height { get; private set; }
    public bool IsHovered { get; set; }
    public bool Clikced { get; set; }
    public bool IsFirstClick { get; set; }
    public MenuButton(Point point, string text, int width, int height)
    {
        IsFirstClick = true;
        Position = point;
        Text = text;
        Width = width;
        Height = height;
    }
}