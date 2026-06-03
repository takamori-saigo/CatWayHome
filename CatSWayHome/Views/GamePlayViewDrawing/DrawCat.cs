using CatSWayHome.Models;
using CatSWayHome.View.Animations;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace CatSWayHome.View.GamePlayViewDrawing;

public class DrawCat: IDrawElement
{
    private Cat _cat;
    private SpriteBatch _spriteBatch;
    private ContentManager _contentManager;
    
    private Texture2D _catCalmTexture;
    private Texture2D _catMovingTexture;
    private Texture2D _catJumpingTexture;
    private Texture2D _dialogWindow; 
    
    private Animation _calmCatAnimation;
    private Animation _movingCatAnimation;
    private Animation _jumpingCatAnimation;

    private SoundEffect _walkCatSound;
    private SoundEffectInstance _walkCatSoundInstance;
    private SoundEffect _jumpCatSound;
    
    public DrawCat(Cat cat, SpriteBatch spriteBatch, ContentManager contentManager)
    {
        _cat = cat;
        _spriteBatch = spriteBatch;
        _contentManager = contentManager;
    }
    
    public void LoadAnimations()
    {
        _calmCatAnimation = new Animation(0, 3, _catCalmTexture.Width/3, _catCalmTexture.Height, 0.6, _catCalmTexture);
        _movingCatAnimation = new Animation(0, 6, _catMovingTexture.Width/6, _catMovingTexture.Height, 0.2, _catMovingTexture);
        _jumpingCatAnimation = new Animation(0, 5, _catJumpingTexture.Width/5, _catJumpingTexture.Height, 0.3, _catJumpingTexture);
            
    }
    
    public void LoadContent()
    {
        _catCalmTexture =  _contentManager.Load<Texture2D>("Cat/cat_calmgR");
        _catMovingTexture =  _contentManager.Load<Texture2D>("Cat/cat_moving");
        _catJumpingTexture = _contentManager.Load<Texture2D>("Cat/jumping_cat");
        _walkCatSound = _contentManager.Load<SoundEffect>("Cat/catWalking");
        _walkCatSoundInstance = _walkCatSound.CreateInstance();
        _walkCatSoundInstance.IsLooped = true;
        _jumpCatSound = _contentManager.Load<SoundEffect>("Cat/jumping_cat_sound");
        LoadAnimations();
    }
    
    public void Draw(SpriteBatch spriteBatch)
    {
        if (_cat.IsJump)
            DrawCurrentAnimationCat(_jumpingCatAnimation);
        else if (_cat.IsMoving)
            DrawCurrentAnimationCat(_movingCatAnimation);
        else
            DrawCurrentAnimationCat(_calmCatAnimation);
            
        if ((_cat.IsMoving && !_cat.IsJump && !_cat.CatWasMoving) || (_cat.CatWasJumping && _cat.IsMoving && !_cat.IsJump))
            _walkCatSoundInstance.Play();                 
        else if ((!_cat.IsMoving || _cat.IsJump) && _cat.CatWasMoving)                                                                                                                                                                                                                 
            _walkCatSoundInstance.Stop();
            
        if (_cat.IsJump && !_cat.CatWasJumping)
            _jumpCatSound.Play();
    }
    
    private void DrawCurrentAnimationCat(Animation animation)
    {
        var frameX = (animation._currentFrame % animation._column) * animation._width;
        var currentPosition = _cat.InitialPosition +
                              new Vector2(0, _cat.DeltaY);
        var sourceRectangle = new Rectangle(frameX, 1, animation._width, animation._height);

        if (animation == _movingCatAnimation)
        {
            _cat.HeightTexture = sourceRectangle.Height;
            _cat.WidthTexture = sourceRectangle.Width;
        }
            
        DebugClassHitbox.DrawHitBox(new Vector2(currentPosition.X+35, currentPosition.Y), _spriteBatch, (int)_cat.WidthTexture-75, (int)_cat.HeightTexture);

        _spriteBatch.Draw(animation._texture,currentPosition, sourceRectangle, Color.White, 
            0f, Vector2.Zero, 1f, _cat.IsGoingBack ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0f);
        animation.Update();
    }
}