using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ZeldaGame.NPCs;

public class Zelda : INPC
{
    private Texture2D texture;
    private Vector2 position;

    public Zelda(Texture2D texture, Vector2 position)
    {
        this.texture = texture;
        this.position = position;
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        Rectangle sourceRectangle = new Rectangle(1, 42, 16, 16);
        Rectangle destinationRectangle = new Rectangle((int)position.X, (int)position.Y, 15*4, 15 * 4);
        spriteBatch.Draw(texture, destinationRectangle, sourceRectangle, Color.White);
    }

    public void Update()
    {

    }
}