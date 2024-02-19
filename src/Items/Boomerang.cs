using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeldaGame.Items {
	public class Boomerang : ISprite {
		private Texture2D texture;
		private Vector2 pos;

		public Boomerang(Texture2D texture, Vector2 pos) {
			this.texture = texture;
			this.pos = pos;

		}

		public void Draw(SpriteBatch spriteBatch, Vector2 location) {
			Rectangle sourceRectangle = new Rectangle(128, 2, 5, 10);
			Rectangle destinationRectangle = new Rectangle((int)pos.X, (int)pos.Y, 15, 30);
			spriteBatch.Draw(texture, destinationRectangle, sourceRectangle, Color.White);
		}

		public void Update() {

		}
	}
}
