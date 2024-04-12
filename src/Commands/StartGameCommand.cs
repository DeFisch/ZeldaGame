

using Microsoft.Xna.Framework.Input;
using ZeldaGame.Player;
using ZeldaGame.Player.Commands;

namespace ZeldaGame.Commands;
    public class StartGameCommand : ICommand
    {
        private Game1 myGame;
        public StartGameCommand(Game1 game) {
            this.myGame = game;
        }
    public void Execute()
        {
            if (Globals.gameStateScreenHandler.CurrentGameState == GameState.GameOver 
            || Globals.gameStateScreenHandler.CurrentGameState == GameState.TitleScreen)
            {
                Globals.gameStateScreenHandler.CurrentGameState = GameState.Playing;
                Globals.audioLoader.PlayBGM();
                myGame.keyboardController.UnregisterPressKey(Keys.Up);
                myGame.keyboardController.UnregisterPressKey(Keys.Down);
                // myGame.keyboardController.RegisterHoldKey(Keys.Up, new SetWalkSpriteCommand(myGame, 0));
                // myGame.keyboardController.RegisterHoldKey(Keys.Down, new SetWalkSpriteCommand(myGame, 2));
            }else if (Globals.gameStateScreenHandler.CurrentGameState == GameState.HUD)
            {
                Globals.gameStateScreenHandler.ResumeGame();
                myGame.headUpDisplay.Display();
            }
        }
    }
