using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static ZeldaGame.Player.PlayerStateMachine;

public class BlueBoomerangSprite : IPlayerProjectile {
	SpriteEffects effect;
	private Texture2D Sprite;
	private Direction direction;

	private int currentFrame;
	private int totalFrames;
	private int frameRate;
	private int frameID;

	public BlueBoomerangSprite(Texture2D sprite, Direction direction) {
		Sprite = sprite;
		effect = SpriteEffects.None;
		this.direction = direction;

		currentFrame = 0;
		totalFrames = 3;
		frameID = 0;
		frameRate = 8;
	}

	public Direction GetDirection() {
		return direction;
	}

	public void Draw(SpriteBatch spriteBatch, Vector2 location) {
		Rectangle sourceRectangle = new Rectangle(91 + (currentFrame * 9), 185, 8, 16);
		Rectangle destinationRectangle = new Rectangle((int)location.X, (int)location.Y, sourceRectangle.Width * 2, sourceRectangle.Height * 2);
		spriteBatch.Draw(Sprite, destinationRectangle, sourceRectangle, Color.White, rotation: 0, new Vector2(0, 0), effects: effect, 1);
	}

	public void Update() {
		frameID++;
		if (frameID % frameRate == 0) {
			currentFrame++;
			frameID = 0;
		}

		if (currentFrame == totalFrames) {
			currentFrame = 0;
		}
	}

}
