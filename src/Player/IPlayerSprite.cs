using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public interface IPlayerSprite : ISprite
{
    public Rectangle GetHitBox();

    new void Update();

    new void Draw(SpriteBatch spriteBatch, Vector2 location, Color color, Vector2 scale);

    public void Pause();

    public void Play();
}
