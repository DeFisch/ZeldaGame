using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static ZeldaGame.Globals;
using ZeldaGame.Collision;

namespace ZeldaGame.Enemy;
public interface IEnemy : ICollidible {

	new public Rectangle GetHitBox();

	public void Knockback(Direction knockbackDirection);
	
	public abstract void Update();

	public void Draw(SpriteBatch spriteBatch);

	public bool TakeDamage(int damage);

	public float DoDamage();

	public int GetHealth();

	new public void OnCollision(Rectangle intersect);

	public bool IsFinished();
}

