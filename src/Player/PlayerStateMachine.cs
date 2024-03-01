using System.Diagnostics;
using Microsoft.Xna.Framework;

namespace ZeldaGame.Player {
	public class PlayerStateMachine {
		public enum Direction { Up, Left, Down, Right };

		public Direction direction;
		public Direction prevDirection;
		private IPlayerSprite sprite;

		public PlayerStateMachine(IPlayerSprite sprite) {
			direction = Direction.Down;
			this.sprite = sprite;
		}

		public Direction GetDirection() {
			return direction;
		}

		public void SetDirection(Direction direction) {
			prevDirection = this.direction;
			this.direction = direction;
		}

        public void Idle()
		{
            sprite.Pause();
		}

		public IPlayerSprite Walk()
		{
			sprite = PlayerSpriteFactory.Instance.CreateWalkSprite(direction);
            sprite.Play();
			return sprite;
		}

		public IPlayerSprite Attack() {
			sprite = PlayerSpriteFactory.Instance.CreateUseItemSprite(direction);
			return sprite;
		}

		public IPlayerSprite UseItem() {
			sprite = PlayerSpriteFactory.Instance.CreateUseItemSprite(direction);
			return sprite;
		}
	}
}