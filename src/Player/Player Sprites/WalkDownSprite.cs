using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections;
using System.Diagnostics;

public class WalkDownSprite : ISprite {
	private readonly Texture2D sprite;
	private Rectangle srcRectangle;
	private Rectangle destRectangle;
	private bool isPlaying;

    private static int currentFrame = 0;
    private readonly int totalFrames = 2;
    private static int frameID = 0;
    private readonly int frameRate = 8;

    public WalkDownSprite(Texture2D sprite)
	{
		isPlaying = false;
		this.sprite = sprite;
	}

	public void Draw(SpriteBatch spriteBatch, Vector2 location)
	{
		srcRectangle = new Rectangle(1 + (17 * currentFrame), 11, 16, 16);
		destRectangle = new Rectangle((int)location.X, (int)location.Y, srcRectangle.Width * 2, srcRectangle.Height * 2);
		SpriteEffects effect = SpriteEffects.None;

		spriteBatch.Draw(sprite, destRectangle, srcRectangle, Color.White, rotation: 0, new Vector2(0, 0), effects: effect, 1);
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
