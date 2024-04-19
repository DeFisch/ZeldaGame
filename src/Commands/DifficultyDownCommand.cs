
using System.Diagnostics;

namespace ZeldaGame.Commands;

public class DifficultyDownCommand : ICommand
{
    private Game1 game;
    public DifficultyDownCommand(Game1 game)
    {
        this.game = game;
    }
    public void Execute()
    {
        Debug.WriteLine("Difficulty Down Command");
        game.level--;
        if (game.level < 0)
        {
            game.level = 4;
        }
    }
}