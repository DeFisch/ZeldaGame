using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ZeldaGame.Block {
	public interface IBlock {
		public void Update();

		public void Draw(SpriteBatch spriteBatc, Vector2 scale);
	}
}
