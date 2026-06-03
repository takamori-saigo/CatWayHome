using System;
using System.Linq;
using CatSWayHome.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Vector2 = System.Numerics.Vector2;


namespace CatSWayHome.Controllers;

public class GamePlayController: IController
{
    private GameModel _game;
    private Cat _cat;
    public GamePlayController(GameModel gameModel)
    {
        _game = gameModel;
        _cat = _game.Kitty;
    }

    public void Update(GameTime gameTime)
    {
        if (_cat.Health == 0) _game.State = GameState.Paused;
        var keyBoard = Keyboard.GetState();
        var elapsed = 1f / 60f;
        if (keyBoard.IsKeyDown(Keys.Escape)) _game.State = GameState.Paused;

        if (_cat.IsFirstLaunch)
        {
            _cat.DialogText = "Welcome to the game!";
            _cat.ShowDialog = true;
            _cat.DialogCharIndex = 0;
            _cat.DialogCharTimer = 0;
            _cat.IsFirstLaunch = false;
        }

        if (_cat.ShowDialog)
        {
            if (_cat.DialogCharIndex < _cat.DialogText.Length)
            {
                _cat.DialogCharTimer += elapsed;
                if (_cat.DialogCharTimer >= 0.15f)
                {
                    _cat.DialogCharIndex++;
                    _cat.DialogCharTimer = 0;
                }
            }
            else
            {
                _cat.DialogTimeLeft -= elapsed;
                if (_cat.DialogTimeLeft <= 0)
                    _cat.ShowDialog = false;
            }
        }
        else
        {
            CheckEntityProximity();
        }

        CatMoving(keyBoard);
        ApplyPhysics(elapsed);
    }

    private void UpdateCoordsEntities()
    {
        foreach (var e in _game.Entities)
        {
            e.Update(_cat.DeltaX);
        }
    }
    
    private void ApplyPhysics(float elapsed)
    {
        if (_cat.IsJump)  
        {
            _cat.VelocityY += Cat.Gravity * elapsed;
            _cat.DeltaY += _cat.VelocityY * elapsed;

            if (_cat.DeltaY >= 0)
            {
                _cat.DeltaY = 0;
                _cat.VelocityY = 0;
                _cat.IsJump = false;
                _cat.IsKnockback = false;
                _cat.OnTheObject = false;
            }
        }

        if (_cat.OnTheObject && !_cat.IsJump && !CheckStandingOnSurface())
        {
            _cat.OnTheObject = false;
            _cat.IsJump = true;
            _cat.VelocityY = 0;
        }
        
        if (_cat.IsKnockback)
            _cat.DeltaX += (_cat.IsGoingBack? 1 : -1) * _cat.VelocityX;
    }

    private void TriggerDialog(string message)
    {
        _cat.DialogText = message;
        _cat.ShowDialog = true;
        _cat.DialogTimeLeft = 5f;
        _cat.DialogCharIndex = 0;
        _cat.DialogCharTimer = 0;
    }

    private void CheckEntityProximity()
    {
        var catScreenX = _cat.InitialPosition.X;
        var catScreenY = _cat.InitialPosition.Y + _cat.DeltaY;

        foreach (var e in _game.Entities)
        {
            if (e.DialogMessage == null || e.HasTriggeredDialog)
                continue;

            var entityScreenX = e.WorldPosition.X - _cat.DeltaX * e.ParallaxFactor;
            var entityScreenY = e.PositionGround;
            var dx = catScreenX - entityScreenX;
            var dy = catScreenY - entityScreenY;
            var distance = Math.Sqrt(dx * dx + dy * dy);

            if (distance <= e.DialogTriggerDistance)
            {
                e.HasTriggeredDialog = true;
                TriggerDialog(e.DialogMessage);
                return;
            }
        }
    }

