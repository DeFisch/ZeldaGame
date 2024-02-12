using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ZeldaGame.Player.PlayerStateMachine;

namespace ZeldaGame.Player {
	public class HurtPlayer : IPlayer {
		PlayerStateMachine stateMachine;
		ISprite sprite;
		
		Vector2 position;
		int direction;
		int speed = 2;

		Game1 game;
		IPlayer decoratedPlayer;
		int timer = 1000;

		public HurtPlayer(IPlayer decoratedPlayer, Game1 game) {
			this.decoratedPlayer = decoratedPlayer;
			sprite = PlayerSpriteFactory.Instance.CreateIdleSprite(Direction.Down);
			this.game = game;
		}

		public void TakeDamage() {
			// doesn't take damage
		}

		public void SetDirection(int direction) {// 0 = up, 1 = left, 2 = down, 3 = right
			stateMachine.SetDirection((Direction)direction);
		}

		public void Update() {
			timer--;
			if (timer == 0) {
				RemoveDecorator();
			}

			decoratedPlayer.Update();
		}

		public void Draw(SpriteBatch spriteBatch) {
			sprite.Draw(spriteBatch, position);
		}

		void RemoveDecorator() {
			game.Link = (Player1)decoratedPlayer;
		}
	}
}