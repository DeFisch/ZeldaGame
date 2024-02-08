using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ZeldaGame.NPCs;

public class Flame : INPC
{
    private Texture2D texture;
    private Vector2 position;

    public int currentFrame;
    public int totalFrames;

    public Flame(Texture2D texture, Vector2 position)
    {
        this.texture = texture;
        this.position = position;
        currentFrame = 0;
        totalFrames = 1;
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        Rectangle sourceRectangle = new Rectangle(52, 11, 15, 16);
        Rectangle destinationRectangle = new Rectangle((int)position.X, (int)position.Y, 15*4, 15*4);
        spriteBatch.Draw(texture, destinationRectangle, sourceRectangle, Color.White);
    }

    public void Update()
    {
        currentFrame = (currentFrame + 1) % totalFrames;

    }
}