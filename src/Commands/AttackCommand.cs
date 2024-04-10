

namespace ZeldaGame.Player.Commands {
	public class AttackCommand : ICommand {
		private Game1 MyGame;

		public AttackCommand(Game1 myGame) {
			MyGame = myGame;
		}

		public void Execute() {
			MyGame.Link.Attack();
		}
	}
}
