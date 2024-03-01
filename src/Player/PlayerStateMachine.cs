using System.Diagnostics;

namespace ZeldaGame.Player {
	public class PlayerStateMachine {
		public enum Direction { Up, Left, Down, Right };
		public enum State { Idle, Walk, Attack, PickUp, UseItem, Block, Colliding };

		public Direction direction;
		public Direction prevDirection;
		private State state;
		private IPlayerSprite sprite;

		public PlayerStateMachine(IPlayerSprite sprite) {
			direction = Direction.Down;
			state = State.Idle;
			this.sprite = sprite;
		}

		public Direction GetDirection() {
			return direction;
		}

		public State GetCurrentState() {
			return state;
		}

		public void SetDirection(Direction direction) {
			prevDirection = this.direction;
			this.direction = direction;
		}

		public int GetState() {
			return (int)state;
		}

		public void Colliding()
		{
			Debug.WriteLine("Colliding");
			state = State.Colliding;
        }

        public void Idle()
		{
            state = State.Idle;
            sprite.Pause();
		}

		public IPlayerSprite Walk()
		{
			state = State.Walk;
			sprite = PlayerSpriteFactory.Instance.CreateWalkSprite(direction);
            sprite.Play();
			return sprite;
		}

		public IPlayerSprite Attack() {
			state = State.Attack;
			sprite = PlayerSpriteFactory.Instance.CreateUseItemSprite(direction);

			return sprite;
		}

		public void PickUp() {
			state = State.PickUp;
		}

		public IPlayerSprite UseItem() {
			state = State.UseItem;
			sprite = PlayerSpriteFactory.Instance.CreateUseItemSprite(direction);
			return sprite;
		}

		public void Block() {
			state = State.Block;
		}
	}
}