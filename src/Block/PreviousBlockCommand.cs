using ZeldaGame;

namespace ZeldaGame.Block {
	public class PreviousBlockCommand : ICommand {
		private Game1 MyGame;
		private int previous = 0;

		// Constructor
		public PreviousBlockCommand(Game1 myGame) {
			MyGame = myGame;
		}

		public void Execute() {
			MyGame.blockSpriteFactory.cycleList(previous);
		}
	}
}
