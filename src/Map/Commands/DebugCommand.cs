
namespace ZeldaGame.Map;

public class DebugCommand : ICommand
{
    private MapHandler mapHandler;
    public DebugCommand(MapHandler mapHandler)
    {
        this.mapHandler = mapHandler;
    }
    public void Execute()
    {
        mapHandler.Debug = !mapHandler.Debug;
    }
}