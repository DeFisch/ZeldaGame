using Microsoft.Xna.Framework;
using ZeldaGame;

/*
 *  QuitCommand class exits myGame
 */
public class QuitCommand : ICommand
{
    Game Game1;
    
    // Constructor
	public QuitCommand(Game1 myGame)
	{
        Game1 = myGame;
    }

    public void Execute()
    {
        Game1.Exit();
    }
}
