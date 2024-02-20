using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ZeldaGame
{
    public class ResetCommand: ICommand
    {
        private Game1 MyGame;

        public ResetCommand(Game1 game)
        {
            MyGame = game;
        }

        public void Execute()
        {
            MyGame.blockSpriteFactory.Reset();
            MyGame.itemFactory.Reset();
            MyGame.NPCFactory.Reset();
            MyGame.Link.Reset();
            MyGame.enemyFactory.Reset();
        }
    }
}
