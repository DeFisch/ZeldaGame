using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class UseItemLeftSprite : ISprite
{
    private Texture2D Sprite;

    public UseItemLeftSprite(Texture2D sprite)
    {
        Sprite = sprite;
    }

    public void Draw(SpriteBatch spriteBatch, Vector2 location)
    {

        Rectangle sourceRectangle = new Rectangle(124, 11, 16, 16);
        Rectangle destinationRectangle = new Rectangle((int)location.X, (int)location.Y, sourceRectangle.Width * 2, sourceRectangle.Height * 2);
        SpriteEffects effect = SpriteEffects.FlipHorizontally;
        spriteBatch.Draw(Sprite, destinationRectangle, sourceRectangle, Color.White, rotation: 0, new Vector2(0, 0), effects: effect, 1);
    }

    public void Update()
    {

    }

}