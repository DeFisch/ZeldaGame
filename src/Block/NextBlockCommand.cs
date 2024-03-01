namespace ZeldaGame.Block {
	public class NextBlockCommand : ICommand {
		private Game1 MyGame;
		private int next = 1;

		// Constructor
		public NextBlockCommand(Game1 myGame) {
			MyGame = myGame;
		}

		public void Execute() {
			MyGame.blockSpriteFactory.cycleList(next);
		}
	}
}
