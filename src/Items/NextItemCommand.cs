using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeldaGame.Items {
	public class NextItemCommand : ICommand {
		private int lastOrNext = 1;
		private Game1 MyGame;
		public NextItemCommand(Game1 myGame) {
			MyGame = myGame;
		}
		public void Execute() {
			MyGame.itemFactory.Cycle(lastOrNext);
		}
	}
}
