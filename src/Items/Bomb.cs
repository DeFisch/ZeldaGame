using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ZeldaGame.Globals;

namespace ZeldaGame.Items {
	public class Bomb : IItemSprite {
		private Texture2D texture;
		private Vector2 pos;
		private Rectangle sourceRectangle;
		private Rectangle destinationRectangle;
		public String id;
		public Bomb(Texture2D texture, Vector2 pos) {
			this.texture = texture;
			this.pos = pos;
			this.id = "Bomb";
		}

		public void Draw(SpriteBatch spriteBatch, Vector2 location, Color color) {
			int scaled_x = ((int)(scale.X * (16 * pos.X + 32)));
			int scaled_y = ((int)(scale.Y * (16 * pos.Y + 32 + 56)));
			sourceRectangle = new Rectangle(136, 0, 8, 16);
			destinationRectangle = new Rectangle(scaled_x, scaled_y, (int)(sourceRectangle.Width * scale.X), (int)(sourceRectangle.Height * scale.Y));
			spriteBatch.Draw(texture, destinationRectangle, sourceRectangle, color);
		}

		public void Update() {

		}
        public Rectangle GetHitBox()
        {
            return destinationRectangle;
        }

		public String GetID() {
			return id;
		}
		public void OnCollision(Rectangle intersect)
        {

        }
    }
}
