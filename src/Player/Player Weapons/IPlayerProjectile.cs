using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static ZeldaGame.Globals;

namespace ZeldaGame.Player;
public interface IPlayerProjectile {
	public Rectangle GetHitBox();

	public Direction GetDirection();

	public int ProjectileDamage();

	public bool IsActive();

	public bool HasCollided();
	public void Collided();

	public void Update();

	public void Draw(SpriteBatch spriteBatch);
}
