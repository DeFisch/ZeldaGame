using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Sprint0.Block {
	public class Water : IBlock {
		private Texture2D texture;
		private Vector2 position;

		public Water(Texture2D texture, Vector2 position) {
			this.texture = texture;
			this.position = position;
		}

		public void Update() {

		}

		public void Draw(SpriteBatch spriteBatch, Vector2 scale) {
			Rectangle sourceRectangle = new Rectangle(563, 49, 16, 16);
			Rectangle destinationRectangle = new Rectangle((int)position.X, (int)position.Y, (int)(sourceRectangle.Width * scale.X), (int)(sourceRectangle.Height * scale.Y));
			spriteBatch.Draw(texture, destinationRectangle, sourceRectangle, Color.White);
		}
	}
}
