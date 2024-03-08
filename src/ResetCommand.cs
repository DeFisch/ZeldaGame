﻿using System.Collections.Generic;
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
            MyGame.map.Reset();
            MyGame.Link.Reset();
            MyGame.itemFactory.Reset();
            MyGame.enemyFactory.Reset();
            MyGame.blockSpriteFactory.Reset();
        }
    }
}
