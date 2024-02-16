using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Runtime.CompilerServices;
using static ZeldaGame.Player.PlayerStateMachine;

public class FireballSprite : IPlayerProjectile {
	SpriteEffects effect;
	private Texture2D Sprite;
	private Direction direction;
	private bool isActive;

    private Vector2 position;
    private Vector2 projectileMovement;
    private int projectileSpeed = 3;

    private int currentFrame;
	private int totalFrames;
	private int frameRate;
	private int frameID;

	public FireballSprite(Texture2D sprite, Direction direction, Vector2 position) {
		isActive = true;
		Sprite = sprite;
		effect = SpriteEffects.None;
		this.direction = direction;
		this.position = position;

		currentFrame = 0;
		totalFrames = 4;
		frameID = 0;
		frameRate = 8;
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
		frameID++;
		if (frameID % frameRate == 0) {
			currentFrame++;
			frameID = 0;
		}

		if (currentFrame == totalFrames) {
			currentFrame = 0;
		}

		switch (currentFrame) {
			case 0:
				effect = SpriteEffects.None;
				break;
			case 2:
				effect = SpriteEffects.FlipHorizontally;
				break;
		}

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
