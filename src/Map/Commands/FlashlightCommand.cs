
namespace ZeldaGame.Map;

public class FlashlightCommand : ICommand
{
    private Game1 game;
    public FlashlightCommand(Game1 game)
    {
        this.game = game;
    }
    public void Execute()
    {
        game.flashlightMode = !game.flashlightMode;
    }
}