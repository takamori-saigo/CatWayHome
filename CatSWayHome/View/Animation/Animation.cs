using System;
using System.Drawing;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CatSWayHome.View.Animations;

public class Animation
{
    public int _width  { get; private set; }
    public int _column  { get; private set; }
    public int _row  { get; private set; }
    public int _height  { get; private set; }
    public Texture2D _texture  { get; private set; }
    public int _currentFrame  { get; private set; }
    public int _totalFrames;
    public DateTime _lastFrameTime { get; private set; }
    private double _elips;
    
    public Animation(int row, int column, int width, int height, double elips , Texture2D texture)
    {
        _elips = elips;
        _row = row;
        _column = column;
        _totalFrames = column;
        _width = width;
        _height = height;
        _texture = texture;
        _currentFrame = 0;
        _lastFrameTime = DateTime.Now;
    }

    public void Update()
    {
        var currentTime = DateTime.Now;
        var elapsedTime = (currentTime - _lastFrameTime).TotalSeconds;

        if (elapsedTime >= _elips)
        {
            _currentFrame = (_currentFrame + 1)%_totalFrames;
            _lastFrameTime = currentTime;
        }
    }
}