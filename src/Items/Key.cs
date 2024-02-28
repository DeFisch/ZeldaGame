using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeldaGame.Items {
	public class Key : IItemSprite {
		private Texture2D texture;
		private Vector2 pos;
        private Rectangle sourceRectangle;
        private Rectangle destinationRectangle;
        public Key(Texture2D texture, Vector2 pos) {
			this.texture = texture;
			this.pos = pos;

		}

		public void Draw(SpriteBatch spriteBatch, Vector2 location, Color color, Vector2 scale) {
			sourceRectangle = new Rectangle(240, 0, 7, 16);
			destinationRectangle = new Rectangle((int)pos.X, (int)pos.Y, (int)(sourceRectangle.Width * scale.X), (int)(sourceRectangle.Height * scale.Y));
			spriteBatch.Draw(texture, destinationRectangle, sourceRectangle, color);
		}

		/*public void PlayToggle() { //What is this lol - Olivia
			throw new NotImplementedException();
		}*/

		public void Update() {

		}
        public Rectangle GetHitBox()
        {
            return destinationRectangle;
        }

        public void ItemAction()
        {

        }
    }
}
