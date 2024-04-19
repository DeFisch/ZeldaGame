using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace ZeldaGame.HUD.Commands
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
            if (Globals.gameStateScreenHandler.CurrentGameState != GameState.Playing) // If the game is paused, don't display the HUD
                return;
            Globals.audioLoader.PlayDisregardMute("Pause");
            if (Globals.gameStateScreenHandler.IsPaused())
                Globals.gameStateScreenHandler.ResumeGame();
            else
                Globals.gameStateScreenHandler.PauseGame(GameState.HUD);
            MyGame.headUpDisplay.Display();
        }
    }
}
