using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeldaGame.Player.Commands {
	public class TakeDamageCommand : ICommand {
		private Game1 myGame;

		public TakeDamageCommand(Game1 myGame) {
			this.myGame = myGame;
		}

		public void Execute() {
			myGame.Link = new HurtPlayer(myGame.Link, myGame);
		}
	}
}
