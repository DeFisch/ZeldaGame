using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static ZeldaGame.Game1;

namespace ZeldaGame.Player;
public class GetItemSprite : PlayerSprite {
	private Texture2D Sprite;
	private int currentFrame;
	private int totalFrames;

	public GetItemSprite(Texture2D sprite) {
		Sprite = sprite;
		currentFrame = 0;
		totalFrames = 64;
	}

    public override void Draw(SpriteBatch spriteBatch, Vector2 location, Color color) {

		Rectangle sourceRectangle = new Rectangle(230, 11, 16, 16);
		destRectangle = new Rectangle((int)location.X, (int)location.Y, (int)(sourceRectangle.Width * Globals.scale.X), (int)(sourceRectangle.Height * Globals.scale.Y));
		SpriteEffects effect = SpriteEffects.None;
		spriteBatch.Draw(Sprite, destRectangle, sourceRectangle, color, rotation: 0, new Vector2(0, 0), effects: effect, 1);
	}

	public override void Update() {
		if (isPlaying) {
			currentFrame++;
			if (currentFrame == totalFrames) {
				currentFrame = 0;
			}
		}
	}
}
