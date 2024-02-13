using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeldaGame.Items {
	public class Key : ISprite {
		private Texture2D texture;
		private Vector2 pos;
		public Key(Texture2D texture, Vector2 pos) {
			this.texture = texture;
			this.pos = pos;

		}

		public void Draw(SpriteBatch spriteBatch, Vector2 location) {
			Rectangle sourceRectangle = new Rectangle(240, 0, 7, 16);
			Rectangle destinationRectangle = new Rectangle((int)pos.X, (int)pos.Y, 14, 42);
			spriteBatch.Draw(texture, destinationRectangle, sourceRectangle, Color.White);
		}

		public void Pause() {
			throw new NotImplementedException();
		}

		public void Play() {
			throw new NotImplementedException();
		}

		public void PlayToggle() {
			throw new NotImplementedException();
		}

		public void Update() {

		}
	}
}
