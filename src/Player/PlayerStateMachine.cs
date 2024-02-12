using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using System.Text;
using System.Threading.Tasks;
using Sprint0;

namespace ZeldaGame.Player {
	public class PlayerStateMachine {
		public enum Direction { Up, Left, Down, Right };
		public enum State { Idle, Walk, Attack, PickUp, UseItem, Block };
		public enum Health { Normal, Hurt };

		public Direction direction;
		private State state;
		private Health health;
		private ISprite sprite;
		private int animTimer;

		public PlayerStateMachine(ISprite sprite) {
			direction = Direction.Down;
			state = State.Idle;
			health = Health.Normal;
			this.sprite = sprite;
			animTimer = -1;
		}

		public void BeHurt() {
			if (health != Health.Hurt) // Note: the if is needed so we only do the transition once
			{
				health = Health.Hurt;
				// Compute and construct player sprite - probably going to use decorator
			}
		}

		public Direction GetDirection() { // may be unneccessary, possible delete
			return direction;
		}

		public State GetCurrentState()
		{
			return state;
		}

		public void SetDirection(Direction direction) { // 0 = up, 1 = left, 2 = down, 3 = right
			this.direction = direction;
		}

		public int GetState() {
			return (int)state;
		}

		public ISprite Idle() {
			if (state != State.Idle)
			{
				state = State.Idle;
				sprite = PlayerSpriteFactory.Instance.CreateIdleSprite(direction);
			}
			return sprite;
		}
		public ISprite Walk() {
			if (state != State.Walk)
			{
				state = State.Walk;
				sprite = PlayerSpriteFactory.Instance.CreateWalkSprite(direction);
			}
			return sprite;
		}
		public ISprite Attack() {
            if (state != State.Attack)
            {
                state = State.Attack;
                sprite = PlayerSpriteFactory.Instance.CreateAttackSprite(direction);
				animTimer = 12;
            }
			return sprite;
        }
		public void PickUp() {
			state = State.PickUp;
			// change sprite to pick up item
		}
		public ISprite UseItem() {
			if (state != State.UseItem)
			{
                state = State.UseItem;
				sprite = PlayerSpriteFactory.Instance.CreateUseItemSprite(direction);
            }
			return sprite;
		}
		public void Block() {
			state = State.Block;
			// change sprite to block
		}
	}
}