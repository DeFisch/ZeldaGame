using static ZeldaGame.Player.PlayerStateMachine;

namespace ZeldaGame.Player.Commands {
	public class SetIdleLeftSpriteCommand : ICommand {
		private Game1 MyGame;

		// Constructor
		public SetIdleLeftSpriteCommand(Game1 myGame) {
			MyGame = myGame;
		}

		public void Execute() {
			MyGame.Link.SetDirection(1);
            MyGame.Link.SetSprite(PlayerSpriteFactory.Instance.CreateIdleSprite(Direction.Left));
			MyGame.Link.Idle();
		}
	}
}
