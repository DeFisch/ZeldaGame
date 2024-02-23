namespace ZeldaGame.Map.Commands;
public class MoveRightCommand : ICommand {
    private MapHandler map;
    public MoveRightCommand(MapHandler map) {
        this.map = map;
    }
    public void Execute() {
        map.move_right();
    }
}
