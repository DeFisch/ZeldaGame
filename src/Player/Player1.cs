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
		private readonly WeaponHandler weaponHandler;
		private IPlayerSprite sprite;
		private Vector2 position;
		private Vector2 movement;
		public Vector2 scale;
		private Vector2 resetPosition;
		private bool isMoving;

		private readonly int speed = 1;
		private Direction direction;
		private Swords currSword;
		private int animTimer;

		public int currHealth = 3;
		public int maxHealth = 3;

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
			animTimer = -1;

			isMoving = false;
		}

		public Dictionary<IPlayerProjectile, Rectangle> GetProjectileHitBoxes()
		{
			return weaponHandler.GetActiveHitBoxes();
		}

		public Rectangle GetPlayerHitBox()
		{
			Rectangle hitbox = sprite.GetHitBox();
			hitbox.Inflate(-6, -8);
			return hitbox;
		}

		public void SetPlayerPosition(Vector2 position, bool offset = true)
		{
			Rectangle sprite_hitbox = sprite.GetHitBox();
			Vector2 sprite_size = new Vector2(sprite_hitbox.Width, sprite_hitbox.Height);
			if (offset) this.position = position - sprite_size / 2;
		}

		public void SetSword(Swords sword) {
			currSword = sword;
		}

		public void SetDirection(Direction direction) { // 0 = up, 1 = left, 2 = down, 3 = right
			stateMachine.SetDirection(direction);
			this.direction = direction;
		}

		public void Idle() {
			stateMachine.Idle();
		}

		public void OnCollision(Rectangle collision)
		{
            Rectangle collisionOverlap = Rectangle.Intersect(GetPlayerHitBox(), collision);

			if (collisionOverlap.Width > collisionOverlap.Height){
				if (collisionOverlap.Center.Y < GetPlayerHitBox().Center.Y)
					position.Y += collisionOverlap.Height;
				else
					position.Y -= collisionOverlap.Height;
			} else {
				if (collisionOverlap.Center.X < GetPlayerHitBox().Center.X)
					position.X += collisionOverlap.Width;
				else
					position.X -= collisionOverlap.Width;
			}
		}

		
		public void Walk()
		{
			if (animTimer < 0)
			{
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
				weaponHandler.UseSword((int)currSword, position, stateMachine.GetDirection(), currHealth, maxHealth);
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
				isMoving = false; // Set to false, will make Link idle if Walk() does not get called again
			} else
			{
				Idle();
			}

			// Updates player sprite and weapon sprites
			weaponHandler.Update();
            sprite.Update();
		}

		public bool isHurting()
		{
			return false;
		}

		public void Draw(SpriteBatch spriteBatch, Color color)
		{
			weaponHandler.Draw(spriteBatch, scale);
			sprite.Draw(spriteBatch, position, color, scale);
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

        public void Reset()
        {
            sprite = PlayerSpriteFactory.Instance.CreateWalkSprite(Direction.Down);
            stateMachine = new PlayerStateMachine(sprite);
            position = resetPosition;
            movement = new Vector2(0, 0);
            animTimer = -1;
        }
    }
}