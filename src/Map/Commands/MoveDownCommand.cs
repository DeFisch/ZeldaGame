namespace ZeldaGame.Map.Commands;
public class MoveDownCommand : ICommand {
    private MapHandler map;
    public MoveDownCommand(MapHandler map) {
        this.map = map;
    }
    public void Execute() {
        if (Globals.gameStateScreenHandler.CurrentGameState == GameState.Playing) {
            map.move_down();
        }
    }
}
