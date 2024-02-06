using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeldaGame.Player {
	public class PlayerStateMachine {
		enum Direction { Up, Left, Down, Right };
		enum State { Idle, Walk, Attack, PickUp, UseItem, Block };
		enum Health { Normal, Hurt };

		private Direction direction;
		private State state;
		private Health health;
		private ISprite sprite;

		public void SetDirection(int direction) { // 0 = up, 1 = left, 2 = down, 3 = right
			this.direction = (Direction)direction;
		}

		public int GetDirection() {
			return (int)direction;
		}

		public void Idle() {
			switch (direction) {
				case Direction.Up:
					sprite = PlayerSpriteFactory.Instance.CreateIdleUpPlayer();
					break;
				case Direction.Left:
					sprite = PlayerSpriteFactory.Instance.CreateIdleLeftPlayer();
					break;
				case Direction.Down:
					sprite = PlayerSpriteFactory.Instance.CreateIdleDownPlayer();
					break;
				case Direction.Right:
					sprite = PlayerSpriteFactory.Instance.CreateIdleRightPlayer();
					break;

			}
		}
		public void Walk() {
			// change sprite to walk based on direction
		}
		public void Attack() {
			// change sprite to attack
		}
		public void PickUp() {
			// change sprite to pick up item
		}
		public void UseItem() {
			// change sprite to use item
		}
		public void Block() {
			// change sprite to block
		}
		public void BeHurt() {
			if (health != Health.Hurt) // Note: the if is needed so we only do the transition once
			{
				health = Health.Hurt;
				// Compute and construct player sprite - probably going to use decorator
			}
		}
		public void Draw(SpriteBatch spriteBatch) {
			spriteBatch.Draw();
		}
	}
}