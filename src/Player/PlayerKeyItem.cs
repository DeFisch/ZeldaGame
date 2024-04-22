using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Diagnostics;
using ZeldaGame.Items;
using static ZeldaGame.Globals;

namespace ZeldaGame.Player {
	public class PlayerKeyItem : IPlayer {
		private readonly PlayerMain decoratedPlayer;

		private readonly Game1 game;
		private int timer = 64;

		public PlayerKeyItem(IPlayer decoratedPlayer, Game1 game) {
			this.decoratedPlayer = (PlayerMain)decoratedPlayer;
			this.game = game;
		}

		public void Attack() {
			decoratedPlayer.Attack();
		}

		public void OnCollision(Rectangle intersect)
		{
            decoratedPlayer.OnCollision(intersect);
        }
		public void Idle() {
			decoratedPlayer.Idle();
		}
		public void Walk() {
			decoratedPlayer.Walk();
		}
		public void UseItem(PlayerProjectiles item) {
			decoratedPlayer.UseItem(item);
		}

		public void GetItem() {
			decoratedPlayer.GetItem();
		}
		public Swords GetSword() {
			return decoratedPlayer.GetSword();
		}
		public void SetSword(Swords newSword) {
			decoratedPlayer.SetSword(newSword);
		}
		public Direction GetDirection() {
			return decoratedPlayer.GetDirection();
		}
		public void SetDirection(Direction direction) {
			decoratedPlayer.SetDirection(direction);
		}
		public bool isHurting() {
			return decoratedPlayer.isHurting();
		}

		public bool IsIdle() {
			return decoratedPlayer.IsIdle();
		}
		public void TakeDamage(float damage)
		{
			// No implementation, don't take damage when already injured
		}
		public void GainHealth(float heal)
		{
			decoratedPlayer.GainHealth(heal);
		}
		public void IncreaseMaxHealth(float hearts) {
			decoratedPlayer.IncreaseMaxHealth(hearts);
		}
		public float GetHealth()
		{
			return decoratedPlayer.GetHealth();
		}

		public float GetMaxHealth() {
			return decoratedPlayer.GetMaxHealth();
        }

        public void Knockback()
        {
			decoratedPlayer.Knockback();
        }

        public Vector2 GetPlayerPosition()
        {
            return decoratedPlayer.GetPlayerPosition();
        }

        public void SetPlayerPosition(Vector2 position, bool offset = true)
        {
            decoratedPlayer.SetPlayerPosition(position);
        }

        public Rectangle GetHitBox()
        {
			return new Rectangle(0,0,0,0);
        }

        public Dictionary<IPlayerProjectile, Rectangle> GetProjectileHitBoxes()
        {
            return decoratedPlayer.GetProjectileHitBoxes();
        }

        public void Update() {
			timer--;
			if (timer == 0) {
				RemoveDecorator();
			}

            decoratedPlayer.Update();
		}
		public void Reset() {
			decoratedPlayer.Reset();
			RemoveDecorator();
		}

		public void Draw(SpriteBatch spriteBatch, Color color) {
			decoratedPlayer.Draw(spriteBatch, Color.White);
		}

		private void RemoveDecorator() {
			game.Link = (PlayerMain)decoratedPlayer;
		}
    }
}