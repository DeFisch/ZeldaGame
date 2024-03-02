using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Diagnostics;
using static ZeldaGame.Player.PlayerStateMachine;
using static ZeldaGame.Player.WeaponHandler;

namespace ZeldaGame.Player {
	public class Player1 : IPlayer
	{
		private PlayerStateMachine stateMachine;
		private WeaponHandler weaponHandler;
		private IPlayerSprite sprite;
		private Vector2 position;
		private Vector2 movement;
		public Vector2 scale;
		private Vector2 resetPosition;
		private bool isMoving;
		private bool isColliding;
		private Direction collisionDirection;
		private Rectangle collision;

		private int speed;
		private Direction direction;
		private Swords currSword;
		private int animTimer;

		public Player1(Vector2 position, Vector2 scale)
		{
			direction = Direction.Down;
			sprite = PlayerSpriteFactory.Instance.CreateWalkSprite(direction);
			stateMachine = new PlayerStateMachine(sprite);
			weaponHandler = new WeaponHandler();

			currSword = weaponHandler.currSword;
			this.position = position;
			resetPosition = position;
			movement = new Vector2(0, 0);
			this.scale = scale;
			speed = 1;
			animTimer = -1;

			isColliding = false;
			isMoving = false;
		}

		public List<Rectangle> GetProjectileHitBoxes()
		{
			return weaponHandler.GetActiveHitBoxes();
		}

		public Rectangle GetPlayerHitBox()
		{
			return sprite.GetHitBox();
        }

		public IPlayerSprite GetSprite() {
			return sprite;
		}

		public void SetSprite(IPlayerSprite sprite) {
			this.sprite = sprite;
		}

        public void SetPlayerPosition(Vector2 position, bool offset = true)
        {
            Rectangle sprite_hitbox = sprite.GetHitBox();
            Vector2 sprite_size = new Vector2(sprite_hitbox.Width, sprite_hitbox.Height);
            if(offset) this.position = position - sprite_size / 2;
        }

        public Swords GetSword() {
			return currSword;
		}
		public void SetSword(Swords sword) {
			currSword = sword;
		}

		public void SetDirection(Direction direction) {// 0 = up, 1 = left, 2 = down, 3 = right
			stateMachine.SetDirection(direction);
			this.direction = direction;
		}

		public void Idle() {
			stateMachine.Idle();
			isColliding = false;
		}

		public void Colliding(Rectangle collision)
		{
            Rectangle collisionOverlap = Rectangle.Intersect(GetPlayerHitBox(), collision);
			this.collision = collision;

			if (!isColliding)
				collisionDirection = direction;

			switch (direction)
			{
				case Direction.Right:
					if (collisionDirection == Direction.Right)
						position.X -= collisionOverlap.Width; break;
				case Direction.Left:
                    if (collisionDirection == Direction.Left) 
						position.X += collisionOverlap.Width; break;
				case Direction.Up:
                    if (collisionDirection == Direction.Up)
                        position.Y += collisionOverlap.Height; break;
				case Direction.Down:
                    if (collisionDirection == Direction.Down) 
						position.Y -= collisionOverlap.Height; break;
			}
			isColliding = true;
			
		}

		public void Walk()
		{
			if (animTimer < 0)
			{
				UpdateMovementVector();
				sprite = stateMachine.Walk();
				isMoving = true;
			}
			if (!GetPlayerHitBox().Intersects(collision))
			{
				isColliding = false;
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

		public void UseItem(int item)
		{
			if (animTimer < 0) {
				animTimer = 15;
				sprite = stateMachine.UseItem();
				weaponHandler.UseItem(item, position, stateMachine.GetDirection());
			}
		}

		public void Update()
		{
			Debug.WriteLine("Colliding: " + isColliding);
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

		public void Draw(SpriteBatch spriteBatch, Color color)
		{
			weaponHandler.Draw(spriteBatch, scale);
			sprite.Draw(spriteBatch, position, color, scale);
		}

        public void Reset()
        {
            sprite = PlayerSpriteFactory.Instance.CreateWalkSprite(Direction.Down);
            stateMachine = new PlayerStateMachine(sprite);
            position = resetPosition;
            movement = new Vector2(0, 0);
            animTimer = -1;
        }

        private void UpdateMovementVector()
		{
            switch (direction)
            {
                case Direction.Up: movement = new Vector2(0, -speed); break;
				case Direction.Left: movement = new Vector2(-speed, 0); break;
                case Direction.Down: movement = new Vector2(0, speed); break;
                case Direction.Right: movement = new Vector2(speed, 0); break;
                default: break;
            }
			movement *= scale;
        }
	}
}