using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ZeldaGame.Player.PlayerStateMachine;


namespace ZeldaGame.Player {
	public class Player1 : IPlayer {
		PlayerStateMachine stateMachine;
		ISprite sprite;
		Vector2 position;
		Vector2 movement;

		int speed;
		int direction;

		public Player1(Vector2 window_size) {
			sprite = PlayerSpriteFactory.Instance.CreateIdleSprite(Direction.Down);
			stateMachine = new PlayerStateMachine(sprite);
			position = new Vector2(window_size.X/2, window_size.Y/2);
			movement = new Vector2(0, 0);
			direction = 2; //down
			speed = 2;
		}

		public ISprite GetSprite() {
			return sprite;
		}

		public void SetSprite(ISprite sprite) {
			this.sprite = sprite;
		}

		public void TakeDamage() {
			stateMachine.BeHurt();
		}

		public void SetDirection(int direction) {// 0 = up, 1 = left, 2 = down, 3 = right
			stateMachine.SetDirection(direction);
			this.direction = direction;
		}

		public void Idle() {
			movement = new Vector2(0, 0);
			sprite = stateMachine.Idle();
		}

		public void Walk() {
			switch (direction) {
				case 0:
					movement = new Vector2(0, -speed);
					break;
				case 1:
					movement = new Vector2(-speed, 0);
					break;
				case 2:
					movement = new Vector2(0, speed);
					break;
				case 3:
					movement = new Vector2(speed, 0);
					break;
				default:
					break;
			}
			sprite = stateMachine.Walk();
		}
		public void Attack() {
			sprite = stateMachine.Attack();
		}
		public void PickUp() {
			// change sprite to pick up item
		}
		public void UseItem() {
			sprite = stateMachine.UseItem();
		}
		public void Block() {
			// change sprite to block
		}

		public void Update() {
			position = position + movement;
			sprite.Update();
		}
		public void Draw(SpriteBatch spriteBatch) {
			sprite.Draw(spriteBatch, position);
		}
	}
}