using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

/*
 *  SetSprite1 class for non-moving, non-animated sprite
 */
public class IdleUpSprite : ISprite
{
    private Texture2D Sprite;

    // Constructor
    public IdleUpSprite(Texture2D sprite)
	{
        Sprite = sprite;
    }

    public void Draw(SpriteBatch spriteBatch, Vector2 location)
    {
        Rectangle sourceRectangle = new Rectangle(1, 11, 16, 16);
        Rectangle destinationRectangle = new Rectangle((int)location.X, (int)location.Y, 64, 64);

        spriteBatch.Begin();
        spriteBatch.Draw(Sprite, destinationRectangle, sourceRectangle, Color.White);
        spriteBatch.End();
    }

    public void Update()
    {
        // No Implementation, nothing needs to be updated
    }

}
