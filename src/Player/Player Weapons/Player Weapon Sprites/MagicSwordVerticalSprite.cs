using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;
using static ZeldaGame.Player.PlayerStateMachine;

public class MagicSwordVerticalSprite : IPlayerProjectile {
	SpriteEffects effect;
	private Texture2D Sprite;
	private Direction direction;
	private bool isActive;

	private int currFrames = 0;
	private int totalFrames = 10;

	private Vector2 position;
	private Vector2 offset;

	private Rectangle destinationRectangle;

	public MagicSwordVerticalSprite(Texture2D sprite, Direction direction, Vector2 position) {
		isActive = true;
		Sprite = sprite;
		effect = SpriteEffects.None;
		this.direction = direction;
		offset = new Vector2(5, 0);
		this.position = position;
	}

    public Rectangle GetHitBox()
    {
        return destinationRectangle;
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
				offset.Y = 12;
				break;
			case 8:
				offset.Y = 8;
				break;
			case 9:
				offset.Y = 4;
				break;
		}

		Rectangle sourceRectangle = new Rectangle(71, 154, 8, 16);
		if (direction == Direction.Down) {
			effect = SpriteEffects.FlipVertically;
			destinationRectangle = new Rectangle((int)(position.X + offset.X * scale.X), (int)(position.Y + offset.Y * scale.Y), (int)(sourceRectangle.Width * scale.X), (int)(sourceRectangle.Height * scale.Y));
		}
		else {
			effect = SpriteEffects.None;
			destinationRectangle = new Rectangle((int)(position.X + offset.X * scale.X), (int)(position.Y - offset.Y * scale.Y), (int)(sourceRectangle.Width * scale.X), (int)(sourceRectangle.Height * scale.Y));
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
