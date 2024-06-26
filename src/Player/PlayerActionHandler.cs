﻿using static ZeldaGame.Globals;

namespace ZeldaGame.Player {
	public class PlayerActionHandler {
		public enum State {Idle, Walk, Attack, UseItem}
		public Direction direction;
		public Direction prevDirection;
		public State state;
		public Swords sword;
		private IPlayerSprite sprite;

		public PlayerActionHandler(IPlayerSprite sprite) {
			direction = Direction.Down;
			this.sprite = sprite;
			this.state = State.Idle;
			this.sword = Swords.Wood;
		}

		public Direction GetDirection() {
			return direction;
		}

		public void SetDirection(Direction direction) {
			prevDirection = this.direction;
			this.direction = direction;
		}

		public Swords GetSword() {
			return sword;
		}

		public void SetSword(Swords newSword) {
			if (newSword >= sword) {
				sword = newSword;
			}
		}

		public State GetState() {
			return state;
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

		public IPlayerSprite UseItem() {
			state = State.UseItem;
			sprite = PlayerSpriteFactory.Instance.CreateUseItemSprite(direction);
			return sprite;
		}
	}
}