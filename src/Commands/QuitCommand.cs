using System;

namespace ZeldaGame.Commands;

public class QuitCommand : ICommand
{
    private Game1 myGame;
    public QuitCommand(Game1 game) {
        this.myGame = game;
    }
    public void Execute()
    {
        if (myGame.GraphicsDevice.GetRenderTargets().Length > 0)
            myGame.GraphicsDevice.SetRenderTarget(null);
        Globals.audioLoader.Reset();
        myGame.Exit();
    }
}
