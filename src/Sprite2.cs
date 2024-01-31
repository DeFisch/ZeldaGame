using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

/*
 * SetSprite2 class for non-moving, animated sprite
 */
public class Sprite2 : ISprite
{
    private Texture2D Sprite;
    private int currentFrame;
    private int totalFrames;

    // Constructor
    public Sprite2(Texture2D sprite)
    {
        currentFrame = 0;
        totalFrames = 2;
        Sprite = sprite;    
    }

    public void Draw(SpriteBatch spriteBatch, Vector2 location)
    {
        Rectangle sourceRectangle = new Rectangle(17 * currentFrame + 1, 11, 16, 16);
        Rectangle destinationRectangle = new Rectangle((int)location.X, (int)location.Y, 64, 64);

        spriteBatch.Begin();
        spriteBatch.Draw(Sprite, destinationRectangle, sourceRectangle, Color.White);
        spriteBatch.End();
    }

    public void Update()
    {
        // Animates sprite
        currentFrame++;
        if (currentFrame == totalFrames)
            currentFrame = 0;
    }
}
