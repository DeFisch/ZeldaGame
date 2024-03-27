
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ZeldaGame.Enemy;

public interface IEnemy {

	public Rectangle GetRectangle();
	
	public abstract void Update();

	public void Draw(SpriteBatch spriteBatch);

	public bool TakeDamage(int damage);

	public float DoDamage();

	public int GetHealth();

	public void Collide(Rectangle intersect);

	public bool IsFinished();
}

