using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeldaGame.Items {
	public class Clock : ISprite {
		private Texture2D texture;
		private Vector2 pos;

		public Clock(Texture2D texture, Vector2 pos) {
			this.texture = texture;
			this.pos = pos;

		}

		public void Draw(SpriteBatch spriteBatch, Vector2 location) {
			Rectangle sourceRectangle = new Rectangle(58, 0, 11, 16);
			Rectangle destinationRectangle = new Rectangle((int)pos.X, (int)pos.Y, 22, 38);
			spriteBatch.Draw(texture, destinationRectangle, sourceRectangle, Color.White);
		}

		public void Update() {

		}
	}
}
