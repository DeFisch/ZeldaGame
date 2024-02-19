using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static ZeldaGame.Player.PlayerStateMachine;

public class FireballSprite : IPlayerProjectile {
	SpriteEffects effect;
	private Texture2D Sprite;
	private Direction direction;
	private bool isActive;

    private Vector2 position;
    private Vector2 projectileMovement;
    private readonly float projectileSpeed = 3.5f;

    private int currentFrame;
	private readonly int totalFrames = 4;
	private int frameID;
    private readonly int frameRate = 8;

    public FireballSprite(Texture2D sprite, Direction direction, Vector2 position) {
		isActive = true;
		Sprite = sprite;
		effect = SpriteEffects.None;
		this.direction = direction;
		this.position = position;

		currentFrame = 0;
		frameID = 0;
	}

	public Direction GetDirection() {
		return direction;
	}

    public bool IsActive()
    {
        return isActive;
    }

    public void Draw(SpriteBatch spriteBatch) {

		Rectangle sourceRectangle = new Rectangle(191, 185, 16, 16);
		Rectangle destinationRectangle = new Rectangle((int)position.X, (int)position.Y, sourceRectangle.Width * 2, sourceRectangle.Height * 2);
		spriteBatch.Draw(Sprite, destinationRectangle, sourceRectangle, Color.White, rotation: 0, new Vector2(0, 0), effect, 1);
	}

	public void Update() {
		// Updates frame of fireball
		frameID++;
		if (frameID % frameRate == 0) {
			currentFrame++;
			frameID = 0;
		}

		if (currentFrame == totalFrames) {
			currentFrame = 0;
		}

		// Updates fireball effect
		switch (currentFrame) {
			case 0:
				effect = SpriteEffects.None;
				break;
			case 2:
				effect = SpriteEffects.FlipHorizontally;
				break;
		}

		// Travels in direction of player
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
