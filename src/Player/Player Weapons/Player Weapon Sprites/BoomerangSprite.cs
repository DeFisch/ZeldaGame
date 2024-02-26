using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using static ZeldaGame.Player.PlayerStateMachine;

public class BoomerangSprite : IPlayerProjectile {
	SpriteEffects effect;
	private Texture2D Sprite;
	private Direction direction;
	private bool isActive;

    private Vector2 position;
    private Vector2 projectileMovement;
    private readonly double projectileSpeed = 2;

	private int existanceCounter;
	private readonly int existanceDuration = 100;
    private int currentFrame;
	private readonly int totalFrames = 3;
	private int frameID;
    private readonly int frameRate = 6;

    public BoomerangSprite(Texture2D sprite, Direction direction, Vector2 position) {
		isActive = true;
		Sprite = sprite;
		effect = SpriteEffects.None;
		this.direction = direction;
		this.position = position;

		existanceCounter = 0;
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

	public void Draw(SpriteBatch spriteBatch, Vector2 scale) {
		Rectangle sourceRectangle = new Rectangle(64 + (currentFrame * 9), 185, 8, 16);
		Rectangle destinationRectangle = new Rectangle((int)position.X + 8, (int)position.Y, (int)(sourceRectangle.Width * scale.X), (int)(sourceRectangle.Height * scale.Y));
		spriteBatch.Draw(Sprite, destinationRectangle, sourceRectangle, Color.White, rotation: 0, new Vector2(0, 0), effects: effect, 1);
	}

	public void Update()
	{
		// Animates boomerang
        frameID++;
        if (frameID % frameRate == 0) {
			currentFrame++;
			frameID = 0;
		}

		if (currentFrame == totalFrames) {
			currentFrame = 0;
		}

        double scale = -4 * (1 / (1 + Math.Exp(-(10.0 / existanceDuration) * (existanceCounter - 0.6 * existanceDuration)))) + 2; // logistic function to smooth out animation
        // Determines direction at which is travels
        switch (this.GetDirection())
        {
            case Direction.Up:
                projectileMovement = new Vector2(0, -(float)projectileSpeed) * (float)scale;
                break;
            case Direction.Down:
                projectileMovement = new Vector2(0, (float)projectileSpeed) * (float)scale;
                break;
            case Direction.Left:
                projectileMovement = new Vector2(-(float)projectileSpeed, 0) * (float)scale;
                break;
            case Direction.Right:
                projectileMovement = new Vector2((float)projectileSpeed, 0) * (float)scale;
                break;
        }

		// Indicates when it is time to remove or come back to player
		existanceCounter++;
        if (existanceCounter <= existanceDuration + 20)
		{
			position += projectileMovement;
		} else
		{
			isActive = false;
		}
    }
}
