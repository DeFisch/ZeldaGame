
using System.Diagnostics;

namespace ZeldaGame.Commands;

public class DifficultyUpCommand : ICommand
{
    private Game1 game;
    public DifficultyUpCommand(Game1 game)
    {
        this.game = game;
    }
    public void Execute()
    {
        Debug.WriteLine("Difficulty Up Command");
        game.level++;
        if (game.level > 4)
        {
            game.level = 0;
        }
    }
}