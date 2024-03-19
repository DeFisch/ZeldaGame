using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ZeldaGame.GameScreens;
public interface IGameScreen
{
    public void Draw(SpriteBatch spriteBatch);
    public void Update();

}
