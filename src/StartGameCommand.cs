using Microsoft.Xna.Framework.Graphics;

namespace ZeldaGame;
    public class StartGameCommand : ICommand
    {
        private Game1 myGame;
        public StartGameCommand(Game1 game) {
            this.myGame = game;
        }
    public void Execute()
        {
            //Add code to switch to initial map
        }
    }
