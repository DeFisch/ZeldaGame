using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeldaGame.Player {
	public class Player1 : IPlayer {
		PlayerStateMachine stateMachine;
		ISprite sprite = PlayerSpriteFactory.Instance.CreateIdleDownPlayer();
		Vector2 position;
		int direction;
		int speed = 2;

		public Player1(Vector2 window_size) {
			stateMachine = new PlayerStateMachine();
			position = new Vector2(window_size.X/2, window_size.Y/2);
			direction = 2; //down
		}

		public void TakeDamage() {
			stateMachine.BeHurt();
		}

		public void SetDirection(int direction) {// 0 = up, 1 = left, 2 = down, 3 = right
			stateMachine.SetDirection(direction);
		}

		public void Walk() {
			stateMachine.Walk();
			switch (direction) {
				case 0:
					position.Y -= speed;
					break;
				case 1:
					position.X -= speed;
					break;
				case 2:
					position.Y += speed;
					break;
				case 3:
					position.X += speed;
					break;
				default:
					break;
			}
		}
		public void Attack() {
			// change sprite to attack
		}
		public void PickUp() {
			// change sprite to pick up item
		}
		public void UseItem() {
			// change sprite to use item
		}
		public void Block() {
			// change sprite to block
		}

		public void Update() {
			//sprite animator update
		}
		public void Draw(SpriteBatch spriteBatch) {
			sprite.Draw();
		}
	}
}