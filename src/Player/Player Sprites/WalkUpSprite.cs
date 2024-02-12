﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class WalkUpSprite : ISprite
{
	private Texture2D sprite;
	private Rectangle srcRectangle;
	private Rectangle destRectangle;

	public int currentFrame;
	public int totalFrames;

	// Constructor
	public WalkUpSprite(Texture2D sprite) {
		srcRectangle = new Rectangle();
		destRectangle = new Rectangle();
		this.sprite = sprite;
		currentFrame = 0;
		totalFrames = 12;
	}

	public void Draw(SpriteBatch spriteBatch, Vector2 location) {

		switch (currentFrame) {
			case 0:
				srcRectangle = new Rectangle(69, 11, 16, 16);
				break;
			case 6:
				srcRectangle = new Rectangle(86, 11, 16, 16);
				break;
		}

		destRectangle = new Rectangle((int)location.X, (int)location.Y, srcRectangle.Width * 2, srcRectangle.Height * 2);
		SpriteEffects effect = SpriteEffects.None;

		spriteBatch.Draw(sprite, destRectangle, srcRectangle, Color.White, rotation: 0, new Vector2(0, 0), effects: effect, 1);
	}

	public void Update() {
		currentFrame = (currentFrame + 1) % totalFrames;
	}

}