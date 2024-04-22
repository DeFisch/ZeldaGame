using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ZeldaGame;

public class EnemyPlayerSprite
{
    private Texture2D texture;
    private Rectangle destRectangle;
    private Rectangle srcRectangle;

    private int currentFrame;
    private readonly int totalFrames = 2;
    private int frameID;
    private readonly int frameRate = 16;
    public EnemyPlayerSprite(Texture2D texture) : base()
    {
        this.texture = texture;
        currentFrame = 0;
        frameID = 0;
    }

    public Rectangle GetHitBox()
    {
        return destRectangle;
    }

    public void Draw(SpriteBatch spriteBatch, Vector2 location, Color color, Vector2 scale)
    {
        srcRectangle = new Rectangle(423 + (17 * currentFrame), 194, 16, 16);
        destRectangle = new Rectangle((int)location.X, (int)location.Y, (int)(srcRectangle.Width * scale.X), (int)(srcRectangle.Height * scale.Y));
        SpriteEffects effect = SpriteEffects.None;

        spriteBatch.Draw(texture, destRectangle, srcRectangle, color, rotation: 0, new Vector2(0, 0), effects: effect, 1);
    }

    public void Update()
    {
        frameID++;
        if (frameID % frameRate == 0)
        {
            currentFrame++;
            frameID = 0;
        }

        if (currentFrame == totalFrames)
        {
            currentFrame = 0;
        }
    }
}