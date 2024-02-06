using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeldaGame.Player {
	public interface IPlayer {
		public void Draw(SpriteBatch spriteBatch);
		public void Update();
	}
}