using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeldaGame.HUD.Commands
{
    public class HUDInventoryCommandNext : ICommand
    {
        private Game1 MyGame;
        private int next = 1;

        public HUDInventoryCommandNext(Game1 myGame)
        {
            MyGame = myGame;
        }

        public void Execute()
        {
            if (MyGame.headUpDisplay.isVisible())
                MyGame.headUpDisplay.HUDInventory.CycleList(next);
        }

    }
}
