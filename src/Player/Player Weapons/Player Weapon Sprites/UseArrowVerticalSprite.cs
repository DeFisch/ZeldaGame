using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class UseArrowVerticalSprite : ISprite
{
    SpriteEffects effect;
    private Texture2D Sprite;
    private int direction;

    public UseArrowVerticalSprite(Texture2D sprite, int direction)
    {
        Sprite = sprite;
        effect = SpriteEffects.None;
        this.direction = direction;
    }

    public void Draw(SpriteBatch spriteBatch, Vector2 location)
    {

        Rectangle sourceRectangle = new Rectangle(1, 185, 8, 15);
        Rectangle destinationRectangle = new Rectangle((int)location.X, (int)location.Y, sourceRectangle.Width * 2, sourceRectangle.Height * 2);
        if (direction == 1) // 1 indicates down
        {
            effect = SpriteEffects.FlipVertically;
        }
        spriteBatch.Draw(Sprite, destinationRectangle, sourceRectangle, Color.White, rotation: 0, new Vector2(0, 0), effects: effect, 1);
    }

    public void Update()
    {

    }

}
