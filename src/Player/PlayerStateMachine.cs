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

		public void SetDirection(int direction) { // 0 = up, 1 = left, 2 = down, 3 = right
			this.direction = (Direction)direction;
		}

		public int GetDirection() {
			return (int)direction;
		}

		public void Idle() {
			// change sprite to stay still
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

	}
}