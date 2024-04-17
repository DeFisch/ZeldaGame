using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeldaGame.HUD
{
    public class HUDTeleportCommand : ICommand
    {
        private Game1 MyGame;

        public HUDTeleportCommand(Game1 myGame)
        {
            MyGame = myGame;
        }

        public void Execute()
        {
            if (MyGame.headUpDisplay.isVisible())
                MyGame.headUpDisplay.HUDMapHandler.mapTeleport.Teleport();
        }

    }
}
