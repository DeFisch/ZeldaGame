using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ZeldaGame.NPCs;

public class Zelda : INPC {
	private Texture2D texture;
	private Vector2 position;
	private int currentFrame = 0;
	private int totalFrames = 2;
	private static int[,] character_sprites = new int[,] { { 1, 42, 16, 16 }, { 18, 42, 16, 16 } };
	private int switchFrameDelay = 15;
	private int frameCounter = 0;

	public Zelda(Texture2D texture, Vector2 position) {
		this.texture = texture;
		this.position = position;
	}

	public void Draw(SpriteBatch spriteBatch, Vector2 scale) {
		int sprite_id = currentFrame % 2;
		Rectangle sourceRectangle = new Rectangle(character_sprites[sprite_id, 0], character_sprites[sprite_id, 1], character_sprites[sprite_id, 2], character_sprites[sprite_id, 3]);
		Rectangle destinationRectangle = new Rectangle((int)position.X, (int)position.Y, (int)(sourceRectangle.Width * scale.X), (int)(sourceRectangle.Height * scale.Y));
		spriteBatch.Draw(texture, destinationRectangle, sourceRectangle, Color.White);
	}

	public void Update() {
		frameCounter++;
		if (frameCounter >= switchFrameDelay) {
			currentFrame++;
			if (currentFrame >= totalFrames) {
				currentFrame = 0;
			}
			frameCounter = 0;
		}
	}
}