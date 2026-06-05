using System;
using System.Collections.Generic;
using CatSWayHome.Models;
using CatSWayHome.View.Animations;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace CatSWayHome.View.GamePlayViewDrawing;

public class DrawEntities: IDrawElement
{
    private SpriteBatch _spriteBatch;
    private ContentManager _content;
    private List<BaseEntity> _entities;
    private Texture2D _rubbish;
    private Texture2D _cucumber;
    private Texture2D _bench;
    private Texture2D _candei;
    private Texture2D _fish;
    private Texture2D _luk;
    private Texture2D _skotch;
    private Texture2D _exitDoor;
    private Texture2D _dog;
    private Animation _dogAnimation;
    private SoundEffect _barkSound;
    private Texture2D _car;
    private Texture2D _barrel;
    public DrawEntities(SpriteBatch spriteBatch, List<BaseEntity> entity,
        ContentManager content)
    {
        _spriteBatch = spriteBatch;
        _content = content;
        _entities = entity;
    }
    
    public void DrawDoor()
    {
        foreach (var e in _entities)
        {
            if (e is ExitDoor exitDoor)
            {
                DrawExitDoorGlow(exitDoor, 0.35f, _exitDoor);
                DrawCurrentEntity(exitDoor, 0.35f, _exitDoor);
                break;
            }
        }
    }

    public void DrawRest()
    {
        var dogs = new List<Dog>();

        foreach (var e in _entities)
        {
            switch (e)
            {
                case Rubbish:
                {
                    DrawCurrentEntity(e as Rubbish, 0.45f, _rubbish);
                    break;
                }
                case Cucumber:
                    DrawCurrentEntity(e as Cucumber, 0.07f, _cucumber);
                    break;
                case Bench:
                    DrawCurrentEntity(e as Bench,  0.4f, _bench);
                    break;
                case Candei:
                    DrawCurrentEntity(e as Candei,  0.15f, _candei);
                    break;
                case Fish:
                    var fish = e as Fish;
                    if (!fish.Take)
                        DrawCurrentEntity(fish, 0.07f, _fish);
                    break;
                case Luk:
                    DrawCurrentEntity(e as Luk, 0.25f, _luk);
                    break;
                case Skotch:
                    DrawCurrentEntity(e as Skotch, 0.25f, _skotch);
                    break;
                case ExitDoor:
                    break;
                case Dog:
                    dogs.Add(e as Dog);
                    break;
                case Car:
                    DrawCurrentEntity(e as Car, 0.5f, _car);
                    break;
                case Barrel:
                    DrawCurrentEntity(e as Barrel, 0.4f, _barrel);
                    break;
            }
        }

        foreach (var d in dogs)
            DrawDog(d);
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        DrawDoor();
        DrawRest();
    }

    private void DrawDog(Dog dog)
    {
        if (dog.HasRun) return;

        if (dog.JustBarked)
        {
            _barkSound.Play();
            dog.JustBarked = false;
        }

        var scale = 1f;
        if (dog.HeightTexture == 0)
        {
            dog.HeightTexture = _dogAnimation._height * scale;
            dog.WidthTexture = _dogAnimation._width * scale;
        }

        _dogAnimation.Update();
        var frameX = (_dogAnimation._currentFrame % _dogAnimation._column) * _dogAnimation._width;
        var sourceRect = new Rectangle(frameX, 0, _dogAnimation._width, _dogAnimation._height);
        var effect = SpriteEffects.FlipHorizontally;
        _spriteBatch.Draw(_dog, new Vector2(dog.Position.X, dog.PositionGround),
            sourceRect, Color.White, 0f, Vector2.Zero, scale, effect, 0f);

        /*DebugClassHitbox.DrawHitBox(new Vector2(dog.Position.X, dog.PositionGround),
            _spriteBatch, (int)dog.WidthTexture, (int)dog.HeightTexture);*/
    }

    private void DrawExitDoorGlow(BaseEntity entity, float scale, Texture2D texture)
    {
        var offset = 5f;

        for (var dx = -3; dx <= 3; dx++)
        {
            for (var dy = -3; dy <= 3; dy++)
            {
                if (dx == 0 && dy == 0) continue;
                var dist = Math.Abs(dx) + Math.Abs(dy);
                var alpha = dist <= 2 ? 0.3f : 0.15f;
                _spriteBatch.Draw(texture, entity.Position + new Vector2(dx * offset, dy * offset),
                    null, Color.LightYellow * alpha, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
            }
        }
    }

    private void DrawCurrentEntity(BaseEntity entity, float scale, Texture2D texture)
    {

        if (entity.HeightTexture == 0)
        {
            entity.HeightTexture = texture.Height * scale;
            entity.WidthTexture = texture.Width * scale;
        }

        /*DebugClassHitbox.DrawHitBox(entity.Position, _spriteBatch, (int)entity.WidthTexture,
            (int)entity.HeightTexture);*/
        _spriteBatch.Draw(texture, entity.Position, null, Color.White, 0f, Vector2.Zero, scale,
            SpriteEffects.None, 0f);
    }
    
    public void LoadContent()
    {
        _rubbish = _content.Load<Texture2D>("Entities/rubish");
        _cucumber = _content.Load<Texture2D>("Entities/Cucomber");
        _bench = _content.Load<Texture2D>("Entities/Bench");
        _candei = _content.Load<Texture2D>("Entities/conditioner");
        _fish = _content.Load<Texture2D>("Entities/fish");
        _luk = _content.Load<Texture2D>("Entities/luk");
        _skotch = _content.Load<Texture2D>("Entities/skotch");
        _exitDoor = _content.Load<Texture2D>("Entities/door");
        _dog = _content.Load<Texture2D>("Entities/dog");
        _barkSound = _content.Load<SoundEffect>("Entities/bark");
        _car = _content.Load<Texture2D>("Entities/car");
        _barrel = _content.Load<Texture2D>("Entities/box");
        _dogAnimation = new Animation(0, 4, _dog.Width / 4, _dog.Height, 0.4, _dog);
    }
}
