using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ZeldaGame.Player.PlayerStateMachine;


namespace ZeldaGame.Player {
	public class Player1 : IPlayer {
		private PlayerStateMachine stateMachine;
		private WeaponHandler weaponHandler;
		private ISprite sprite;
		private Vector2 position;
		private Vector2 movement;

		public static bool isMoving;

		int speed;
		Direction direction;
		int animTimer;

		public Player1(Vector2 window_size, WeaponHandler weaponHandler )
		{
			sprite = PlayerSpriteFactory.Instance.CreateWalkSprite(Direction.Down);
			stateMachine = new PlayerStateMachine(sprite);
			this.weaponHandler = weaponHandler;

			position = new Vector2(window_size.X / 2, window_size.Y / 2);
			movement = new Vector2(0, 0);
			speed = 2;
			animTimer = -1;

			isMoving = false;
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

		public void SetDirection(Direction direction) {// 0 = up, 1 = left, 2 = down, 3 = right
			stateMachine.SetDirection(direction);
			this.direction = direction;
		}

		public void Idle() {
			stateMachine.Idle();
		}

		public void Walk()
		{
			if (animTimer == -1) {
				UpdateMovementVector();
                sprite = stateMachine.Walk();
				isMoving = true;
            }
		}
		public void Attack()
		{
			if (animTimer == -1) {
				sprite = stateMachine.Attack();
				animTimer = 12;
			}
		}
		public void PickUp()
		{
			// change sprite to pick up item
		}
		public void UseItem(int item)
		{
			if (animTimer == -1) {
				sprite = stateMachine.UseItem();
				weaponHandler.UseItem(item, position, stateMachine.GetDirection());
				animTimer = 20;
			}
		}
		public void Block() {
			// change sprite to block
		}

		public void Update()
		{
			if (animTimer >= 0) {
				animTimer--;
			}
			if (animTimer == 0) {
				Debug.WriteLine("Done.");
				Idle();
			}

            if (isMoving)
			{
				position += movement;
				isMoving = false; // Set to false, will make Link idle if Walk() does not get called again
			} else
			{
				Idle();
			}

            sprite.Update();
		}
		public void Draw(SpriteBatch spriteBatch)
		{
			sprite.Draw(spriteBatch, position);
		}

		/*
		 * Class methods
		 */
		private void UpdateMovementVector()
		{
            switch (direction)
            {
                case Direction.Up:
                    movement = new Vector2(0, -speed);
                    break;
                case Direction.Left:
                    movement = new Vector2(-speed, 0);
                    break;
                case Direction.Down:
                    movement = new Vector2(0, speed);
                    break;
                case Direction.Right:
                    movement = new Vector2(speed, 0);
                    break;
                default:
                    break;
            }
        }

	}
}