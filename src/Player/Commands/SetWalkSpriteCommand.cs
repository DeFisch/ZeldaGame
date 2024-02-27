using static ZeldaGame.Player.PlayerStateMachine;

namespace ZeldaGame.Player.Commands {
	public class SetWalkSpriteCommand : ICommand {
		private Game1 MyGame;
		private Direction direction;

		public SetWalkSpriteCommand(Game1 myGame, int direction) {
			MyGame = myGame;
			this.direction = (Direction)direction;
		}

		public void Execute() {
			MyGame.Link.SetDirection(direction);
			MyGame.Link.Walk();
		}
	}
}