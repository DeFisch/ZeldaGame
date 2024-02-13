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
		public Direction prevDirection;
		private State state;
		private State prevState;
		private Health health;
		private ISprite sprite;
		private int animTimer;

		public PlayerStateMachine(ISprite sprite) {
			direction = Direction.Down;
			state = State.Idle;
			prevState = state;
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

		public State GetCurrentState() {
			return state;
		}

		public void SetDirection(Direction direction) { // 0 = up, 1 = left, 2 = down, 3 = right
			prevDirection = this.direction;
			this.direction = direction;
		}

		public int GetState() {
			return (int)state;
		}

		public ISprite Idle() {
			if (state != State.Idle) {
				prevState = state;
				if (prevState == State.Walk && direction == prevDirection) {
					sprite.Pause();
				}
				else {
					sprite = PlayerSpriteFactory.Instance.CreateWalkSprite(direction);
				}
				//sprite = PlayerSpriteFactory.Instance.CreateIdleSprite(direction);
				state = State.Idle;
			}
			return sprite;
		}
		public ISprite Walk() {
			if (state != State.Walk) {
				prevState = state;
				if (prevState == State.Idle && direction == prevDirection) {
					sprite.Play();
				}
				else {
					sprite = PlayerSpriteFactory.Instance.CreateWalkSprite(direction);
				}
				state = State.Walk;
			}
			return sprite;
		}
		public ISprite Attack() {
			if (state != State.Attack) {
				prevState = state;
				state = State.Attack;
				sprite = PlayerSpriteFactory.Instance.CreateAttackSprite(direction);
				animTimer = 12;
			}
			return sprite;
		}
		public void PickUp() {
			prevState = state;
			state = State.PickUp;
			// change sprite to pick up item
		}
		public ISprite UseItem() {
			if (state != State.UseItem) {
				prevState = state;
				state = State.UseItem;
				sprite = PlayerSpriteFactory.Instance.CreateUseItemSprite(direction);
			}
			return sprite;
		}
		public void Block() {
			prevState = state;
			state = State.Block;
			// change sprite to block
		}
	}
}