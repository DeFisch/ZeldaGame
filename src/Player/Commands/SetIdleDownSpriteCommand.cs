namespace ZeldaGame.Player.Commands {
	public class SetIdleDownSpriteCommand : ICommand {
		private Game1 MyGame;

		// Constructor
		public SetIdleDownSpriteCommand(Game1 myGame) {
			MyGame = myGame;
		}

		public void Execute() {
			MyGame.Link.SetDirection(2);
			MyGame.Link.SetSprite(PlayerSpriteFactory.Instance.CreateIdleDownPlayer());
			MyGame.Link.Idle();
		}
	}
}
