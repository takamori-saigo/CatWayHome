    using System;
    using CatSWayHome.Models;
    using CatSWayHome.View.Animations;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Audio;   

    namespace CatSWayHome.View;

    public class GamePlayView: IViewGame
    {
        private GameModel _gameModel;
        private SpriteBatch _spriteBatch;
        /*
        private Texture2D _cointTexture;
        */
        private Texture2D _background;
        
        private Texture2D _heart;
        private Texture2D _emptyHeart;
        
        private Texture2D _rubbish;
        private Texture2D _cucumber;
        
        private Texture2D _catCalmTexture;
        private Texture2D _catMovingTexture;
        private Texture2D _catJumpingTexture;
        
        private SoundEffect _walkCatSound;
        private SoundEffectInstance _walkCatSoundInstance;
        private SoundEffect _jumpCatSound;
        
        private bool _catWasMoving;
        private bool _catWasJumping;
        
        private SpriteFont _font;
        /*
        private Dictionary<Coin, Animation> _coinAnimations;
        */
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
            _calmCatAnimation = new Animation(0, 3, _catCalmTexture.Width/3, _catCalmTexture.Height, 0.6, _catCalmTexture);
            _movingCatAnimation = new Animation(0, 6, _catMovingTexture.Width/6, _catMovingTexture.Height, 0.2, _catMovingTexture);
            _jumpingCatAnimation = new Animation(0, 5, _catJumpingTexture.Width/5, _catJumpingTexture.Height, 0.3, _catJumpingTexture);
            
            
            /*
            _coinAnimations = new();
            */
            
            /*foreach (var c in _gameModel.coins)
                _coinAnimations[c] = new Animation(0,4, _cointTexture.Width/6, _cointTexture.Height, 0.18, _cointTexture);*/
        }
        
        public void LoadTexture()
        {
            _font  = _contentManager.Load<SpriteFont>("Font");
            /*
            _cointTexture = _contentManager.Load<Texture2D>("coin");
            */
            _heart =  _contentManager.Load<Texture2D>("heart");
            _emptyHeart =  _contentManager.Load<Texture2D>("emptyHeart");
            _catCalmTexture =  _contentManager.Load<Texture2D>("cat_calmgR");
            _catMovingTexture =  _contentManager.Load<Texture2D>("cat_moving");
            _catJumpingTexture = _contentManager.Load<Texture2D>("jumping_cat");
            _background = _contentManager.Load<Texture2D>("new_background");
            _rubbish = _contentManager.Load<Texture2D>("rubish");
            _cucumber = _contentManager.Load<Texture2D>("Cucomber");
            
            _walkCatSound = _contentManager.Load<SoundEffect>("catWalking");
            _walkCatSoundInstance = _walkCatSound.CreateInstance();
            _walkCatSoundInstance.IsLooped = true;
            _jumpCatSound = _contentManager.Load<SoundEffect>("jumping_cat_sound");
        }
        
        public void Draw()
        {
            DrawMap();
            DrawCat();
            DrawEntities();
            /*
            foreach (var c in _gameModel.coins) DrawCoin(c);
            */
            DrawEntities();
            /*
            DrawScore();
            */
            DrawHeart();
        }

        private void DrawEntities()
        {
            var entites = _gameModel.Entities;
            
            for (var i = 0; i < entites.Count; i++)
            {
                var scale = 1f;
                switch (entites[i])
                {
                    case Rubbish:
                    {
                        var rub = entites[i];
                        scale = 0.37f;
                        _spriteBatch.Draw(_rubbish, rub.Position, null, Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
                        break;
                    }
                    case Cucumber:
                        var cuc = entites[i];
                        scale = 0.08f;
                        
                        if (cuc.HeightTexture == 0)
                        {
                            cuc.HeightTexture = _cucumber.Height * scale ;
                            cuc.WidthTexture = _cucumber.Width * scale ;
                        }
                        
                        DrawHitBox(cuc.Position, (int)cuc.WidthTexture, (int)cuc.HeightTexture);

                        _spriteBatch.Draw(_cucumber, cuc.Position, null, Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
                        break;
                }
            }
        }
        
        private void DrawCat()
        {
            var isMoving = _gameModel.Kitty.IsMoving;
            var isJumpig = _gameModel.Kitty.IsJump;
            if (isJumpig)
                DrawCurrentAnimationCat(_jumpingCatAnimation);
            else if (isMoving)
                DrawCurrentAnimationCat(_movingCatAnimation);
            else
                DrawCurrentAnimationCat(_calmCatAnimation);
            
            if ((isMoving && !isJumpig && !_catWasMoving) || (_catWasJumping && isMoving && !isJumpig))
                _walkCatSoundInstance.Play();                 
            else if ((!isMoving || isJumpig) && _catWasMoving)                                                                                                                                                                                                                 
                _walkCatSoundInstance.Stop();
            
            if (isJumpig && !_catWasJumping)
                _jumpCatSound.Play();

            _catWasMoving = isMoving; 
            _catWasJumping = isJumpig;
        }
        
        private void DrawCurrentAnimationCat(Animation animation)
        {
            
            var frameX = (animation._currentFrame % animation._column) * animation._width;
            var currentPosition = _gameModel.Kitty.InitialPosition +
                                  new Vector2(0, _gameModel.Kitty.DeltaY);
            var sourceRectangle = new Rectangle(frameX, 1, animation._width, animation._height);

            if (animation == _movingCatAnimation)
            {
                _gameModel.Kitty.HeightTexture = sourceRectangle.Height;
                _gameModel.Kitty.WidthTexture = sourceRectangle.Width;
            }

            
            DrawHitBox(new Vector2(currentPosition.X+35, currentPosition.Y), (int)_gameModel.Kitty.WidthTexture-75, (int)_gameModel.Kitty.HeightTexture);


            _spriteBatch.Draw(animation._texture,currentPosition, sourceRectangle, Color.White, 
                0f, Vector2.Zero, 1f, _gameModel.Kitty.IsGoingBack ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0f);
            animation.Update();
        }
        
        private void DrawMap()
        {
            var viewport = _spriteBatch.GraphicsDevice.Viewport;
            var bgWidth = _background.Width;
            var sourceWidth = 900;
            var scrollX = (_gameModel.Kitty.DeltaX % bgWidth + bgWidth) % bgWidth;
            var scale = (float)viewport.Width / sourceWidth;
            var firstWidth = Math.Min(sourceWidth, bgWidth-scrollX);
            var source1 = new Rectangle(scrollX, 800, firstWidth, 800);
            _spriteBatch.Draw(_background, Vector2.Zero, source1, 
                Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
            
            
            if (firstWidth < sourceWidth)
            {
                var secondWidth = sourceWidth - firstWidth;
                var source2 = new Rectangle(0, 800, secondWidth, 800);
                var pos2 = new Vector2(firstWidth * scale, 0);
                _spriteBatch.Draw(_background, pos2, source2, Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
            }
        }
        
        /*
        private void DrawCoin(Coin coin) 
        {
            var animation = _coinAnimations[coin];
            var frameX = (animation._currentFrame % animation._column) * animation._width;
            var sourceRectangle = new Rectangle(frameX, 1, animation._width, animation._height);
        
            _spriteBatch.Draw(_cointTexture, coin.Position, sourceRectangle, Color.White, 
                0f, Vector2.Zero, 2f, SpriteEffects.None, 0f);
            animation.Update();
        }
        */

        /*private void DrawScore()
        {
            var score = _gameModel.Kitty.Score;
            var text = $"SCORE: {score}";
            var size = _font.MeasureString(text);
            
            _spriteBatch.DrawString(_font, text, new Vector2(255,-5), Color.Black);
        }*/

        private void DrawHeart()
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

        private void DrawHitBox(Vector2 position, int width, int height)
        {
            var _pixelTexture = new Texture2D(_spriteBatch.GraphicsDevice, 1, 1);
            _pixelTexture.SetData(new[] { Color.White });
            _spriteBatch.Draw(_pixelTexture,position,new Rectangle(0,0,
                    width, height),
                Color.LightPink * 0.5f);
        }
    }