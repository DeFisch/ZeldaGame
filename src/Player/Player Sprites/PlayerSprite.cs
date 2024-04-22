using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;
using ZeldaGame.Player;

public abstract class PlayerSprite : IPlayerSprite
{
    public static bool isPlaying;

    public static Rectangle destRectangle;

    public PlayerSprite()
    {

    }

    public abstract void Update();

    public abstract void Draw(SpriteBatch spriteBatch, Vector2 location, Color color);

    public Rectangle GetHitBox()
    {
        return destRectangle;
    }

    public void Play()
    {
        isPlaying = true;
    }

    public void Pause()
    {
        isPlaying = false;
    }
}