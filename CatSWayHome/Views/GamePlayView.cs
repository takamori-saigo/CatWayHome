    using System;
    using System.Collections.Generic;
    using CatSWayHome.Models;
    using CatSWayHome.View.Animations;
    using CatSWayHome.View.GamePlayViewDrawing;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Audio;   

    namespace CatSWayHome.View;

    public class GamePlayView: IViewGame
    {
        private GameModel _gameModel;
        private SpriteBatch _spriteBatch;
        
        private List<IDrawElement> _drawElements;
        
        public GamePlayView(SpriteBatch spriteBatch, GameModel gameModel, ContentManager content)
        {
            _spriteBatch = spriteBatch;
            _gameModel = gameModel;
            
            _drawElements = new List<IDrawElement>();
            _drawElements.Add(new DrawMap(_gameModel, content));
            _drawElements.Add(new DrawCat(_gameModel.Kitty, spriteBatch, content));
            _drawElements.Add(new DrawEntities(spriteBatch, _gameModel.Entities, content));
            _drawElements.Add(new DrawHeart(spriteBatch, _gameModel.Kitty, content));
        }
        
        public void LoadContent()
        {
            foreach (var e in _drawElements)
            {
                e.LoadContent();
            }
        }
        
        public void Draw()
        {
            foreach (var e in _drawElements)
            {
                e.Draw(_spriteBatch);
            }
        }
    }