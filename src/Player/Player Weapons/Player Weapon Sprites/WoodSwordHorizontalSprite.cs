using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static ZeldaGame.Player.PlayerStateMachine;

public class WoodSwordHorizontalSprite : IPlayerProjectile {
	SpriteEffects effect;
	private Texture2D Sprite;
	private Direction direction;
	private bool isActive;

	private int currFrames = 0;
	private int totalFrames = 10;

	private Vector2 position;
	private Vector2 offset;
	public WoodSwordHorizontalSprite(Texture2D sprite, Direction direction, Vector2 position) {
		isActive = true;
		Sprite = sprite;
		effect = SpriteEffects.None;
		this.direction = direction;
		offset = new Vector2(12, 1);
		this.position = position;
	}

	public Direction GetDirection() {
		return direction;
	}

    public bool IsActive()
    {
        return isActive;
    }

    public void Draw(SpriteBatch spriteBatch, Vector2 scale) {
		switch (currFrames) {
			case 0:
				offset.X = 12;
				break;
			case 8:
				offset.X = 8;
				break;
			case 9:
				offset.X = 4;
				break;
		}

		Rectangle sourceRectangle = new Rectangle(10, 154, 16, 16);
		Rectangle destinationRectangle;
		if (direction == Direction.Right) {
			effect = SpriteEffects.None;
			destinationRectangle = new Rectangle((int)(position.X + offset.X * scale.X), (int)(position.Y + offset.Y * scale.Y), (int)(sourceRectangle.Width * scale.X), (int)(sourceRectangle.Height * scale.Y));
		} else {
			effect = SpriteEffects.FlipHorizontally;
			destinationRectangle = new Rectangle((int)(position.X - offset.X * scale.X), (int)(position.Y + offset.Y * scale.Y), (int)(sourceRectangle.Width * scale.X), (int)(sourceRectangle.Height * scale.Y));
		}
		spriteBatch.Draw(Sprite, destinationRectangle, sourceRectangle, Color.White, rotation: 0, new Vector2(0, 0), effects: effect, 1);
	}

	public void Update() {
		if (isActive) {
			currFrames++;

			if (currFrames == totalFrames) {
				currFrames = 0;
				isActive = false;
			}
		}
	}

}
