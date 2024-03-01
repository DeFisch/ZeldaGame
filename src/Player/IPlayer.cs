using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static ZeldaGame.Player.PlayerStateMachine;

namespace ZeldaGame.Player {
	public interface IPlayer {

		public void Attack();
		public void Idle();
		public void Colliding();
		public void Walk();
		public void UseItem(int item);
		public void SetDirection(Direction direction); 
		public void Draw(SpriteBatch spriteBatch, Color color);
		public void Update();
		public void Reset();
		public void SetPlayerPosition(Vector2 position);
		public Rectangle GetPlayerHitBox();
	}
}