using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeldaGame.Items {
	public class HeartContainer : ISprite {
		private Texture2D texture;
		private Vector2 pos;
		public HeartContainer(Texture2D texture, Vector2 pos) {
			this.texture = texture;
			this.pos = pos;

		}

		public void Draw(SpriteBatch spriteBatch, Vector2 location, Color color) {
			Rectangle sourceRectangle = new Rectangle(24, 1, 14, 14);
			Rectangle destinationRectangle = new Rectangle((int)pos.X, (int)pos.Y, 28, 28);
			spriteBatch.Draw(texture, destinationRectangle, sourceRectangle, color);
		}

		public void Update() {

		}
	}
}
