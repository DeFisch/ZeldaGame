using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeldaGame.Items {
	public class Bow : IItemSprite {
		private Texture2D texture;
		private Vector2 pos;
		private Rectangle sourceRectangle;
		private Rectangle destinationRectangle;

		public Bow(Texture2D texture, Vector2 pos) {
			this.texture = texture;
			this.pos = pos;

		}

		public void Draw(SpriteBatch spriteBatch, Vector2 location, Color color, Vector2 scale) {
            int scaled_x = 105 + ((int)pos.X * 51);
            int scaled_y = 120 + ((int)pos.Y * 51);
            sourceRectangle = new Rectangle(144, 0, 8, 16);
            destinationRectangle = new Rectangle(scaled_x, scaled_y, (int)(sourceRectangle.Width * scale.X), (int)(sourceRectangle.Height * scale.Y));
            spriteBatch.Draw(texture, destinationRectangle, sourceRectangle, color);
		}

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
