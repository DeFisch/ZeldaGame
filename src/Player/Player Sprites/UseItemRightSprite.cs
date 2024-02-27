using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class UseItemRightSprite : IPlayerSprite {
	private Texture2D Sprite;

	private bool isPlaying;
	private int currentFrame;
	private int totalFrames;
	private int timesLooped;

    private Rectangle destinationRectangle;

    // Constructor
    public UseItemRightSprite(Texture2D sprite) {
		isPlaying = true;
		Sprite = sprite;
		currentFrame = 0;
		totalFrames = 12;
		timesLooped = 0;
	}

    public Rectangle GetHitBox()
    {
        return destinationRectangle;
    }

    public void Draw(SpriteBatch spriteBatch, Vector2 location, Color color, Vector2 scale) {

		Rectangle sourceRectangle = new Rectangle(124, 11, 16, 16);
		destinationRectangle = new Rectangle((int)location.X, (int)location.Y, (int)(sourceRectangle.Width * scale.X), (int)(sourceRectangle.Height * scale.Y));
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
		}
	}
	public void Play() {
		isPlaying = true;
	}

	public void Pause() {
		isPlaying = false;
	}
}