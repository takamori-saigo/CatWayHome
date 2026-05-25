using Microsoft.Xna.Framework;


namespace CatSWayHome.Models.Buttons;

public class MenuButton
{
    public Point Position { get; set; }
    public string Text { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }
    public bool IsHovered { get; set; }
    public bool Clikced { get; set; }
    public bool IsFirstClick { get; set; }
    public MenuButton()
    {
        IsFirstClick = true;
    }
}