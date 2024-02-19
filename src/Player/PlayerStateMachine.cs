﻿namespace ZeldaGame.Player {
	public class PlayerStateMachine {
		public enum Direction { Up, Left, Down, Right };
		public enum State { Idle, Walk, Attack, PickUp, UseItem, Block };
		public enum Health { Full, Normal, Hurt };

		public Direction direction;
		public Direction prevDirection;
		private State state;
		private Health health;
		private ISprite sprite;

		public PlayerStateMachine(ISprite sprite) {
			direction = Direction.Down;
			state = State.Idle;
			health = Health.Full;
			this.sprite = sprite;
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
			state = State.Attack;
			sprite = PlayerSpriteFactory.Instance.CreateAttackSprite(direction);

			return sprite;
		}

		public void PickUp() {
			state = State.PickUp;
		}

		public ISprite UseItem() {
			state = State.UseItem;
			sprite = PlayerSpriteFactory.Instance.CreateUseItemSprite(direction);
			return sprite;
		}

		public void Block() {
			state = State.Block;
		}
	}
}