using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static ZeldaGame.Player.PlayerStateMachine;

namespace ZeldaGame.Player {
	public class HurtPlayer : IPlayer {
		private IPlayer decoratedPlayer;

		Game1 game;
		int timer = 100;

		public HurtPlayer(IPlayer decoratedPlayer, Game1 game) {
			this.decoratedPlayer = decoratedPlayer;
			this.game = game;
		}

		public void Attack() {
			decoratedPlayer.Attack();
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
	}
}