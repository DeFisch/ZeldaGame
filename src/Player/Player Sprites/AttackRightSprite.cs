using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class AttackRightSprite : ISprite
{
    private Texture2D Sprite;

    public int currentFrame;
    public int totalFrames;

    // Constructor
    public AttackRightSprite(Texture2D sprite)
    {
        Sprite = sprite;
        currentFrame = 0;
        totalFrames = 4;
    }

    public void Draw(SpriteBatch spriteBatch, Vector2 location)
    {

        Rectangle sourceRectangle = new Rectangle(1 + (17 * currentFrame), 77, 16, 17);
        Rectangle destinationRectangle = new Rectangle((int)location.X, (int)location.Y, sourceRectangle.Width * 2, sourceRectangle.Height * 2);
        SpriteEffects effect = SpriteEffects.None;
        spriteBatch.Draw(Sprite, destinationRectangle, sourceRectangle, Color.White, rotation: 0, new Vector2(0, 0), effects: effect, 1);
    }

    public void Update()
    {
        currentFrame++;
        if (currentFrame == totalFrames)
        {
            currentFrame = 0;
        }
    }

}