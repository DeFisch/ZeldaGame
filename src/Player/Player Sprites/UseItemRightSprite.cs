using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ZeldaGame.Player;
public class UseItemRightSprite : PlayerSprite {
	private Texture2D Sprite;

	private int currentFrame;
	private int totalFrames;
	private int timesLooped;

    public UseItemRightSprite(Texture2D sprite) {
		Sprite = sprite;
		currentFrame = 0;
		totalFrames = 12;
		timesLooped = 0;
	}

    public override void Draw(SpriteBatch spriteBatch, Vector2 location, Color color, Vector2 scale) {

		Rectangle sourceRectangle = new Rectangle(124, 11, 16, 16);
		destRectangle = new Rectangle((int)location.X, (int)location.Y, (int)(sourceRectangle.Width * scale.X), (int)(sourceRectangle.Height * scale.Y));
		SpriteEffects effect = SpriteEffects.None;
		spriteBatch.Draw(Sprite, destRectangle, sourceRectangle, color, rotation: 0, new Vector2(0, 0), effects: effect, 1);
	}

	public override void Update()
	{
		if (isPlaying)
		{
			currentFrame++;
			if (currentFrame == totalFrames)
			{
				currentFrame = 0;
				timesLooped++;
			}
		}
	}
}