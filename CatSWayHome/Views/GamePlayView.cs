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
    private ContentManager _content;
    
    private List<IDrawElement> _drawElements;
    private DrawCat _drawCat;
    private DrawEntities _drawEntities;
    private DrawHeart _drawHeart;

    private SoundEffect _cityNoise;
    private SoundEffectInstance _cityNoiseInstance;
    
    public GamePlayView(SpriteBatch spriteBatch, GameModel gameModel, ContentManager content)
    {
        _spriteBatch = spriteBatch;
        _gameModel = gameModel;
        _content = content;
        
        _drawCat = new DrawCat(_gameModel.Kitty, spriteBatch, content);
        _drawEntities = new DrawEntities(spriteBatch, _gameModel.Entities, content);
        _drawHeart = new DrawHeart(spriteBatch, _gameModel.Kitty, content);
        _drawElements = new List<IDrawElement>();
        _drawElements.Add(new DrawMap(_gameModel, content));
        _drawElements.Add(_drawEntities);
        _drawElements.Add(_drawCat);
        _drawElements.Add(_drawHeart);
    }
    
    public void LoadContent()
    {
        foreach (var e in _drawElements)
        {
            e.LoadContent();
        }

        _cityNoise = _content.Load<SoundEffect>("background/CityNoise");
        _cityNoiseInstance = _cityNoise.CreateInstance();
        _cityNoiseInstance.IsLooped = true;
    }
    
    public void PlayMusic()
    {
        if (_cityNoiseInstance.State != SoundState.Playing)
            _cityNoiseInstance.Play();
    }
    
    public void StopMusic()
    {
        if (_cityNoiseInstance.State == SoundState.Playing)
            _cityNoiseInstance.Stop();
    }
    
    public void Draw()
    {
        foreach (var e in _drawElements)
        {
            if (e == _drawCat || e == _drawEntities || e == _drawHeart)
                continue;
            e.Draw(_spriteBatch);
        }

        _drawEntities.DrawDoor();
        _drawCat.Draw(_spriteBatch);
        _drawEntities.DrawRest();
        _drawHeart.Draw(_spriteBatch);
        _drawCat.DrawDialog();
    }
}