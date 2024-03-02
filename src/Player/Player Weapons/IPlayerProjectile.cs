using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static ZeldaGame.Player.PlayerStateMachine;

namespace ZeldaGame.Player;
public interface IPlayerProjectile {
	public Rectangle GetHitBox();

	public Direction GetDirection();

	public int ProjectileDamage();

	public bool IsActive();

	public void Collided();

	public void Update();

	public void Draw(SpriteBatch spriteBatch, Vector2 scale);
}
