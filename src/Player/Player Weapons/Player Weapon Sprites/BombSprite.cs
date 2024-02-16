using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static ZeldaGame.Player.PlayerStateMachine;

public class BombSprite : IPlayerProjectile {
	SpriteEffects effect;
	private Texture2D Sprite;
	private bool isActive;

    private Vector2 position;

    private int currentFrame;
	private int totalFrames;
	private int frameRate;
	private int frameID;

	public BombSprite(Texture2D sprite, Vector2 position) {
		isActive = true;
		Sprite = sprite;
		effect = SpriteEffects.None;
		this.position = position;

		currentFrame = 0;
		totalFrames = 4;
		frameID = 0;
		frameRate = 8;
	}

	public Direction GetDirection() {
		// No implementation, filler?
		return Direction.Right;
	}

    public bool IsActive()
    {
        return isActive;
    }

    public void Draw(SpriteBatch spriteBatch) {
		Rectangle sourceRectangle = new Rectangle(129, 185, 8, 16);
		Rectangle destinationRectangle = new Rectangle((int)position.X, (int)position.Y, sourceRectangle.Width * 2, sourceRectangle.Height * 2);
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
			isActive = false;
		}
	}

}
