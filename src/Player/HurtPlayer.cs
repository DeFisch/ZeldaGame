using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using static ZeldaGame.Player.PlayerStateMachine;

namespace ZeldaGame.Player {
	public class HurtPlayer : IPlayer {
		private Player1 decoratedPlayer;

		Game1 game;
		int timer = 50;

		public HurtPlayer(IPlayer decoratedPlayer, Game1 game) {
			this.decoratedPlayer = (Player1)decoratedPlayer;
			this.game = game;
		}

		public void Attack() {
			decoratedPlayer.Attack();
		}

		public void OnCollision(Rectangle collision)
		{
            decoratedPlayer.OnCollision(collision);
        }
		public void Idle() {
			decoratedPlayer.Idle();
		}
		public void Walk() {
			decoratedPlayer.Walk();
		}
		public void UseItem(int item) {
			decoratedPlayer.UseItem(item);
		}
		public void SetDirection(Direction direction) {
			decoratedPlayer.SetDirection(direction);
		}

		public bool isHurting() {
			return true;
		}

		public void TakeDamage(float damage)
		{
			// No implementation, don't take damage when already injured
		}

		public float GetHealth()
		{
			return decoratedPlayer.GetHealth();
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
			decoratedPlayer.Draw(spriteBatch, Color.Red);
		}

		void RemoveDecorator() {
			game.Link = (Player1)decoratedPlayer;
		}

        public void SetPlayerPosition(Vector2 position, bool offset = true)
        {
			 decoratedPlayer.SetPlayerPosition(position);
        }

        public Rectangle GetPlayerHitBox()
        {
			return decoratedPlayer.GetPlayerHitBox();
        }

        public Dictionary<IPlayerProjectile, Rectangle> GetProjectileHitBoxes()
        {
            return decoratedPlayer.GetProjectileHitBoxes();
        }
    }
}