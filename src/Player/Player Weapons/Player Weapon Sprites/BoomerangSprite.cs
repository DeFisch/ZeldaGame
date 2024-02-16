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
    private double projectileSpeed = 3;

	private int existanceCounter;
	private int existanceDuration;
    private int currentFrame;
	private int totalFrames;
	private int frameRate;
	private int frameID;

	public BoomerangSprite(Texture2D sprite, Direction direction, Vector2 position) {
		isActive = true;
		Sprite = sprite;
		effect = SpriteEffects.None;
		this.direction = direction;
		this.position = position;

		existanceCounter = 0;
		existanceDuration = 100;
		currentFrame = 0;
		totalFrames = 3;
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
		Rectangle sourceRectangle = new Rectangle(64 + (currentFrame * 9), 185, 8, 16);
		Rectangle destinationRectangle = new Rectangle((int)position.X, (int)position.Y, sourceRectangle.Width * 2, sourceRectangle.Height * 2);
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

		// Determines direction at which is travels
        switch (this.GetDirection())
        {
            case Direction.Up:
                projectileMovement = new Vector2(0, -(float)projectileSpeed);
                break;
            case Direction.Down:
                projectileMovement = new Vector2(0, (float)projectileSpeed);
                break;
            case Direction.Left:
                projectileMovement = new Vector2(-(float)projectileSpeed, 0);
                break;
            case Direction.Right:
                projectileMovement = new Vector2((float)projectileSpeed, 0);
                break;
        }

		// Indicates when it is time to remove or come back to player
        if (existanceCounter < existanceDuration)
        {
            existanceCounter++;
        }

        if (existanceCounter <= existanceDuration/2)
		{
			position += projectileMovement;
        } else if (existanceCounter < existanceDuration)
		{
			position -= projectileMovement;
		} else
		{
			isActive = false;
		}
    }

}
