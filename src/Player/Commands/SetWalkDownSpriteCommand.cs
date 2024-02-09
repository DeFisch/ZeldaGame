namespace ZeldaGame.Player.Commands {
	public class SetWalkDownSpriteCommand : ICommand {
		private Game1 MyGame;

		// Constructor
		public SetWalkDownSpriteCommand(Game1 myGame) {
			MyGame = myGame;
		}

		public void Execute() {
			MyGame.Link.SetDirection(2);
			MyGame.Link.SetSprite(PlayerSpriteFactory.Instance.CreateWalkDownSprite());
			MyGame.Link.Walk();
		}
	}
}
