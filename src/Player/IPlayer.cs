using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using ZeldaGame.Collision;
using static ZeldaGame.Player.PlayerActionHandler;
using static ZeldaGame.Globals;
using ZeldaGame.Items;

namespace ZeldaGame.Player {
	public interface IPlayer : ICollidible {
		public void Attack();
		public void Idle();
		public bool IsIdle();
		new public void OnCollision(Rectangle intersect);
		public void Walk();
		public void UseItem(PlayerProjectiles item);
		public void GetItem();
		public Direction GetDirection();
		public void SetDirection(Direction direction);
		public Swords GetSword();
		public void SetSword(Swords sword);
		public void Draw(SpriteBatch spriteBatch, Color color);
		public void Update();
		public void Reset();
		public void Knockback();
		public Vector2 GetPlayerPosition();
		public void SetPlayerPosition(Vector2 position, bool offset = true);
		new public Rectangle GetHitBox();
		public Dictionary<IPlayerProjectile, Rectangle> GetProjectileHitBoxes();
		public bool isHurting();
		public void TakeDamage(float damage);
		public void GainHealth(float heal);
		public void IncreaseMaxHealth(float hearts);
		public float GetHealth();
		public float GetMaxHealth();
	}
}