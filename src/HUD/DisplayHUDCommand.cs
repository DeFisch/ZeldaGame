using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

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
            if (Globals.gameStateScreenHandler.CurrentGameState == GameState.Pause) // If the game is paused, don't display the HUD
                return;
            Globals.audioLoader.PlayDisregardMute("Pause");
            if (MyGame.IsPaused())
                MyGame.ResumeGame();
            else
                MyGame.PauseGame(GameState.HUD);
            MyGame.headUpDisplay.Display();
        }
    }
}
