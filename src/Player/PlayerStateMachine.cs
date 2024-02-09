﻿using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
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

		public PlayerStateMachine(ISprite sprite) {
			direction = Direction.Down;
			state = State.Idle;
			health = Health.Normal;
			this.sprite = sprite;
		}

		public void BeHurt() {
			if (health != Health.Hurt) // Note: the if is needed so we only do the transition once
			{
				health = Health.Hurt;
				// Compute and construct player sprite - probably going to use decorator
			}
		}

		public void SetDirection(int direction) { // 0 = up, 1 = left, 2 = down, 3 = right
			this.direction = (Direction)direction;
		}

		public int GetDirection() { // may be unneccessary, possible delete
			return (int)direction;
		}

		public void Idle() {
			if (state != State.Idle) {
				state = State.Idle;
				switch (direction) { // switch case is repetitive, could create sprite selector class + method
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
		}
		public void Walk() {
			if (state != State.Walk) {
				state = State.Walk;
				switch (direction) { // switch case is repetitive, could create sprite selector class + method
					case Direction.Up:
						sprite = PlayerSpriteFactory.Instance.CreateWalkUpPlayer();
						break;
					case Direction.Left:
						sprite = PlayerSpriteFactory.Instance.CreateWalkLeftPlayer();
						break;
					case Direction.Down:
						sprite = PlayerSpriteFactory.Instance.CreateWalkDownPlayer();
						break;
					case Direction.Right:
						sprite = PlayerSpriteFactory.Instance.CreateWalkRightPlayer();
						break;
				}
			}
		}
		public void Attack() {
			state = State.Attack;
			// change sprite to attack
		}
		public void PickUp() {
			state = State.PickUp;
			// change sprite to pick up item
		}
		public void UseItem() {
			state = State.UseItem;
			// change sprite to use item
		}
		public void Block() {
			state = State.Block;
			// change sprite to block
		}

		public void Draw(SpriteBatch spriteBatch, Vector2 location) {
			sprite.Draw(spriteBatch, location);
		}
	}
}