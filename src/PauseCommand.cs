using Microsoft.Xna.Framework;

namespace ZeldaGame
{
    public class PauseCommand : ICommand
    {
        private Game1 myGame;
        public PauseCommand(Game1 game)
        {
            this.myGame = game;
        }
        public void Execute()
        {
            if (myGame.headUpDisplay.isVisible())
                return;
            Globals.audioLoader.PlayDisregardMute("Pause");
            if (myGame.IsPaused())
                myGame.ResumeGame();
            else
                myGame.PauseGame();
        }
    }
}