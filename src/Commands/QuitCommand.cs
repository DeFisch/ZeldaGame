﻿namespace ZeldaGame
{
    public class QuitCommand : ICommand
    {
        private Game1 myGame;
        public QuitCommand(Game1 game) {
            this.myGame = game;
        }
        public void Execute()
        {
            myGame.Exit();
        }
    }
}