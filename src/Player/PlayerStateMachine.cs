using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using System.Text;
using System.Threading.Tasks;
using Sprint0;
using System.Diagnostics;

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

		public void Idle()
		{
            state = State.Idle;
            sprite.Pause();	
		}

		public ISprite Walk()
		{
			state = State.Walk;
			sprite = PlayerSpriteFactory.Instance.CreateWalkSprite(direction);
            sprite.Play();
			return sprite;
		}

		public ISprite Attack() {
			prevState = state;
			state = State.Attack;
			sprite = PlayerSpriteFactory.Instance.CreateAttackSprite(direction);
			animTimer = 12; //actual time

			return sprite;
		}

		public void PickUp() {
			prevState = state;
			state = State.PickUp;
			// change sprite to pick up item
		}

		public ISprite UseItem() {
			//prevState = state;
			state = State.UseItem;
			sprite = PlayerSpriteFactory.Instance.CreateUseItemSprite(direction);
			//animTimer = 12; //random number, will change l8r
			return sprite;
		}

		public void Block() {
			prevState = state;
			state = State.Block;
			animTimer = 5; //idk actual anim timer
			// change sprite to block
		}
	}
}