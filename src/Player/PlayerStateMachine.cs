﻿using System.Diagnostics;
using Microsoft.Xna.Framework;

namespace ZeldaGame.Player {
	public class PlayerStateMachine {
		public enum Direction { Up, Left, Down, Right };
		public enum State {Idle, Walk, Attack, UseItem}
		public Direction direction;
		public Direction prevDirection;
		public State state;
		private IPlayerSprite sprite;

		public PlayerStateMachine(IPlayerSprite sprite) {
			direction = Direction.Down;
			this.sprite = sprite;
			this.state = State.Idle;
		}

		public Direction GetDirection() {
			return direction;
		}

		public void SetDirection(Direction direction) {
			prevDirection = this.direction;
			this.direction = direction;
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