using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

/*
 *  Sprite3 class for moving, non-animated sprite
 */
public class Sprite3 : ISprite
{
    private Texture2D Sprite;
    private int Y;
    private bool GoingUp;

    // Constructor
    public Sprite3(Texture2D sprite)
    {
        Y = 0;
        GoingUp = false;
        Sprite = sprite;
    }

    public void Draw(SpriteBatch spriteBatch, Vector2 location)
    {

        Rectangle sourceRectangle = new Rectangle(1, 11, 16, 16);
        Rectangle destinationRectangle = new Rectangle((int)location.X, Y, 64, 64);

        spriteBatch.Begin();
        spriteBatch.Draw(Sprite, destinationRectangle, sourceRectangle, Color.White);
        spriteBatch.End();
    }

    public void Update()
    {
        // Moves up and down the screen from positition 0 to 200
        if (Y < 200 && !GoingUp)
        {
            Y += 10;
        } else if (Y > 0 && GoingUp)
        {
            Y -= 10;
        }

        if (Y >= 200)
        {
            GoingUp = true;
        }
        else if (Y <= 0)
        {
            GoingUp = false;
        }
    }
}
