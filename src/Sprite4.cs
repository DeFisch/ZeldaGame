using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

/*
 * Sprite4 class for moving, animated sprite
 */
public class Sprite4 : ISprite
{
    private Texture2D Sprite;
    private int X;
    private bool GoingLeft;
    private int SpriteMovementFrame;
    private int totalFrames;

    // Constructor
    public Sprite4(Texture2D sprite)
    {
        SpriteMovementFrame = 2;
        totalFrames = 2;
        X = 250;
        GoingLeft = false;
        Sprite = sprite;
    }

    public void Draw(SpriteBatch spriteBatch, Vector2 location)
    {
        Rectangle sourceRectangle = new Rectangle(17 * SpriteMovementFrame + 1, 11, 16, 16);
        Rectangle destinationRectangle = new Rectangle(X, (int)location.Y, 64, 64);

        spriteBatch.Begin();
        spriteBatch.Draw(Sprite, destinationRectangle, sourceRectangle, Color.White);
        spriteBatch.End();
    }

    public void Update()
    {
        // Moves left to right on the screen from pos X=250 to X=450
        if (X < 450 && !GoingLeft)
        {
            X += 15;
        }
        else if (X > 250 && GoingLeft)
        {
            X -= 15;
        }

        if (X > 450)
        {
            GoingLeft = true;
        }
        else if (X <= 250)
        {
            GoingLeft = false;
        }

        // Animates sprite
        SpriteMovementFrame++;
        if (SpriteMovementFrame == (totalFrames + 2))
            SpriteMovementFrame = 2;
    }

}
