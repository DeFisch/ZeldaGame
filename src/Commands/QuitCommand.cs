namespace ZeldaGame.Commands;

public class QuitCommand : ICommand
{
    private Game1 myGame;
    public QuitCommand(Game1 game) {
        this.myGame = game;
    }
    public void Execute()
    {
        myGame.GraphicsDevice.SetRenderTarget(null);
        myGame.Exit();
    }
}
