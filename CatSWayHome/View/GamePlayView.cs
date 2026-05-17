    using System.Collections.Generic;
    using CatSWayHome.Controllers;
    using CatSWayHome.Models;
    using CatSWayHome.View.Animations;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    namespace CatSWayHome.View;

    public class GamePlayView: IViewGame
    {
        private GameModel _gameModel;
        private SpriteBatch _spriteBatch;
        private Texture2D _cointTexture;
        private Texture2D _heart;
        private Texture2D _emptyHeart;
        private SpriteFont _font;
        private Dictionary<Coin, CoinAnimation> _coinAnimations;
        public GamePlayView(SpriteBatch spriteBatch, GameModel gameModel)
        {
            _spriteBatch = spriteBatch;
            _gameModel = gameModel;
            _font  = _gameModel.CatGame.Content.Load<SpriteFont>("Font");
            _cointTexture = gameModel.CatGame.Content.Load<Texture2D>("coin");
            _heart =  gameModel.CatGame.Content.Load<Texture2D>("heart");
            _emptyHeart =  gameModel.CatGame.Content.Load<Texture2D>("emptyHeart");
            _coinAnimations = new();
            
            foreach (var c in _gameModel.coins)
            {
                _coinAnimations[c] = new CoinAnimation(6, _cointTexture.Width/6, _cointTexture.Height, _cointTexture);
            }
        }
        public void Draw()
        {
            foreach (var c in _gameModel.coins) DrawCoin(c);
            DrawScore();
            DrawHeart();
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