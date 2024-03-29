﻿

namespace ZeldaGame;
    public class StartGameCommand : ICommand
    {
        private Game1 myGame;
        public StartGameCommand(Game1 game) {
            this.myGame = game;
        }
    public void Execute()
        {
            if(Globals.gameStateScreenHandler.CurrentGameState == GameState.Playing)
            {
                return;
            }
            Globals.gameStateScreenHandler.CurrentGameState = GameState.Playing;
            Globals.audioLoader.PlayBGM();
        }
    }