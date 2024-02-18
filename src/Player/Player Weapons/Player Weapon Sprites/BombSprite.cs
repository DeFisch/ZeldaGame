using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static ZeldaGame.Player.PlayerStateMachine;

public class BombSprite : IPlayerProjectile {
	SpriteEffects effect;
	private Texture2D Sprite;
	private bool isActive;

    private Vector2 position;
	private Direction direction;

    private int currentFrame;
	private int totalFrames;
	private int frameRate;
	private int frameID;
	private int bombCounter;
	private int bombTimer;

	private int XOffSet;
	private int YOffSet;

	public BombSprite(Texture2D sprite, Direction direction,  Vector2 position) {
		isActive = true;
		Sprite = sprite;
		effect = SpriteEffects.None;
		this.position = position;
		this.direction = direction;

		currentFrame = 0;
		totalFrames = 4;
		frameID = 0;
		frameRate = 8;
		bombCounter = 0;
		bombTimer = 48;

		SetOffSet();
	}

	public Direction GetDirection() {
		return direction;
	}

    public bool IsActive()
    {
        return isActive;
    }

    public void Draw(SpriteBatch spriteBatch) {
		Rectangle sourceRectangle;
		Rectangle destinationRectangle;
		if (currentFrame == 0)
		{
            sourceRectangle = new Rectangle(129, 185, 8, 16);
            destinationRectangle = new Rectangle((int)position.X + XOffSet + 8, (int)position.Y + YOffSet, sourceRectangle.Width * 2, sourceRectangle.Height * 2);
        } else
		{
			sourceRectangle = new Rectangle(121 + (17* currentFrame), 185, 16, 16);
            destinationRectangle = new Rectangle((int)position.X + XOffSet, (int)position.Y + YOffSet, sourceRectangle.Width * 2, sourceRectangle.Height * 2);
        }
		spriteBatch.Draw(Sprite, destinationRectangle, sourceRectangle, Color.White, rotation: 0, new Vector2(0, 0), effects: effect, 1);
	}

	public void Update() {
		frameID++;
		bombCounter++;
		if (frameID % frameRate == 0) {
			
			frameID = 0;
			if (bombCounter >= bombTimer)
			{
				currentFrame++;
			}
		}

		if (currentFrame == totalFrames) {
			currentFrame = 0;
			isActive = false;
		}
	}

	private void SetOffSet()
	{
        switch (direction)
        {
            case Direction.Up:
				XOffSet = 0; YOffSet = -32;
				break;
            case Direction.Left:
                XOffSet = -32; YOffSet = 0;
                break;
            case Direction.Down:
                XOffSet = 0; YOffSet = 32;
                break;
            case Direction.Right:
                XOffSet = 32; YOffSet = 0;
                break;
        }
    }

}
