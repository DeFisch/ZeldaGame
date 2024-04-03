using Microsoft.Xna.Framework;

namespace ZeldaGame.Commands;

public class PauseCommand : ICommand
{
    private Game1 myGame;
    public PauseCommand(Game1 game)
    {
        this.myGame = game;
    }
    public void Execute()
    {
        if (Globals.gameStateScreenHandler.CurrentGameState == GameState.HUD) // If the game displaying HUD, don't pause
            return;
        Globals.audioLoader.PlayDisregardMute("Pause");
        if (Globals.gameStateScreenHandler.IsPaused())
            Globals.gameStateScreenHandler.ResumeGame();
        else
            Globals.gameStateScreenHandler.PauseGame(GameState.Pause);
    }
}