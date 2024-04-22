using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static ZeldaGame.Player.PlayerActionHandler;

namespace ZeldaGame.Player;
public interface IPlayerSprite
{
    public Rectangle GetHitBox();

    public void Update();

    public void Draw(SpriteBatch spriteBatch, Vector2 location, Color color);

    public void Pause();

    public void Play();
}
