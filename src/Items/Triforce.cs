using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeldaGame.Items {
	public class Triforce : ISprite {
		private int counter = 0;
		private Texture2D texture;
		private Vector2 pos;
		public Triforce(Texture2D texture, Vector2 pos) {
			this.texture = texture;
			this.pos = pos;

		}

		public void Draw(SpriteBatch spriteBatch, Vector2 location, Color color) {

			Rectangle sourceRectangle_1 = new Rectangle(274, 2, 12, 10);
			Rectangle destinationRectangle_1 = new Rectangle((int)pos.X, (int)pos.Y, 24, 20);

			Rectangle sourceRectangle_2 = new Rectangle(274, 19, 12, 10);
			Rectangle destinationRectangle_2 = new Rectangle((int)pos.X, (int)pos.Y, 24, 20);

			if (counter <= 10) {
				counter++;
				spriteBatch.Draw(texture, destinationRectangle_1, sourceRectangle_1, color);
			}
			if (counter > 10 && counter <= 20) {
				counter++;
				spriteBatch.Draw(texture, destinationRectangle_2, sourceRectangle_2, color);
			}
			if (counter > 20) {
				counter = 0;
			}
		}

		public void Update() {

		}
	}
}
