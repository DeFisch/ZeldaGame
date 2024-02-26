using ZeldaGame;

namespace ZeldaGame.NPCs;
public class PreviousNPCCommand : ICommand {
	private Game1 myGame;
	private int previous = 0;

	public PreviousNPCCommand(Game1 game) {
		this.myGame = game;
	}

	public void Execute() {
		myGame.NPCFactory.cycleList(previous);
	}
}
