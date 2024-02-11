using static ZeldaGame.Player.PlayerStateMachine;

namespace ZeldaGame.Player.Commands {
	public class SetIdleUpSpriteCommand : ICommand {
		private Game1 MyGame;

		// Constructor
		public SetIdleUpSpriteCommand(Game1 myGame) {
			MyGame = myGame;
		}

		public void Execute() {
			MyGame.Link.SetDirection(0);
            MyGame.Link.SetSprite(PlayerSpriteFactory.Instance.CreateIdleSprite(Direction.Up));
            MyGame.Link.Idle();
		}
	}
}
