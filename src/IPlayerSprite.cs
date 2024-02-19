using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public interface IPlayerSprite : ISprite
{
    new void Update();

    new void Draw(SpriteBatch spriteBatch, Vector2 location, Color color);

    public void Pause();

    public void Play();
}
