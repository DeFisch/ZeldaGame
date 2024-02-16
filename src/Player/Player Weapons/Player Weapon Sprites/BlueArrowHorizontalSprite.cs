using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static ZeldaGame.Player.PlayerStateMachine;

public class BlueArrowHorizontalSprite : IPlayerProjectile {
	SpriteEffects effect;
	private Texture2D Sprite;
	private Direction direction;
    private bool isActive;

    private Vector2 position;
    private Vector2 projectileMovement;
    private int projectileSpeed = 3;

    public BlueArrowHorizontalSprite(Texture2D sprite, Direction direction, Vector2 position) {
        isActive = true;
		Sprite = sprite;
		effect = SpriteEffects.None;
		this.direction = direction;
        this.position = position;
	}

	public Direction GetDirection() {
		return direction;
	}

    public bool IsActive()
    {
        return isActive;
    }

    public void Draw(SpriteBatch spriteBatch) {
		Rectangle sourceRectangle = new Rectangle(36, 185, 16, 16);
		Rectangle destinationRectangle = new Rectangle((int)position.X, (int)position.Y, sourceRectangle.Width * 2, sourceRectangle.Height * 2);
		if (direction == Direction.Left) {
			effect = SpriteEffects.FlipHorizontally;
		}
		spriteBatch.Draw(Sprite, destinationRectangle, sourceRectangle, Color.White, rotation: 0, new Vector2(0, 0), effects: effect, 1);
	}

	public void Update() {
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