using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeldaGame.HUD
{
    public class DisplayHUDCommand : ICommand
    {
        private Game1 MyGame;

        public DisplayHUDCommand(Game1 game)
        {
            MyGame = game;
        }

        public void Execute()
        {
            if (MyGame.IsPaused())
                MyGame.ResumeGame();
            else
                MyGame.PauseGame();
            MyGame.headUpDisplay.Display();
        }
    }
}
