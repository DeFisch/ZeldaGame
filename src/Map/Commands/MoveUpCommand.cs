namespace ZeldaGame.Map.Commands;
public class MoveUpCommand : ICommand {
    private MapHandler map;
    public MoveUpCommand(MapHandler map) {
        this.map = map;
    }
    public void Execute() {
        if (Globals.gameStateScreenHandler.CurrentGameState == GameState.Playing) {
            map.move_up();
        }
    }
}
