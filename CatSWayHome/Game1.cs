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
    public Game1()
    {
        Content.RootDirectory = "Content";
        _graphics = new GraphicsDeviceManager(this);
        IsMouseVisible = false;
    }

    protected override void Initialize()
    {
        _graphics.PreferredBackBufferWidth = 1600;
        _graphics.PreferredBackBufferHeight = 900;
        _graphics.ApplyChanges();
        _gameModel = new GameModel(this);
        _gameController = new GameController(_gameModel);
        base.Initialize();
        

    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        _viewGame = new ViewGame(_spriteBatch, _gameModel);
        // TODO: use this.Content to load your game content here
    }
    
    protected override void Update(GameTime gameTime)
    {
        _gameController.Update();
        if (_gameModel.State == GameState.Exit) Exit();
        // TODO: Add your update logic here
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