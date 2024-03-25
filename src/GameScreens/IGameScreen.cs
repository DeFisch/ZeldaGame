using Microsoft.Xna.Framework.Graphics;

namespace ZeldaGame;
public interface IGameScreen
{
    public void Draw(SpriteBatch spriteBatch);
    public void Update();

}
