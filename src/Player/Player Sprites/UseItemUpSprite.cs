using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ZeldaGame.Player;

public class UseItemUpSprite : IPlayerSprite {
	private Texture2D Sprite;

	public bool isPlaying;
	public int currentFrame;
	public int totalFrames;
	public int timesLooped;

	// Constructor
	public UseItemUpSprite(Texture2D sprite) {
		Sprite = sprite;
		currentFrame = 0;
		totalFrames = 12;
		timesLooped = 0;
		isPlaying = true;
	}

	public void Draw(SpriteBatch spriteBatch, Vector2 location, Color color) {

		Rectangle sourceRectangle = new Rectangle(141, 11, 16, 16);
		Rectangle destinationRectangle = new Rectangle((int)location.X, (int)location.Y, sourceRectangle.Width * 2, sourceRectangle.Height * 2);
		SpriteEffects effect = SpriteEffects.None;
		spriteBatch.Draw(Sprite, destinationRectangle, sourceRectangle, color, rotation: 0, new Vector2(0, 0), effects: effect, 1);
	}

	public void Update() {
		if (isPlaying) {
			currentFrame++;
			if (currentFrame == totalFrames) {
				currentFrame = 0;
				timesLooped++;
			}

			if (timesLooped > 0) {
				Pause();
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