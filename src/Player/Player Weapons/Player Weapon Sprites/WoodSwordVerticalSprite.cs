﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static ZeldaGame.Player.PlayerStateMachine;

public class WoodSwordVerticalSprite : IPlayerProjectile {
	SpriteEffects effect;
	private Texture2D Sprite;
	private Direction direction;
	private bool isActive;

	private Vector2 position;
	public WoodSwordVerticalSprite(Texture2D sprite, Direction direction) {
		isActive = true;
		Sprite = sprite;
		effect = SpriteEffects.None;
		this.direction = direction;
	}

	public Direction GetDirection() {
		return direction;
	}

    public bool IsActive()
    {
        return isActive;
    }

    public void Draw(SpriteBatch spriteBatch) {

		Rectangle sourceRectangle = new Rectangle(1, 154, 16, 16);
		Rectangle destinationRectangle = new Rectangle((int)position.X, (int)position.Y, sourceRectangle.Width * 2, sourceRectangle.Height * 2);
		if (direction == Direction.Down) // 1 indicates down
		{
			effect = SpriteEffects.FlipVertically;
		}
		spriteBatch.Draw(Sprite, destinationRectangle, sourceRectangle, Color.White, rotation: 0, new Vector2(0, 0), effects: effect, 1);
	}

	public void Update() {

	}

}