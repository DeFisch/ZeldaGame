using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static ZeldaGame.Player.PlayerStateMachine;

public interface IPlayerProjectile {
	public Direction GetDirection();

	public bool IsActive();

	public void Update();

	public void Draw(SpriteBatch spriteBatch, Vector2 scale);
}
