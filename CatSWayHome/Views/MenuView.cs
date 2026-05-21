using System;
using System.Threading.Tasks;
using CatSWayHome.Models;
using CatSWayHome.Models.Buttons;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;


namespace CatSWayHome.View;

public class MenuView: IViewGame
{
    private GameModel _gameModel;
    private Texture2D _background;
    private Texture2D _calmButton;
    private Texture2D _hoveringButton;
    private Texture2D _clickedButton;
    private Texture2D _cursorTexture;
    private SpriteBatch _spriteBatch;
    private ContentManager _content;
    private SpriteFont _font;
    private int _screenWidth;
    private int _screenHeight;
    
    public MenuView(SpriteBatch spriteBatch, GameModel gameModel, ContentManager content)
    {
        _spriteBatch = spriteBatch;
        _gameModel = gameModel;
        _content = content;
        _screenWidth = _spriteBatch.GraphicsDevice.Viewport.Width;
        _screenHeight = _spriteBatch.GraphicsDevice.Viewport.Height;
        LoadContent();
        InitializeButtons();
    }

    private void LoadContent()
    {
        _calmButton = _content.Load<Texture2D>("CalmButton");
        _hoveringButton = _content.Load<Texture2D>("HoveringMenuButton");
        _clickedButton = _content.Load<Texture2D>("PressedMenuButton");
        _background = _content.Load<Texture2D>("menu_background");
        _cursorTexture = _content.Load<Texture2D>("Cursor");
        _font = _content.Load<SpriteFont>("Font");
    }
    
    public void InitializeButtons()
    {
        // TODO: Make Udaptive Buttons
        var startButton = _gameModel.StartMenuButton;
        startButton.Text = "START";
        startButton.Position = new Point(_screenWidth / 2 - 200, _screenHeight / 2 - 60);
        startButton.Width = 400;
        startButton.Height = 120;

        var exitButton = _gameModel.ExitMenuButton;
        exitButton.Text = "EXIT";
        exitButton.Position = new Point(_screenWidth / 2 - 200, _screenHeight / 2 - 60 + 150);
        exitButton.Width = 400;
        exitButton.Height = 120;
    }
    
    public void Draw()
    {
        var rectForBackGround = new Rectangle(0, 0, _screenWidth, _screenHeight);
        _spriteBatch.Draw(_background, rectForBackGround, Color.White);
        
        
        DrawButton(_gameModel.StartMenuButton);
        DrawButton(_gameModel.ExitMenuButton);
        
        DrawButtonText(_gameModel.StartMenuButton, "CONTINUE");
        DrawButtonText(_gameModel.ExitMenuButton, "EXIT");

        DrawCursor();
    }

    public void DrawCursor()
    {
        var mouseState = Mouse.GetState();
        var cursorPosition = new Vector2(mouseState.X, mouseState.Y);
        var cursorScale = 0.08f;
        _spriteBatch.Draw(_cursorTexture, cursorPosition, null, Color.White, 
            0f, Vector2.Zero, cursorScale, SpriteEffects.None, 0f);
    }
    
    
    public void DrawButton(MenuButton menuButton)
    {
        if (!menuButton.Clikced)
        {
            if (menuButton.IsHovered)
                _spriteBatch.Draw(_hoveringButton, new Rectangle(menuButton.Position.X, menuButton.Position.Y, menuButton.Width, menuButton.Height),  Color.White);
            else _spriteBatch.Draw(_calmButton, new Rectangle(menuButton.Position.X, menuButton.Position.Y, menuButton.Width, menuButton.Height),  Color.White);

        }
        else
        {
            _spriteBatch.Draw(_clickedButton,
                new Rectangle(menuButton.Position.X, menuButton.Position.Y, menuButton.Width, menuButton.Height),
                Color.White);
        }
    }
    
    public void DrawButtonText(MenuButton menuButton, string textWhenClicled)
    {
        var text = menuButton.IsFirstClick ? menuButton.Text : textWhenClicled;
        var textSize = _font.MeasureString(text);
    
        var scaleX = menuButton.Width / textSize.X;
        var scaleY = menuButton.Height / textSize.Y;
        var scale = Math.Min(scaleX, scaleY) - 0.3f; 
    
        var textPosition = new Vector2(
            menuButton.Position.X + (menuButton.Width - textSize.X * scale) / 2,
            menuButton.Position.Y + (menuButton.Height - textSize.Y * scale) / 2
        );
    
        _spriteBatch.DrawString(_font, text, textPosition, Color.Black, 
            0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
    }
}