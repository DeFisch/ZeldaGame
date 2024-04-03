namespace ZeldaGame.Map.Commands;
public class MoveLeftCommand : ICommand {
    private MapHandler map;
    public MoveLeftCommand(MapHandler map) {
        this.map = map;
    }
    public void Execute() {
        if (Globals.gameStateScreenHandler.CurrentGameState == GameState.Playing) {
            map.move_left();
        }
    }
}
