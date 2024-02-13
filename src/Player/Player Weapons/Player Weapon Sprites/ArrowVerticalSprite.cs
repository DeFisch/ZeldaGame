using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static ZeldaGame.Player.PlayerStateMachine;

public class ArrowVerticalSprite : IProjectile {
	SpriteEffects effect;
	private Texture2D Sprite;
	private Direction direction;

	public ArrowVerticalSprite(Texture2D sprite, Direction direction) {
		Sprite = sprite;
		effect = SpriteEffects.None;
		this.direction = direction;
	}

	public Direction GetDirection() {
		return direction;
	}

	public void Draw(SpriteBatch spriteBatch, Vector2 location) {

		Rectangle sourceRectangle = new Rectangle(1, 185, 8, 16);
		Rectangle destinationRectangle = new Rectangle((int)location.X, (int)location.Y, sourceRectangle.Width * 2, sourceRectangle.Height * 2);
		if (direction == Direction.Down) // 1 indicates down
		{
			effect = SpriteEffects.FlipVertically;
		}
		spriteBatch.Draw(Sprite, destinationRectangle, sourceRectangle, Color.White, rotation: 0, new Vector2(0, 0), effects: effect, 1);
	}

	public void Update() {

	}

}
