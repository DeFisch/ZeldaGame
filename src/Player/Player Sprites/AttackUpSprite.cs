using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ZeldaGame.Player;

public class AttackUpSprite : ISprite {
	private Texture2D Sprite;

	public bool isPlaying;
	public int currentFrame;
	public int totalFrames;
	public int timesLooped;

	// Constructor
	public AttackUpSprite(Texture2D sprite) {
		Sprite = sprite;
		currentFrame = 0;
		totalFrames = 12;
		timesLooped = 0;
		isPlaying = true;
	}

	public void Draw(SpriteBatch spriteBatch, Vector2 location) {
		if (isPlaying) {
			Rectangle sourceRectangle = new Rectangle(141, 11, 16, 16);
			Rectangle destinationRectangle = new Rectangle((int)location.X, (int)location.Y, sourceRectangle.Width * 2, sourceRectangle.Height * 2);
			SpriteEffects effect = SpriteEffects.None;
			spriteBatch.Draw(Sprite, destinationRectangle, sourceRectangle, Color.White, rotation: 0, new Vector2(0, 0), effects: effect, 1);
		}
	}

	public void Update() {
		currentFrame++;
		if (currentFrame == totalFrames) {
			currentFrame = 0;
			timesLooped++;
		}
	}

	public void Play() {
		isPlaying = true;
	}

	public void Pause() {
		isPlaying = false;
	}
}