using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeldaGame.Items {
	public class FairyItem : ISprite {
		private int counter = 0;
		private Texture2D texture;
		private Vector2 pos;
		public FairyItem(Texture2D texture, Vector2 pos) {
			this.texture = texture;
			this.pos = pos;
		}

		public void Draw(SpriteBatch spriteBatch, Vector2 location, Color color, Vector2 scale) {

			Rectangle sourceRectangle_1 = new Rectangle(39, 0, 9, 16);
			Rectangle destinationRectangle_1 = new Rectangle((int)pos.X, (int)pos.Y, (int)(sourceRectangle_1.Width * scale.X), (int)(sourceRectangle_1.Height * scale.Y));

			Rectangle sourceRectangle_2 = new Rectangle(47, 0, 9, 16);
			Rectangle destinationRectangle_2 = new Rectangle((int)pos.X, (int)pos.Y, (int)(sourceRectangle_2.Width * scale.X), (int)(sourceRectangle_2.Height * scale.Y));

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
