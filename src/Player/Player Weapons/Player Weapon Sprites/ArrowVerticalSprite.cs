using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static ZeldaGame.Player.PlayerStateMachine;

public class ArrowVerticalSprite : IPlayerProjectile {
	SpriteEffects effect;
	private Texture2D Sprite;
	private Direction direction;
    private bool isActive;

	private Vector2 position;
    private Vector2 projectileMovement;
    private readonly int projectileSpeed = 4;
    private readonly int damage = 2;

    private Rectangle destinationRectangle;

    public ArrowVerticalSprite(Texture2D sprite, Direction direction, Vector2 position) {
        isActive = true;
		Sprite = sprite;
		effect = SpriteEffects.None;
		this.direction = direction;
		this.position = position;
	}

    public Rectangle GetHitBox()
    {
        return destinationRectangle;
    }

    public int ProjectileDamage()
    {
        return damage;
    }

    public Direction GetDirection() {
		return direction;
	}

    public bool IsActive()
    {
        return isActive;
    }

    public void Draw(SpriteBatch spriteBatch, Vector2 scale) {

		Rectangle sourceRectangle = new Rectangle(1, 185, 8, 16);
		destinationRectangle = new Rectangle((int)position.X + 8, (int)position.Y, (int)(sourceRectangle.Width * scale.X), (int)(sourceRectangle.Height * scale.Y));
		if (direction == Direction.Down) // 1 indicates down
		{
			effect = SpriteEffects.FlipVertically;
		}
		spriteBatch.Draw(Sprite, destinationRectangle, sourceRectangle, Color.White, rotation: 0, new Vector2(0, 0), effects: effect, 1);
	}

	public void Update() {
        // Moves in direction player is facing
        switch (this.GetDirection())
        {
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

}
