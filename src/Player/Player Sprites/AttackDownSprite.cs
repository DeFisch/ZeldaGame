using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class AttackDownSprite : ISprite
{
    private Texture2D Sprite;

    public bool isPlaying;
    public int currentFrame;
    public int totalFrames;

    // Constructor
    public AttackDownSprite(Texture2D sprite)
    {
		isPlaying = true;
		Sprite = sprite;
        currentFrame = 0;
        totalFrames = 12;
    }

    public void Draw(SpriteBatch spriteBatch, Vector2 location)
    {
        Rectangle sourceRectangle = new Rectangle(107, 11, 16, 16);
        Rectangle destinationRectangle = new Rectangle((int)location.X, (int)location.Y, sourceRectangle.Width * 2, sourceRectangle.Height * 2);
        SpriteEffects effect = SpriteEffects.None;
        spriteBatch.Draw(Sprite, destinationRectangle, sourceRectangle, Color.White, rotation: 0, new Vector2(0, 0), effects: effect, 1);
    }

    public void Update()
    {
        if (!isPlaying) {
            currentFrame++;
            if (currentFrame == totalFrames) {
                currentFrame = 0;
            }
        }
    }

    public void PlayToggle() {
        isPlaying = !isPlaying;
    }

}
