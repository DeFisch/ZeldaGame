﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

/*
 *  SetSprite1 class for non-moving, non-animated sprite
 */
public class IdleRightSprite : ISprite
{
    private Texture2D Sprite;

	public int currentFrame;
	public int totalFrames;

	// Constructor
	public IdleRightSprite(Texture2D sprite)
	{
        Sprite = sprite;

		currentFrame = 0;
		totalFrames = 1;
	}

    public void Draw(SpriteBatch spriteBatch, Vector2 location)
    {
		Rectangle sourceRectangle = new Rectangle(35, 11, 16, 16);
		Rectangle destinationRectangle = new Rectangle((int)location.X, (int)location.Y, sourceRectangle.Width * 2, sourceRectangle.Height * 2);
		SpriteEffects effect = SpriteEffects.None;
		//spriteBatch.Begin();
		spriteBatch.Draw(Sprite, destinationRectangle, sourceRectangle, Color.White, rotation: 0, new Vector2(0, 0), effects: effect, 1);
		//spriteBatch.End();
	}

	public void Update()
    {
		currentFrame = (currentFrame + 1) % totalFrames;
	}

}