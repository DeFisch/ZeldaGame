using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;
using static ZeldaGame.Player.PlayerActionHandler;

namespace ZeldaGame.Player;
public class MagicSwordVerticalProjectileSprite : IPlayerProjectile {
	SpriteEffects effect;
	private Texture2D Sprite;
	private Direction direction;
	private bool isActive;
	private bool collided;

	private Vector2 position;
	private Vector2 projectileMovement;
	private readonly int projectileSpeed = 6;
	private readonly int damage = 4;
	private readonly int expirationTimer = 10;
	private int expirationCounter;

	private Rectangle destinationRectangle;
	private Rectangle sourceRectangle;

	public MagicSwordVerticalProjectileSprite(Texture2D sprite, Direction direction, Vector2 position) {
		isActive = true;
		Sprite = sprite;
		effect = SpriteEffects.None;
		this.direction = direction;
		this.position = position;
		expirationCounter = 0;
	}

	public Rectangle GetHitBox() {
		return destinationRectangle;
	}

	public int ProjectileDamage() {
		return damage;
	}

	public Direction GetDirection() {
		return direction;
	}

	public bool IsActive() {
		return isActive;
	}

	public void Collided() {
		sourceRectangle = new Rectangle(97, 154, 8, 16);
		collided = true;
	}

	public void Draw(SpriteBatch spriteBatch, Vector2 scale) {
		if (!collided) {
			sourceRectangle = new Rectangle(71, 154, 8, 16);
			destinationRectangle = new Rectangle((int)position.X, (int)position.Y, (int)(sourceRectangle.Width * scale.X), (int)(sourceRectangle.Height * scale.Y));
		}
		else {
			// Drawing explosion after arrow collides
			if (direction == Direction.Right) {
				destinationRectangle = new Rectangle((int)position.X + 20, (int)position.Y, (int)(sourceRectangle.Width * scale.X), (int)(sourceRectangle.Height * scale.Y));
			}
			else {
				destinationRectangle = new Rectangle((int)position.X, (int)position.Y, (int)(sourceRectangle.Width * scale.X), (int)(sourceRectangle.Height * scale.Y));
			}
		}
		if (direction == Direction.Down) {
			effect = SpriteEffects.FlipVertically;
		}
		spriteBatch.Draw(Sprite, destinationRectangle, sourceRectangle, Color.White, rotation: 0, new Vector2(0, 0), effects: effect, 1);
	}

	public void Update() {
		// Moves in direction player is facing
		if (!collided) {
			switch (this.GetDirection()) {
				case Direction.Up:
					projectileMovement = new Vector2(0, -projectileSpeed);
					break;
				case Direction.Down:
					projectileMovement = new Vector2(0, projectileSpeed);
					break;
				case Direction.Left:
					projectileMovement = new Vector2(-projectileSpeed, 0);
					break;
				case Direction.Right:
					projectileMovement = new Vector2(projectileSpeed, 0);
					break;
			}
			position += projectileMovement;
		}
		else {
			expirationCounter++;
			if (expirationCounter == expirationTimer) {
				isActive = false;
			}
		}
	}

}
