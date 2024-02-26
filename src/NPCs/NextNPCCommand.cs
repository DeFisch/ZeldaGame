using ZeldaGame;

namespace ZeldaGame.NPCs;

public class NextNPCCommand : ICommand {
	private Game1 myGame;
	private int next = 1;

	public NextNPCCommand(Game1 game) {
		this.myGame = game;
	}

	public void Execute() {
		myGame.NPCFactory.cycleList(next);
	}
}
