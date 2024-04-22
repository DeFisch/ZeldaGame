using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;

namespace ZeldaGame.Player;
public class WalkDownSprite : PlayerSprite {
	private readonly Texture2D sprite;

    private static int currentFrame = 0;
    private readonly int totalFrames = 2;
    private static int frameID = 0;
    private readonly int frameRate = 8;

	private Rectangle srcRectangle;

    public WalkDownSprite(Texture2D sprite) : base()
	{
		this.sprite = sprite;
	}

	public override void Draw(SpriteBatch spriteBatch, Vector2 location, Color color)
	{
        srcRectangle = new Rectangle(1 + (17 * currentFrame), 11, 16, 16);
		destRectangle = new Rectangle((int)location.X, (int)location.Y, (int)(srcRectangle.Width * Globals.scale.X), (int)(srcRectangle.Height * Globals.scale.Y));
        SpriteEffects effect = SpriteEffects.None;
        spriteBatch.Draw(sprite, destRectangle, srcRectangle, color, rotation: 0, new Vector2(0, 0), effects: effect, 1);
	}

	public override void Update() {
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
}
