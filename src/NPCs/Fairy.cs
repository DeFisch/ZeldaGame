using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ZeldaGame.NPCs;

public class Fairy : INPC {
	private Texture2D texture;
	private Vector2 position;

	public Fairy(Texture2D texture, Vector2 position) {
		this.texture = texture;
		this.position = position;
	}

	public void Draw(SpriteBatch spriteBatch) {
		Rectangle sourceRectangle = new Rectangle(186, 11, 7, 16);
		Rectangle destinationRectangle = new Rectangle((int)position.X, (int)position.Y, sourceRectangle.Width * 3, sourceRectangle.Height * 3);
		spriteBatch.Draw(texture, destinationRectangle, sourceRectangle, Color.White);
	}

	public void Update() {

	}

}