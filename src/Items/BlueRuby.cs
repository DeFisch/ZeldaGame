﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeldaGame.Map;

namespace ZeldaGame.Items {
	public class BlueRuby : IItemSprite {
		private Texture2D texture;
		private Vector2 pos;
		private Rectangle sourceRectangle;
		public Rectangle destinationRectangle;
		public String id;
		
		public BlueRuby(Texture2D texture, Vector2 pos) {
			this.texture = texture;
			this.pos = pos;
			this.id = "BlueRuby";
		}

		public void Draw(SpriteBatch spriteBatch, Vector2 location, Color color, Vector2 scale) {
			int scaled_x = ((int)(scale.X * (16 * pos.X + 32)));
			int scaled_y = ((int)(scale.Y * (16 * pos.Y + 32 + 56)));
			sourceRectangle = new Rectangle(72, 16, 8, 16);
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
