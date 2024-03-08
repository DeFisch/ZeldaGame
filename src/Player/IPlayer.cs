using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using static ZeldaGame.Player.PlayerStateMachine;

namespace ZeldaGame.Player {
	public interface IPlayer {

		public void Attack();
		public void Idle();
		public void Colliding(Rectangle collision);
		public void Walk();
		public void UseItem(int item);
		public void SetDirection(Direction direction); 
		public void Draw(SpriteBatch spriteBatch, Color color);
		public void Update();
		public void Reset();
		public void SetPlayerPosition(Vector2 position, bool offset = true);
		public Rectangle GetPlayerHitBox();
		public Dictionary<IPlayerProjectile, Rectangle> GetProjectileHitBoxes();
		public bool isHurting();
	}
}