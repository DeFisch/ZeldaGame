using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ZeldaGame.NPCs;

public class Flame : INPC
{
    private Texture2D texture;
    private Vector2 position;

    private int currentFrame = 0;
    private int totalFrames = 2;
    private static int[,] character_sprites = new int[,] { { 52, 11, 15, 16 }, { 69, 11, 16, 16 } };
    private int switchFrameDelay = 15;
    private int frameCounter = 0;

    public Flame(Texture2D texture, Vector2 position)
    {
        this.texture = texture;
        this.position = position;
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        int sprite_id = currentFrame % 2;
        Rectangle sourceRectangle = new Rectangle(character_sprites[sprite_id, 0], character_sprites[sprite_id, 1], character_sprites[sprite_id, 2], character_sprites[sprite_id, 3]);
        Rectangle destinationRectangle = new Rectangle((int)position.X, (int)position.Y, 15 * 4, 15 * 4);
        spriteBatch.Draw(texture, destinationRectangle, sourceRectangle, Color.White);
    }

    public void Update()
    {
        frameCounter++;
        if (frameCounter >= switchFrameDelay)
        {
            currentFrame++;
            if (currentFrame >= totalFrames)
            {
                currentFrame = 0;
            }
            frameCounter = 0;
        }
    }
}