using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeldaGame.Items
{
    public class YellowRuby : ISprite
    {
        private int counter = 0;
        private Texture2D texture;
        private Vector2 pos;
        public YellowRuby(Texture2D texture, Vector2 pos)
        {
            this.texture = texture;
            this.pos = pos;

        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {
            Rectangle sourceRectangle = new Rectangle(72, 16, 8, 16);
            Rectangle destinationRectangle = new Rectangle((int)pos.X, (int)pos.Y, 16, 42);
            spriteBatch.Draw(texture, destinationRectangle, sourceRectangle, Color.White);

            Rectangle sourceRectangle_1 = new Rectangle(72, 16, 8, 16);
            Rectangle destinationRectangle_1 = new Rectangle((int)pos.X, (int)pos.Y, 16, 42);

            Rectangle sourceRectangle_2 = new Rectangle(72, 0, 8, 16);
            Rectangle destinationRectangle_2 = new Rectangle((int)pos.X, (int)pos.Y, 16, 42);

            if (counter <= 10)
            {
                counter++;
                spriteBatch.Draw(texture, destinationRectangle_1, sourceRectangle_1, Color.White);
            }
            if (counter > 10 && counter <= 20)
            {
                counter++;
                spriteBatch.Draw(texture, destinationRectangle_2, sourceRectangle_2, Color.White);
            }
            if (counter > 20)
            {
                counter = 0;
            }
        }

		public void PlayToggle() {
			throw new NotImplementedException();
		}

		public void Update()
        {

        }
    }
}
