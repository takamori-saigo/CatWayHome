    using System.Collections.Generic;
    using CatSWayHome.Controllers;
    using CatSWayHome.Models;
    using CatSWayHome.View.Animations;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;

    namespace CatSWayHome.View;

    public class GamePlayView: IViewGame
    {
        private GameModel _gameModel;
        private SpriteBatch _spriteBatch;
        private Texture2D _cointTexture;
        private Texture2D _heart;
        private Texture2D _emptyHeart;
        private Texture2D _background;
        
        private Texture2D _catCalmTexture;
        private Texture2D _catMovingTexture;
        private Texture2D _catJumpingTexture;
        
        private SpriteFont _font;
        private Dictionary<Coin, Animation> _coinAnimations;
        private Animation _calmCatAnimation;
        private Animation _movingCatAnimation;
        private Animation _jumpingCatAnimation;
        
        private ContentManager _contentManager;
        public GamePlayView(SpriteBatch spriteBatch, GameModel gameModel, ContentManager content)
        {
            _spriteBatch = spriteBatch;
            _gameModel = gameModel;
            _contentManager = content;
            
            LoadTexture();

            LoadAnimations();
        }

        public void LoadAnimations()
        {
            _calmCatAnimation = new Animation(0, 4, _catCalmTexture.Width/4, _catCalmTexture.Height, 0.6, _catCalmTexture);
            
            
            _movingCatAnimation = new Animation(0, 6, _catMovingTexture.Width/6, _catMovingTexture.Height, 0.2, _catMovingTexture);
            _jumpingCatAnimation = new Animation(0, 5, _catJumpingTexture.Width/5, _catJumpingTexture.Height, 0.2, _catJumpingTexture);
            
            
            _coinAnimations = new();
            
            foreach (var c in _gameModel.coins)
                _coinAnimations[c] = new Animation(0,4, _cointTexture.Width/6, _cointTexture.Height, 0.18, _cointTexture);
        }
        
        public void LoadTexture()
        {
            _font  = _contentManager.Load<SpriteFont>("Font");
            _cointTexture = _contentManager.Load<Texture2D>("coin");
            _heart =  _contentManager.Load<Texture2D>("heart");
            _emptyHeart =  _contentManager.Load<Texture2D>("emptyHeart");
            _catCalmTexture =  _contentManager.Load<Texture2D>("calm_cat");
            _catMovingTexture =  _contentManager.Load<Texture2D>("cat_moving");
            _catJumpingTexture = _contentManager.Load<Texture2D>("jumping_cat");
            _background = _contentManager.Load<Texture2D>("new_background");
        }
        
        public void Draw()
        {
            DrawMap();
            DrawCat();
            foreach (var c in _gameModel.coins) DrawCoin(c);
            DrawScore();
            DrawHeart();
        }

        public void DrawCat()
        {
            if (_gameModel.Kitty.IsCalm)
                DrawCurrentAnimationCat(_calmCatAnimation);
            else if (_gameModel.Kitty.IsMoving)
                DrawCurrentAnimationCat(_movingCatAnimation);
            else if (_gameModel.Kitty.IsJump) DrawCurrentAnimationCat(_jumpingCatAnimation);
        }
        public void DrawCurrentAnimationCat(Animation animation)
        {
            
            var frameX = (animation._currentFrame % animation._column) * animation._width;
            var sourceRectangle = new Rectangle(frameX, 1, animation._width, animation._height);
            _spriteBatch.Draw(animation._texture,_gameModel.Kitty.InitionalPosition, sourceRectangle, Color.White, 
                0f, Vector2.Zero, 1f, _gameModel.Kitty.IsGoingBack ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0f);
            animation.Update();
        }
        
        public void DrawMap()
        {
            var current = new Rectangle(_gameModel.Kitty.DeltaX, 680, 900, 630);
            var destinationRectangle = new Rectangle(0, 0, 
                _spriteBatch.GraphicsDevice.Viewport.Width, 
                _spriteBatch.GraphicsDevice.Viewport.Height);
            _spriteBatch.Draw(_background, destinationRectangle, current, Color.White);
        }
        
        public void DrawCoin(Coin coin) 
        {
            var animation = _coinAnimations[coin];
            var frameX = (animation._currentFrame % animation._column) * animation._width;
            var sourceRectangle = new Rectangle(frameX, 1, animation._width, animation._height);
        
            _spriteBatch.Draw(_cointTexture, coin.Position, sourceRectangle, Color.White, 
                0f, Vector2.Zero, 2f, SpriteEffects.None, 0f);
            animation.Update();
        }

        public void DrawScore()
        {
            var score = _gameModel.Kitty.Score;
            var text = $"SCORE: {score}";
            var size = _font.MeasureString(text);
            
            _spriteBatch.DrawString(_font, text, new Vector2(255,-5), Color.Black);
        }

        public void DrawHeart()
        {
            var aliveHearts = _gameModel.Kitty.Health;
            var countBreak = 4 - aliveHearts;
            var position = Vector2.Zero;
            for (var i = 0; i < aliveHearts; i++)
            {
                _spriteBatch.Draw(_heart, position, null , Color.White, 0f, Vector2.Zero, 2f, SpriteEffects.None, 0f);
                position+=new Vector2(60, 0);
            }
            for (var i = 0; i < countBreak; i++)
            {
                _spriteBatch.Draw(_emptyHeart, position, null , Color.White, 0f, Vector2.Zero, 2f, SpriteEffects.None, 0f);
                position+=new Vector2(60, 0);
            }
            
        }
    }