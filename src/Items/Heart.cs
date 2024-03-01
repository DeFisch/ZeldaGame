using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeldaGame.Items {
	public class Heart : IItemSprite {
		private int counter = 0;
		private Texture2D texture;
		private Vector2 pos;
        private Rectangle sourceRectangle_1;
        private Rectangle sourceRectangle_2;
        private Rectangle destinationRectangle_1;
        private Rectangle destinationRectangle_2;

        public Heart(Texture2D texture, Vector2 pos) {
			this.texture = texture;
			this.pos = pos;

		}

		public void Draw(SpriteBatch spriteBatch, Vector2 location, Color color, Vector2 scale) {
            int scaled_x = 113 + ((int)pos.X * 51);
            int scaled_y = 113 + ((int)pos.Y * 51);
            sourceRectangle_1 = new Rectangle(0, 0, 7, 8);
			destinationRectangle_1 = new Rectangle(scaled_x, scaled_y, (int)(sourceRectangle_1.Width * scale.X), (int)(sourceRectangle_1.Height * scale.Y));

			sourceRectangle_2 = new Rectangle(0, 8, 7, 8);
			destinationRectangle_2 = new Rectangle(scaled_x, scaled_y, (int)(sourceRectangle_2.Width * scale.X), (int)(sourceRectangle_2.Height * scale.Y));

			if (counter <= 10) {
				counter++;
				spriteBatch.Draw(texture, destinationRectangle_1, sourceRectangle_1, color);
				Console.WriteLine("Red");
			}
			if (counter > 10 && counter <= 20) {
				counter++;
				spriteBatch.Draw(texture, destinationRectangle_2, sourceRectangle_2, color);

				Console.WriteLine("Blue");
			}
			if (counter > 20) {
				counter = 0;
			}
		}

		public void Update() {

		}

        public Rectangle GetHitBox()
        {
            return destinationRectangle_1;
        }

        public void ItemAction()
        {

        }
    }
}
