using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class WalkLeftSprite : ISprite
{
	private Texture2D sprite;
	private Rectangle srcRectangle;
	private Rectangle destRectangle;
	private bool isPlaying;

	public int currentFrame;
	public int totalFrames;

	// Constructor
	public WalkLeftSprite(Texture2D sprite) {
		isPlaying = true;
		srcRectangle = new Rectangle();
		destRectangle = new Rectangle();
		this.sprite = sprite;
		currentFrame = 0;
		totalFrames = 12;
	}

	public void Draw(SpriteBatch spriteBatch, Vector2 location) {

		switch (currentFrame) {
			case 0:
				srcRectangle = new Rectangle(35, 11, 16, 16);
				break;
			case 6:
				srcRectangle = new Rectangle(52, 11, 16, 16);
				break;
		}

		destRectangle = new Rectangle((int)location.X, (int)location.Y, srcRectangle.Width * 2, srcRectangle.Height * 2);
		SpriteEffects effect = SpriteEffects.FlipHorizontally;

		spriteBatch.Draw(sprite, destRectangle, srcRectangle, Color.White, rotation: 0, new Vector2(0, 0), effects: effect, 1);
	}

	public void Update() {
		if (isPlaying) {
			currentFrame = (currentFrame + 1) % totalFrames;
		}
	}

	public void PlayToggle() {
		isPlaying = !isPlaying;
	}
}
