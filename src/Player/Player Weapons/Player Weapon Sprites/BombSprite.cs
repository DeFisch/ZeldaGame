using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;
using static ZeldaGame.Globals;

namespace ZeldaGame.Player;
public class BombSprite : IPlayerProjectile {
	private SpriteEffects effect;
	private Texture2D Sprite;
	private bool isActive;
	private bool exploded;

    private Vector2 position;
	private Direction direction;
    private readonly int damage = 2;

    private int currentFrame;
	private readonly int totalFrames = 4;
	private int frameID;
    private readonly int frameRate = 8;
    private int bombCounter;
	private readonly int bombTimer = 48;

	private int XOffSet;
	private int YOffSet;

	private Rectangle destinationRectangle;
	private Rectangle sourceRectangle;
	private Rectangle hitBox;

	public BombSprite(Texture2D sprite, Direction direction,  Vector2 position) {
		isActive = true;
		Sprite = sprite;
		effect = SpriteEffects.None;
		this.position = position;
		this.direction = direction;

		currentFrame = 0;
		frameID = 0;
		bombCounter = 0;

		SetOffSet();
	}

    public Rectangle GetHitBox()
    {
		if (exploded)
		{
            hitBox = destinationRectangle;
            hitBox.Inflate(50, 50);
		}
        return hitBox;
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

    public bool HasCollided()
    {
        return exploded;
    }

    public void Collided()
    {
        //isActive = false;
    }

    public void Draw(SpriteBatch spriteBatch) {
		if (currentFrame == 0)
		{
            sourceRectangle = new Rectangle(129, 185, 8, 16);
            destinationRectangle = new Rectangle((int)position.X + XOffSet + 8, (int)position.Y + YOffSet, (int)(sourceRectangle.Width * scale.X), (int)(sourceRectangle.Height * scale.Y));
        } else
		{
			sourceRectangle = new Rectangle(121 + (17* currentFrame), 185, 16, 16);
            destinationRectangle = new Rectangle((int)position.X + XOffSet, (int)position.Y + YOffSet, (int)(sourceRectangle.Width * scale.X), (int)(sourceRectangle.Height * scale.Y));
        }
		spriteBatch.Draw(Sprite, destinationRectangle, sourceRectangle, Color.White, rotation: 0, new Vector2(0, 0), effects: effect, 1);
	}

	public void Update() {
		// Lets first frame of bomb exist for the counter duration before it animates the explosion
		frameID++;
		bombCounter++;
		if (frameID % frameRate == 0) {
			frameID = 0;
			if (bombCounter >= bombTimer)
			{
				Globals.audioLoader.Play("LOZ_Bomb_Blow");
				exploded = true;
				currentFrame++;
			}
		}

		// Once exploded, it expires
		if (currentFrame == totalFrames) {
			currentFrame = 0;
			isActive = false;
		}
	}

	// Sets bomb in front of player
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
