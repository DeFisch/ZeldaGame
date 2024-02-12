using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class UseArrowHorizontalSprite : ISprite
{
    SpriteEffects effect;
    private Texture2D Sprite;
    private int direction;
    private int X;

    public UseArrowHorizontalSprite(Texture2D sprite, int direction)
    {
        Sprite = sprite;
        effect = SpriteEffects.None;
        this.direction = direction;
    }

    public void Draw(SpriteBatch spriteBatch, Vector2 location)
    {

        Rectangle sourceRectangle = new Rectangle(10, 185, 15, 15);
        Rectangle destinationRectangle = new Rectangle((int)location.X, (int)location.Y, sourceRectangle.Width * 2, sourceRectangle.Height * 2);
        if (direction == 1) // 1 indicates left
        {
            effect = SpriteEffects.FlipHorizontally;
        }
        spriteBatch.Draw(Sprite, destinationRectangle, sourceRectangle, Color.White, rotation: 0, new Vector2(0, 0), effects: effect, 1);
    }

    public void Update()
    {

    }

}
