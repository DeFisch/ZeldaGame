﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;

namespace ZeldaGame.Player;
public class WalkLeftSprite : IPlayerSprite {
	private Texture2D sprite;
	private static Rectangle destRectangle;
	private bool isPlaying;

    private static int currentFrame = 0;
    private readonly int totalFrames = 2;
    private static int frameID = 0;
    private readonly int frameRate = 8;

    public WalkLeftSprite(Texture2D sprite)
    {
		isPlaying = false;
		this.sprite = sprite;
    }

    public Rectangle GetHitBox()
    {
        return destRectangle;
    }

    public void Draw(SpriteBatch spriteBatch, Vector2 location, Color color, Vector2 scale)
	{
        Rectangle srcRectangle = new Rectangle(35 + (17 * currentFrame), 11, 16, 16);
        destRectangle = new Rectangle((int)location.X, (int)location.Y, (int)(srcRectangle.Width * scale.X), (int)(srcRectangle.Height * scale.Y));
		SpriteEffects effect = SpriteEffects.FlipHorizontally;

		spriteBatch.Draw(sprite, destRectangle, srcRectangle, color, rotation: 0, new Vector2(0, 0), effects: effect, 1);
	}

	public void Update() {
        if (isPlaying)
        {
            frameID++;
            if (frameID % frameRate == 0)
            {
                currentFrame++;
                frameID = 0;
            }

            if (currentFrame == totalFrames)
            {
                currentFrame = 0;
            }
        }
    }

	public void Play() {
		isPlaying = true;
	}

	public void Pause() {
		isPlaying = false;
	}
}
