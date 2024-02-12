using ZeldaGame;

namespace ZeldaGame.NPCs;

public class NextNPCCommand : ICommand
{
    private Game1 MyGame;
    private int next = 1;

    public  NextNPCCommand (Game1 myGame)
    {
        MyGame = myGame;
    }

    public void Execute()
    {
       MyGame.NPCFactory.cycleList(next);
    }
}
