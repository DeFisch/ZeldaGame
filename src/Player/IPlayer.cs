using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using static ZeldaGame.Player.PlayerStateMachine;

namespace ZeldaGame.Player {
	public interface IPlayer {
		public void Attack();
		public void Idle();
		public bool IsIdle();
		public void OnCollision(Rectangle collision);
		public void Walk();
		public void UseItem(int item);
		public Direction GetDirection();
		public void SetDirection(Direction direction); 
		public void Draw(SpriteBatch spriteBatch, Color color);
		public void Update();
		public void Reset();
		public void Knockback();
		public Vector2 GetPlayerPosition();
		public void SetPlayerPosition(Vector2 position, bool offset = true);
		public Rectangle GetPlayerHitBox();
		public Dictionary<IPlayerProjectile, Rectangle> GetProjectileHitBoxes();
		public bool isHurting();
		public void TakeDamage(float damage);
		public void GainHealth(float heal);
		public void GainMaxHealth(float hearts);
		public float GetHealth();
		public float GetMaxHealth();
	}
}