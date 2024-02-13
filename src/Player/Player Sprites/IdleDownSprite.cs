using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class IdleDownSprite : ISprite
{
    private Texture2D Sprite;

	public int currentFrame;
	public int totalFrames;

	// Constructor
	public IdleDownSprite(Texture2D sprite)
	{
        Sprite = sprite;
        currentFrame = 0;
        totalFrames = 1;
    }

    public void Draw(SpriteBatch spriteBatch, Vector2 location)
    {
		Rectangle sourceRectangle = new Rectangle(1, 11, 16, 16);
		Rectangle destinationRectangle = new Rectangle((int)location.X, (int)location.Y, sourceRectangle.Width * 2, sourceRectangle.Height * 2);
		SpriteEffects effect = SpriteEffects.None;
		//spriteBatch.Begin();
		spriteBatch.Draw(Sprite, destinationRectangle, sourceRectangle, Color.White, rotation: 0, new Vector2(0, 0), effects: effect, 1);
		//spriteBatch.End();
	}

	public void Update()
    {
		currentFrame = (currentFrame + 1) % totalFrames;
	}

	public void PlayToggle() {
		throw new System.NotImplementedException();
	}
}
