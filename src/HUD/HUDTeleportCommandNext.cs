﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeldaGame.HUD
{
    public class HUDTeleportCommandNext : ICommand
    {
        private Game1 MyGame;
        private int next = 1;

        public HUDTeleportCommandNext(Game1 myGame)
        {
            MyGame = myGame;
        }

        public void Execute()
        {
            if (MyGame.headUpDisplay.isVisible())
                MyGame.headUpDisplay.HUDMapHandler.mapTeleport.CycleList(next);
        }

    }
}
