using CatSWayHome.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace CatSWayHome.Controllers;

public class GamePlayController: IController
{
    private GameModel _game;
    public GamePlayController(GameModel gameModel)
    {
        _game = gameModel;
    }
    public void Update()
    {
        var keyBoard = Keyboard.GetState();
        var elapsed = 1f / 60f;
        if (keyBoard.IsKeyDown(Keys.Escape))
            _game.State = GameState.Paused;

        if ((keyBoard.IsKeyDown(Keys.Space) || keyBoard.IsKeyDown(Keys.Up) || keyBoard.IsKeyDown(Keys.W)) && !_game.Kitty.IsJump)
        {
            _game.Kitty.VelocityY = Cat.JumpVelocity;
            _game.Kitty.IsJump = true;
        }
        
        CatMoving(keyBoard);
        ApplyPhysics(elapsed);
    }
    
    private void ApplyPhysics(float elapsed)
    {
        var cat = _game.Kitty;

        if (cat.IsJump)
        {
            cat.VelocityY += Cat.Gravity * elapsed;
            cat.DeltaY += cat.VelocityY * elapsed;

            if (cat.DeltaY >= 0)
            {
                cat.DeltaY = 0;
                cat.VelocityY = 0;
                cat.IsJump = false;
            }
        }
    }
    
    public void CatMoving(KeyboardState keyboard)
    {
        var velocity = _game.Kitty.Velocity;
        var cat = _game.Kitty;
        _game.Kitty.IsMoving = false;
        if (keyboard.IsKeyDown(Keys.D) || keyboard.IsKeyDown(Keys.Right))
        {
            cat.IsCalm = false;
            cat.IsMoving = true;
            cat.DeltaX += velocity;
            cat.IsGoingBack = false;
        }
        else if (keyboard.IsKeyDown(Keys.A) || keyboard.IsKeyDown(Keys.Left))
        {
            cat.IsCalm = false;
            cat.IsMoving = true;
            cat.DeltaX -= velocity;
            cat.IsGoingBack = true;
        }
        else
        {
            cat.IsCalm = true;
            cat.IsMoving = false;
        }
    }
}