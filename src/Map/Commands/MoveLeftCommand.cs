namespace ZeldaGame.Map.Commands;
public class MoveLeftCommand : ICommand {
    private MapHandler map;
    public MoveLeftCommand(MapHandler map) {
        this.map = map;
    }
    public void Execute() {
        map.move_left();
    }
}
