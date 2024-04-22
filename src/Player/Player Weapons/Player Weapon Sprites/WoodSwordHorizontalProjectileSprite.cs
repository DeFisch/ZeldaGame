using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static ZeldaGame.Globals;

namespace ZeldaGame.Player;
public class WoodSwordHorizontalProjectileSprite : IPlayerProjectile {
	SpriteEffects effect;
	private Texture2D Sprite;
	private Direction direction;
	private bool isActive;
	private bool collided;

	private Vector2 position;
	private Vector2 offset;
	private Vector2 projectileMovement;
	private readonly int projectileSpeed = 6;
	private readonly int damage = 1;
	private readonly int expirationTimer = 20;
	private int expirationCounter;

	private Rectangle destinationRectangle;
	private Rectangle sourceRectangle;

	SwordProjectileExplosion swordExplosion;


    public WoodSwordHorizontalProjectileSprite(Texture2D sprite, Direction direction, Vector2 position) {
		isActive = true;
		Sprite = sprite;
		effect = SpriteEffects.None;
		this.direction = direction;
		offset = new Vector2(12, 1);
		this.position = position;
		expirationCounter = 0;
	}

	public Rectangle GetHitBox() {
		if (collided)
			return swordExplosion.GetHitBox();
		else
		{
            Rectangle hitBox = destinationRectangle;
            hitBox.Inflate(-8, -25);
            return hitBox;
        }
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

    public bool HasCollided()
    {
        return collided;
    }

    public void Collided() {
		if (direction == Direction.Right)
            swordExplosion = new(Sprite, position + new Vector2(60, 0));
		else if (direction == Direction.Left)
			swordExplosion = new(Sprite, position - new Vector2(20, 0));
		collided = true;
	}

	public void Draw(SpriteBatch spriteBatch, Vector2 scale) {
		if (!collided) {
			sourceRectangle = new Rectangle(10, 154, 16, 16);
			if (direction == Direction.Right) {
				destinationRectangle = new Rectangle((int)(position.X + offset.X * scale.X), (int)(position.Y + offset.Y * scale.Y), (int)(sourceRectangle.Width * scale.X), (int)(sourceRectangle.Height * scale.Y));
			}
			else {
				destinationRectangle = new Rectangle((int)(position.X - offset.X * scale.X), (int)(position.Y + offset.Y * scale.Y), (int)(sourceRectangle.Width * scale.X), (int)(sourceRectangle.Height * scale.Y));
			}
			
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
		if (direction == Direction.Left) {
			effect = SpriteEffects.FlipHorizontally;
		}

		if (!collided)
			spriteBatch.Draw(Sprite, destinationRectangle, sourceRectangle, Color.White, rotation: 0, new Vector2(0, 0), effects: effect, 1);
		else
            swordExplosion.Draw(spriteBatch, scale);
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

		if (collided)
            swordExplosion.Update();
	}
}
