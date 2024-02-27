using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;

public class WalkDownSprite : IPlayerSprite {
	private readonly Texture2D sprite;
	private bool isPlaying;

    private static int currentFrame = 0;
    private readonly int totalFrames = 2;
    private static int frameID = 0;
    private readonly int frameRate = 8;

    public static Rectangle destRectangle;

    public WalkDownSprite(Texture2D sprite)
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
        Rectangle srcRectangle = new Rectangle(1 + (17 * currentFrame), 11, 16, 16);
		destRectangle = new Rectangle((int)location.X, (int)location.Y, (int)(srcRectangle.Width * scale.X), (int)(srcRectangle.Height * scale.Y));
        SpriteEffects effect = SpriteEffects.None;

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
