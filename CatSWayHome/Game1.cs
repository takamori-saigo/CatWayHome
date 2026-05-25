using CatSWayHome.Controllers;
using CatSWayHome.Models;
using CatSWayHome.View;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace CatSWayHome;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private GameController _gameController;
    private ViewGame _viewGame;
    private GameModel _gameModel;
    private int _screenWidth;
    private int _screenHeight;
    public Game1()
    {
        Content.RootDirectory = "Content";
        _graphics = new GraphicsDeviceManager(this);
        IsMouseVisible = false;
        _screenWidth = 1600;
        _screenHeight = 900;
    }

    protected override void Initialize()
    {
        _graphics.PreferredBackBufferWidth = _screenWidth;
        _graphics.PreferredBackBufferHeight = _screenHeight;
        _graphics.ApplyChanges();
        _gameModel = new GameModel();
        _gameController = new GameController(_gameModel);
        base.Initialize();
        

    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        _viewGame = new ViewGame(_spriteBatch, _gameModel, Content);
    }
    
    protected override void Update(GameTime gameTime)
    {
        _gameController.Update();
        if (_gameModel.State == GameState.Exit) Exit();
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.LightPink);
        _spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp);
        _viewGame.Draw();
        _spriteBatch.End();
        base.Draw(gameTime);
    }
}