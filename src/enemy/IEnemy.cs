
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ZeldaGame.Enemy;

public interface IEnemy {

	public Rectangle GetRectangle();
	
	public abstract void Update();

	public void Draw(SpriteBatch spriteBatch);

	public void TakeDamage(int damage);
}

