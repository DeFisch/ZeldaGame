using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeldaGame.HUD
{
    public class HUDInventoryCommandLast : ICommand
    {
        private Game1 MyGame;
        private int last = 0;

        public HUDInventoryCommandLast(Game1 myGame)
        {
            MyGame = myGame;
        }

        public void Execute()
        {
            if (MyGame.headUpDisplay.isVisible())
                MyGame.headUpDisplay.HUDInventory.CycleList(last);
        }

    }
}
