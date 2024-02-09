namespace ZeldaGame.Player.Commands {
	public class SetWalkLeftSpriteCommand : ICommand {
		private Game1 MyGame;

		// Constructor
		public SetWalkLeftSpriteCommand(Game1 myGame) {
			MyGame = myGame;
		}

		public void Execute() {
			MyGame.Link.SetDirection(1);
			MyGame.Link.SetSprite(PlayerSpriteFactory.Instance.CreateWalkLeftSprite());
			MyGame.Link.Walk();
		}
	}
}
