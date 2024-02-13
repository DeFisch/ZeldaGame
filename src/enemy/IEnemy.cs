using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ZeldaGame.Enemy;

public interface IEnemy {

	public abstract void Update();

	public void Draw(SpriteBatch spriteBatch);
}

