using static ZeldaGame.Globals;

namespace ZeldaGame.Player.Commands {
	public class UseItemCommand : ICommand {
		private Game1 MyGame;
		private PlayerProjectiles item;   // integer value indicates what item to use, starting with index 0

		public UseItemCommand(Game1 myGame, PlayerProjectiles item) {
			MyGame = myGame;
			this.item = item;
		}

		public void Execute() {
			MyGame.Link.UseItem(item);
		}
	}
}
