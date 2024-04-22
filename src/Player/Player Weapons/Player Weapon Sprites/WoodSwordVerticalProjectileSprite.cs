using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static ZeldaGame.Globals;

namespace ZeldaGame.Player;
public class WoodSwordVerticalProjectileSprite : IPlayerProjectile {
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

    public WoodSwordVerticalProjectileSprite(Texture2D sprite, Direction direction, Vector2 position) {
		isActive = true;
		Sprite = sprite;
		effect = SpriteEffects.None;
		this.direction = direction;
		this.position = position;
		offset = new Vector2(5, 12);
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
        if (direction == Direction.Up)
            swordExplosion = new(Sprite, position + new Vector2(10, -30));
        else if (direction == Direction.Down)
            swordExplosion = new(Sprite, position - new Vector2(-10, -30));
        collided = true;
    }

	public void Draw(SpriteBatch spriteBatch) {
		sourceRectangle = new Rectangle(1, 154, 8, 16);
		if (direction == Direction.Down) {
			destinationRectangle = new Rectangle((int)(position.X + offset.X * scale.X), (int)(position.Y + offset.Y * scale.Y), (int)(sourceRectangle.Width * scale.X), (int)(sourceRectangle.Height * scale.Y));
		}
		else {
			destinationRectangle = new Rectangle((int)(position.X + offset.X * scale.X), (int)(position.Y - offset.Y * scale.Y), (int)(sourceRectangle.Width * scale.X), (int)(sourceRectangle.Height * scale.Y));
		}
		if (direction == Direction.Down) {
			effect = SpriteEffects.FlipVertically;
		}

        if (!collided)
            spriteBatch.Draw(Sprite, destinationRectangle, sourceRectangle, Color.White, rotation: 0, new Vector2(0, 0), effects: effect, 1);
        else
            swordExplosion.Draw(spriteBatch);
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