    private bool CheckStandingOnSurface()
    {
        var catLeft = _cat.InitialPosition.X + 35;
        var catTop = _cat.InitialPosition.Y + _cat.DeltaY;
        var catRight = catLeft + _cat.WidthTexture - 75;
        var catBottom = catTop + _cat.HeightTexture;

        foreach (var e in _game.Entities)
        {
            if (e.IsSurface)
            {
                var surfLeft = e.WorldPosition.X - _cat.DeltaX * e.ParallaxFactor;
                var surfTop = e.PositionGround;
                var surfRight = surfLeft + e.WidthTexture;
                var surfBottom = surfTop + e.HeightTexture;

                if (catLeft <= surfRight && catRight >= surfLeft &&
                    catTop <= surfBottom && catBottom >= surfTop)
                    return true;
            }
        }

        return false;
    }

    private void CatMoving(KeyboardState keyboard)
    {
        var velocity = _cat.VelocityX;
        
        _cat.CatWasMoving = _cat.IsMoving; 
        _cat.IsMoving = false;
        _cat.CatWasJumping = _cat.IsJump;
        if (_cat.IsJump)
            CheckHitBoxes(_cat.DeltaX);
        if ((keyboard.IsKeyDown(Keys.Space) || keyboard.IsKeyDown(Keys.Up) || keyboard.IsKeyDown(Keys.W)) && !_cat.IsJump)
        {
            _cat.VelocityY = Cat.JumpVelocity;
            _cat.IsJump = true;
        }
        if ((keyboard.IsKeyDown(Keys.D) || keyboard.IsKeyDown(Keys.Right)) && 
            !_cat.IsKnockback &&
            !CheckHitBoxes(_cat.DeltaX + velocity))
        {
            _cat.IsCalm = false;
            _cat.IsMoving = true;
            _cat.DeltaX += velocity;
            _cat.IsGoingBack = false;
        }
        else if ((keyboard.IsKeyDown(Keys.A) || keyboard.IsKeyDown(Keys.Left)) &&
                 !_cat.IsKnockback &&
                 !CheckHitBoxes(_cat.DeltaX - velocity))
        {
            _cat.IsCalm = false;
            _cat.IsMoving = true;
            _cat.DeltaX -= velocity;
            _cat.IsGoingBack = true;
        }
        else
        {
            _cat.IsCalm = true;
            _cat.IsMoving = false;
        }
        UpdateCoordsEntities();
    }

    private bool CheckHitBoxes(int newX)
    {
        var catRect = new Rectangle((int)_cat.InitialPosition.X+35, (int)(_cat.InitialPosition.Y + _cat.DeltaY),
            (int)_cat.WidthTexture-75, (int)_cat.HeightTexture);
        foreach (var e in _game.Entities)
        {
            var entityScreenX = e.WorldPosition.X - newX * e.ParallaxFactor;
            var entityRect = new Rectangle((int)entityScreenX, e.PositionGround - e.HitBoxPoisitionY,
                (int)e.WidthTexture, (int)e.HeightTexture + e.HitBoxPoisitionY);
            if (catRect.Intersects(entityRect))
            {
                CheckKollision(e);
                return true;
            }
        }
        
        return false;
    }

    private void CheckKollision(BaseEntity entity)
    {
        switch (entity)
        {
            case Cucumber :
                _cat.Health--;
                _cat.VelocityY = Cat.JumpVelocity;
                _cat.IsJump = true;
                _cat.IsKnockback = true;
                break;
            case Fish :
                var fish = entity as Fish;
                if (_cat.Health < 4)
                    _cat.Health++;
                fish.Take = true;
                _game.Entities.Remove(fish);
                break;
        }

        
        
        if (entity.IsSurface && entity.PositionGround >= _cat.InitialPosition.Y + _cat.DeltaY)
        {
            _cat.DeltaY = entity.PositionGround - _cat.InitialPosition.Y - _cat.HeightTexture;
            _cat.VelocityY = 0;
            _cat.IsJump = false;
            _cat.OnTheObject = true;
        }

        if (entity.DialogMessage != null && !entity.HasTriggeredDialog && !_cat.ShowDialog)
        {
            entity.HasTriggeredDialog = true;
            TriggerDialog(entity.DialogMessage);
        }
    } 
}