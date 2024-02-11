using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Sprint0;

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

		public int GetDirection() { // may be unneccessary, possible delete
			return (int)direction;
		}

		public void SetDirection(int direction) { // 0 = up, 1 = left, 2 = down, 3 = right
			this.direction = (Direction)direction;
		}

		public int GetState() {
			return (int)state;
		}

		public void Idle() {
			if (state != State.Idle) {
				state = State.Idle;
				switch (direction) { // switch case is repetitive, could create sprite selector class + method
					case Direction.Up:
						sprite = PlayerSpriteFactory.Instance.CreateIdleUpSprite();
						break;
					case Direction.Left:
						sprite = PlayerSpriteFactory.Instance.CreateIdleLeftSprite();
						break;
					case Direction.Down:
						sprite = PlayerSpriteFactory.Instance.CreateIdleDownSprite();
						break;
					case Direction.Right:
						sprite = PlayerSpriteFactory.Instance.CreateIdleRightSprite();
						break;
				}
			}
		}
		public void Walk() {
			if (state != State.Walk) {
				state = State.Walk;
				switch (direction) { // switch case is repetitive, could create sprite selector class + method
					case Direction.Up:
						sprite = PlayerSpriteFactory.Instance.CreateWalkUpSprite();
						break;
					case Direction.Left:
						sprite = PlayerSpriteFactory.Instance.CreateWalkLeftSprite();
						break;
					case Direction.Down:
						sprite = PlayerSpriteFactory.Instance.CreateWalkDownSprite();
						break;
					case Direction.Right:
						sprite = PlayerSpriteFactory.Instance.CreateWalkRightSprite();
						break;
				}
			}
		}
		public ISprite Attack() {
            if (state != State.Attack)
            {
                state = State.Attack;
                switch (direction)
                { // switch case is repetitive, could create sprite selector class + method
                    case Direction.Up:
                        sprite = PlayerSpriteFactory.Instance.CreateAttackUpSprite();
                        break;
                    case Direction.Left:
                        sprite = PlayerSpriteFactory.Instance.CreateAttackLeftSprite();
                        break;
                    case Direction.Down:
                        sprite = PlayerSpriteFactory.Instance.CreateAttackDownSprite();
                        break;
                    case Direction.Right:
                        sprite = PlayerSpriteFactory.Instance.CreateAttackRightSprite();
                        break;
                }
            }
			return sprite;
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