using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ZeldaGame.NPCs;

public class Fairy : INPC {
	private Texture2D texture;
	private Vector2 position;
	private Rectangle sourceRectangle;
	private Rectangle destinationRectangle;
	private string collisionMessage = "Fairy";

    public Fairy(Texture2D texture, Vector2 position) {
		this.texture = texture;
		this.position = position;
	}

	public void Draw(SpriteBatch spriteBatch, Vector2 scale) {
		sourceRectangle = new Rectangle(186, 11, 8, 16);
		destinationRectangle = new Rectangle((int)position.X, (int)position.Y, (int)(sourceRectangle.Width * scale.X), (int)(sourceRectangle.Height * scale.Y));
		spriteBatch.Draw(texture, destinationRectangle, sourceRectangle, Color.White);
	}

    public void Update() {

	}
    public Rectangle GetNPCHitBox()
    {
        return destinationRectangle;
    }

    public string GetCollisionMessage()
    {
		return collisionMessage;
    }
}