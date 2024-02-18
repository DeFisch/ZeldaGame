using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;
using static ZeldaGame.Player.PlayerStateMachine;
using static ZeldaGame.Player.WeaponHandler;

namespace ZeldaGame.Player {
	public class Player1 : IPlayer
	{
		private PlayerStateMachine stateMachine;
		private WeaponHandler weaponHandler;
		private ISprite sprite;
		private Vector2 position;
		private Vector2 movement;
		private Vector2 resetPosition;
		private static bool isMoving;

		private int speed;
		private Direction direction;
		private Swords currSword;
		private int animTimer;

		public Player1(Vector2 window_size)
		{
			direction = Direction.Down;
			sprite = PlayerSpriteFactory.Instance.CreateWalkSprite(direction);
			stateMachine = new PlayerStateMachine(sprite);
			weaponHandler = new WeaponHandler();

			currSword = weaponHandler.currSword;
			position = new Vector2(window_size.X / 2, window_size.Y / 2);
			resetPosition = position;
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

		public Swords GetSword() {
			return currSword;
		}
		public void SetSword(Swords sword) {
			currSword = sword;
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
			if (animTimer < 0) {
				UpdateMovementVector();
				sprite = stateMachine.Walk();
				isMoving = true;
			}
		}

		public void Attack()
		{
			if (animTimer < 0 && currSword != Swords.None) {
				animTimer = 16;
				sprite = stateMachine.Attack();
				weaponHandler.UseSword((int)currSword, position, stateMachine.GetDirection());
			}
		}
		public void PickUp()
		{
			// change sprite to pick up item
		}

		public void UseItem(int item)
		{
			if (animTimer < 0) {
				animTimer = 20;
				sprite = stateMachine.UseItem();
				weaponHandler.UseItem(item, position, stateMachine.GetDirection());
			}
		}

		public void Update()
		{
			// Animates attack or item use, then resets to idle
			if (animTimer >= 0)
				animTimer--;
			if (animTimer == 0) {
				sprite = PlayerSpriteFactory.Instance.CreateWalkSprite(direction);
				Idle();
			}

			// if player is moving, Walk() updates, otherwise Idle()
            if (isMoving)
			{
				position += movement;
				//animTimer = 0;
				isMoving = false; // Set to false, will make Link idle if Walk() does not get called again
			} else
			{
				Idle();
			}

			// Updates player sprite and weapon sprites
			weaponHandler.Update();
            sprite.Update();
		}
		public void Draw(SpriteBatch spriteBatch)
		{
			weaponHandler.Draw(spriteBatch);
			sprite.Draw(spriteBatch, position);
		}

        public void Reset()
        {
            sprite = PlayerSpriteFactory.Instance.CreateWalkSprite(Direction.Down);
            stateMachine = new PlayerStateMachine(sprite);
            position = resetPosition;
            movement = new Vector2(0, 0);
            animTimer = -1;
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