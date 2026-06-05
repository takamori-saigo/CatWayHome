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
    private KeyboardState _previousKeyboardState;
    public GamePlayController(GameModel gameModel)
    {
        _game = gameModel;
        _cat = _game.Kitty;
    }

    public void Update(GameTime gameTime)
    {
        if (_cat.Health == 0) _game.State = GameState.Lost;
        var keyBoard = Keyboard.GetState();
        var elapsed = 1f / 60f;
        if (keyBoard.IsKeyDown(Keys.Escape))
        {
            _game.StartMenuButton.Text = "ПРОДОЛЖИТЬ";
            _game.State = GameState.Paused;
        }

        if (_cat.IsFirstLaunch && _cat.IsMoving)
        {
            _cat.DialogText = "Эх... меня наверное уже потеряла хозяйка..Нужно возвращаться домой";
            _cat.ShowDialog = true;
            _cat.DialogTimeLeft = 2f;
            _cat.DialogCharIndex = _cat.DialogText.Length;
            _cat.IsFirstLaunch = false;
        }

        if (_cat.ShowDialog)
        {
            _cat.DialogTimeLeft -= elapsed;
            if (_cat.DialogTimeLeft <= 0)
                _cat.ShowDialog = false;
        }
        else
        {
            CheckEntityProximity();
        }

        if (_cat.JumpCooldown > 0)
            _cat.JumpCooldown -= elapsed;
        if (_cat.InvulnerabilityTimer > 0)
            _cat.InvulnerabilityTimer -= elapsed;
        if (_cat.ExitDoorTimer > 0)
        {
            _cat.ExitDoorTimer -= elapsed;
            if (_cat.ExitDoorTimer <= 0)
                _game.State = GameState.Won;
        }
        UpdateDog();
        CheckDogCollision();
        CatMoving(keyBoard);
        ApplyPhysics(elapsed);
        _previousKeyboardState = keyBoard;
    }

    private void UpdateCoordsEntities()
    {
        foreach (var e in _game.Entities)
        {
            e.Update(_cat.DeltaX);
        }
    }
    
    private void UpdateDog()
    {
        var catScreenX = _cat.InitialPosition.X;
        foreach (var e in _game.Entities.OfType<Dog>())
        {
            if (e.HasRun) continue;

            if (!e.IsRunning)
            {
                var dogScreenX = e.WorldPosition.X - _cat.DeltaX * e.ParallaxFactor;
                var dx = (float)catScreenX - dogScreenX;
                var distance = Math.Abs(dx);

                if (distance <= e.TriggerDistance)
                {
                    e.IsRunning = true;
                    e.RunWorldPosX = e.WorldPosition.X;
                    e.JustBarked = true;
                }
            }

            if (e.IsRunning)
            {
                e.RunWorldPosX -= 4f;
                e.WorldPosition = new Vector2(e.RunWorldPosX, e.PositionGround);
                if (e.RunWorldPosX < _cat.DeltaX * e.ParallaxFactor - 200)
                    e.HasRun = true;
            }
        }
    }

    private void CheckDogCollision()
    {
        var catRect = new Rectangle((int)_cat.InitialPosition.X + 35, (int)(_cat.InitialPosition.Y + _cat.DeltaY),
            (int)_cat.WidthTexture - 75, (int)_cat.HeightTexture);

        foreach (var e in _game.Entities.OfType<Dog>())
        {
            if (e.HasRun || !e.IsRunning) continue;

            var dogScreenX = e.WorldPosition.X - _cat.DeltaX * e.ParallaxFactor;
            var dogRect = new Rectangle((int)dogScreenX, e.PositionGround - e.HitBoxPoisitionY,
                (int)e.WidthTexture, (int)e.HeightTexture + e.HitBoxPoisitionY);

            if (catRect.Intersects(dogRect))
            {
                if (IsCatNearRubbish()) return;
                if (_cat.InvulnerabilityTimer <= 0)
                {
                    _cat.Health--;
                    _cat.JustGotHit = true;
                    _cat.InvulnerabilityTimer = 1.5f;
                }
                _cat.VelocityY = Cat.JumpVelocity;
                _cat.IsJump = true;
                _cat.IsKnockback = true;
            }
        }
    }

    private bool IsCatNearRubbish()
    {
        var catLeft = _cat.InitialPosition.X + 35;
        var catRight = catLeft + _cat.WidthTexture - 75;
        foreach (var e in _game.Entities)
        {
            if (e is Rubbish || e is Car)
            {
                var entityScreenX = e.WorldPosition.X - _cat.DeltaX * e.ParallaxFactor;
                var width = e.WidthTexture > 0 ? e.WidthTexture : 200;
                var left = entityScreenX;
                var right = entityScreenX + width;
                if (catLeft < right && catRight > left)
                    return true;
            }
        }
        return false;
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
        _cat.DialogTimeLeft = 4f;
        _cat.DialogCharIndex = message.Length;
    }

    private void CheckEntityProximity()
    {
        var catScreenX = _cat.InitialPosition.X;
        var catScreenY = _cat.InitialPosition.Y + _cat.DeltaY;

        foreach (var e in _game.Entities)
        {
            if (e.DialogMessage == null || _cat.TriggeredDialogTypes.Contains(e.GetType().Name) || e is Fish)
                continue;

            var entityScreenX = e.WorldPosition.X - _cat.DeltaX * e.ParallaxFactor;
            var entityScreenY = e.PositionGround;
            var dx = catScreenX - entityScreenX;
            var dy = catScreenY - entityScreenY;
            var distance = Math.Sqrt(dx * dx + dy * dy);

            if (distance <= e.DialogTriggerDistance)
            {
                _cat.TriggeredDialogTypes.Add(e.GetType().Name);
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
        var jumpJustPressed = (keyboard.IsKeyDown(Keys.Space) && !_previousKeyboardState.IsKeyDown(Keys.Space)) ||
                              (keyboard.IsKeyDown(Keys.Up) && !_previousKeyboardState.IsKeyDown(Keys.Up)) ||
                              (keyboard.IsKeyDown(Keys.W) && !_previousKeyboardState.IsKeyDown(Keys.W));
        if (jumpJustPressed && !_cat.IsJump && _cat.JumpCooldown <= 0)
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
            if (e is Dog { HasRun: true }) continue;

            var entityScreenX = e.WorldPosition.X - newX * e.ParallaxFactor;
            var entityRect = new Rectangle((int)entityScreenX, e.PositionGround - e.HitBoxPoisitionY,
                (int)e.WidthTexture, (int)e.HeightTexture + e.HitBoxPoisitionY);
            if (catRect.Intersects(entityRect))
            {
                CheckKollision(e);
                if (e is Rubbish || e is Car) continue;
                return true;
            }
        }
        
        return false;
    }

    private void CheckKollision(BaseEntity entity)
    {
        switch (entity)
        {
            case Skotch:
            case Cucumber :
                if (_cat.InvulnerabilityTimer <= 0)
                {
                    _cat.Health--;
                    _cat.JustGotHit = true;
                    _cat.InvulnerabilityTimer = 1.5f;
                }
                _cat.VelocityY = Cat.JumpVelocity;
                _cat.IsJump = true;
                _cat.IsKnockback = true;
                break;
            case Luk :
                if (_cat.InvulnerabilityTimer <= 0)
                {
                    _cat.Health--;
                    _cat.JustGotHit = true;
                    _cat.InvulnerabilityTimer = 1.5f;
                }
                _cat.VelocityY = Cat.JumpVelocity;
                _cat.IsJump = true;
                _cat.IsKnockback = true;
                break;
            case Dog:
                if (IsCatNearRubbish())
                    break;
                if (_cat.InvulnerabilityTimer <= 0)
                {
                    _cat.Health--;
                    _cat.JustGotHit = true;
                    _cat.InvulnerabilityTimer = 1.5f;
                }
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
                _cat.TriggeredDialogTypes.Add(entity.GetType().Name);
                TriggerDialog(entity.DialogMessage);
                break;
            case ExitDoor:
                if (_cat.ExitDoorTimer <= 0)
                    _cat.ExitDoorTimer = 1f;
                break;
        }
        
        if (entity.IsSurface &&
            entity.PositionGround >= _cat.InitialPosition.Y + _cat.DeltaY &&
            _cat.InitialPosition.Y + _cat.DeltaY + _cat.HeightTexture <= entity.PositionGround + 20)
        {
            _cat.IsKnockback = false;
            if (_cat.VelocityY < 0) 
            {
                _cat.VelocityY = 0;
                _cat.DeltaY = entity.PositionGround - _cat.InitialPosition.Y + 1;
            }
            else
            {
                _cat.DeltaY = entity.PositionGround - _cat.InitialPosition.Y - _cat.HeightTexture;
                _cat.VelocityY = 0;
                _cat.IsJump = false;
                _cat.OnTheObject = true;
                _cat.JumpCooldown = 0.15f;
            }
        }

        if (entity.DialogMessage != null && !_cat.TriggeredDialogTypes.Contains(entity.GetType().Name) && !_cat.ShowDialog)
        {
            _cat.TriggeredDialogTypes.Add(entity.GetType().Name);
            TriggerDialog(entity.DialogMessage);
        }
    } 
}