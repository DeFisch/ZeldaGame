using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeldaGame.Items
{
    public class LastItemCommand : ICommand
    {
        private int lastOrNext = 0;
        private Game1 MyGame;
        public LastItemCommand(Game1 myGame)
        {
            MyGame = myGame;
        }
        public void Execute()
        {
            MyGame.objectFactory.Cycle(lastOrNext);
        }
    }
}

