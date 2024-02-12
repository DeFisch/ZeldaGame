using ZeldaGame;

namespace ZeldaGame.NPCs;
    public class PreviousNPCCommand : ICommand
    {
        private Game1 MyGame;
        private int previous = 0;

        public PreviousNPCCommand(Game1 myGame)
        {
            MyGame = myGame;
        }

        public void Execute()
        {
            MyGame.NPCFactory.cycleList(previous);
        }
    }
