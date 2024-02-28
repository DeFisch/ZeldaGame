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
		private Rectangle sourceRectangle_1;
		private Rectangle sourceRectangle_2;
		private Rectangle destinationRectangle_1;
		private Rectangle destinationRectangle_2;
		public YellowRuby(Texture2D texture, Vector2 pos)
		{
			this.texture = texture;
			this.pos = pos;

		}

		public void Draw(SpriteBatch spriteBatch, Vector2 location, Color color, Vector2 scale)
		{

			sourceRectangle_1 = new Rectangle(72, 16, 8, 16);
			destinationRectangle_1 = new Rectangle((int)pos.X, (int)pos.Y, (int)(sourceRectangle_1.Width * scale.X), (int)(sourceRectangle_1.Height * scale.Y));

			sourceRectangle_2 = new Rectangle(72, 0, 8, 16);
			destinationRectangle_2 = new Rectangle((int)pos.X, (int)pos.Y, (int)(sourceRectangle_2.Width * scale.X), (int)(sourceRectangle_2.Height * scale.Y));

			if (counter <= 10)
			{
				counter++;
				spriteBatch.Draw(texture, destinationRectangle_1, sourceRectangle_1, color);
			}
			if (counter > 10 && counter <= 20)
			{
				counter++;
				spriteBatch.Draw(texture, destinationRectangle_2, sourceRectangle_2, color);
			}
			if (counter > 20)
			{
				counter = 0;
			}
		}

		public void Update()
		{

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
