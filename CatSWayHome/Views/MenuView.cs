using System;
using CatSWayHome.Models;
using CatSWayHome.Models.Buttons;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;   



namespace CatSWayHome.View;

public class MenuView: IViewGame
{
    private GameModel _gameModel;
    private Texture2D _background;
    private Texture2D _calmButton;
    private Texture2D _hoveringButton;
    private Texture2D _clickedButton;
    private Texture2D _cursorTexture;
    private SoundEffect _menuSound;
    private SoundEffectInstance _menuSoundInstance;
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

    public void LoadContent()
    {
        _calmButton = _content.Load<Texture2D>("buttoms/CalmButton");
        _hoveringButton = _content.Load<Texture2D>("buttoms/HoveringMenuButton");
        _clickedButton = _content.Load<Texture2D>("buttoms/PressedMenuButton");
        _background = _content.Load<Texture2D>("background/menu_background");
        _cursorTexture = _content.Load<Texture2D>("background/Cursor");
        _font = _content.Load<SpriteFont>("background/Font");
        _menuSound = _content.Load<SoundEffect>("buttoms/menu_Sound");
        _menuSoundInstance = _menuSound.CreateInstance();
        _menuSoundInstance.IsLooped = true;
    }
    
    public void InitializeButtons()
    {
        var startButton = _gameModel.StartMenuButton;
        startButton.Text = "НАЧАТЬ";
        startButton.Position = new Point(_screenWidth / 2 - 200, _screenHeight / 2 - 60);
        startButton.Width = 400;
        startButton.Height = 120;

        var exitButton = _gameModel.ExitMenuButton;
        exitButton.Text = "ВЫХОД";
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
        
        DrawButtonText(_gameModel.StartMenuButton, "ПРОДОЛЖИТЬ");
        DrawButtonText(_gameModel.ExitMenuButton, "ВЫХОД");

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
        var text = menuButton.Text;
        var textSize = _font.MeasureString(text);
    
        var padding = 20f;
        var scale = Math.Min(
            (menuButton.Height * 0.5f) / textSize.Y,
            (menuButton.Width - padding * 2) / textSize.X
        );
    
        var textPosition = new Vector2(
            menuButton.Position.X + (menuButton.Width - textSize.X * scale) / 2,
            menuButton.Position.Y + (menuButton.Height - textSize.Y * scale) / 2
        );
    
        _spriteBatch.DrawString(_font, text, textPosition, Color.Black, 
            0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
    }

    public void PlayMusic()
    {
        if (_menuSoundInstance.State != SoundState.Playing)
            _menuSoundInstance.Play();
    }
    
    public void StopMusic()
    {
        if (_menuSoundInstance.State == SoundState.Playing)
            _menuSoundInstance.Stop();
    }
}