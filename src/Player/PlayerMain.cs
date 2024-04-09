using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Diagnostics;
using ZeldaGame.Items;
using static ZeldaGame.Player.PlayerActionHandler;
using static ZeldaGame.Globals;

namespace ZeldaGame.Player {
	public class PlayerMain : IPlayer
	{
		private PlayerActionHandler actionHandler;
		private readonly WeaponHandler weaponHandler;
		private IPlayerSprite sprite;
		private Vector2 position;
		private Vector2 movement;
		public Vector2 scale;
		private Vector2 resetPosition;
		private bool isMoving;

        private readonly float speed = 1.25f;
		private int knockbackTimer = 0;
		private readonly int knockbackScale = 8;
		private Direction direction;
		private Vector2 knockback;
		private Swords currSword;
		private int animTimer;
        private static float health = 3.0f;
		private float maxHealth = 3f;

		public PlayerMain(Vector2 position, Vector2 scale, Game1 game)
		{
			direction = Direction.Down;
			sprite = PlayerSpriteFactory.Instance.CreateWalkSprite(direction);
            actionHandler = new PlayerActionHandler(sprite);
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

		public Rectangle GetHitBox()
		{
			Rectangle hitbox = sprite.GetHitBox();
			hitbox.Inflate(-6, -8);
			return hitbox;
		}

		public void Knockback()
		{
			knockbackTimer = 10;	// Initiates countdown in Update() that pushes player position back
        }

		public Vector2 GetPlayerPosition()
		{
			return position;
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

		public Direction GetDirection() {
			return direction;
		}

		public void SetDirection(Direction direction) { // 0 = up, 1 = left, 2 = down, 3 = right
			actionHandler.SetDirection(direction);
			this.direction = direction;
		}

		public bool IsIdle() {
			State state = actionHandler.GetState();
			return state == State.Idle;
		}

		public void Idle() {
			actionHandler.Idle();
		}

		public void OnCollision(Rectangle collision)
		{
            Rectangle collisionOverlap = Rectangle.Intersect(GetHitBox(), collision);

			if (collisionOverlap.Width > collisionOverlap.Height){
				if (collisionOverlap.Center.Y < GetHitBox().Center.Y)
				{
					position.Y += collisionOverlap.Height;
					knockback = new Vector2(0, knockbackScale);	   // Sets a directional Vector2 that will knockback player
				}
				else
				{
                    position.Y -= collisionOverlap.Height;
					knockback = new Vector2(0, -knockbackScale);
                }
			} else {
				if (collisionOverlap.Center.X < GetHitBox().Center.X)
				{
					position.X += collisionOverlap.Width;
					knockback = new Vector2(knockbackScale, 0);
				}
				else
				{
					position.X -= collisionOverlap.Width;
                    knockback = new Vector2(-knockbackScale, 0);

                }
            }
        }

        public void Walk()
		{
			if (animTimer < 0)
			{
				UpdateMovementVector();
				sprite = actionHandler.Walk();
				isMoving = true;
			}
		}

		public void Attack()
		{
			if (animTimer < 0 && currSword != Swords.None) {
				animTimer = 16;
				sprite = actionHandler.Attack();
				weaponHandler.UseSword(currSword, position, actionHandler.GetDirection(), health, maxHealth);
			}
		}

		public void UseItem(PlayerProjectiles item)
		{
			if (animTimer < 0) {
				animTimer = 15;
				sprite = actionHandler.UseItem();
                if (item == PlayerProjectiles.Bomb)
                {
					if (ItemActionHandler.inventoryCounts[8] > 0)
					{
						weaponHandler.UseItem(item, position, actionHandler.GetDirection());
						ItemActionHandler.inventoryCounts[8]--;
					}
                }
				else
				{
                    weaponHandler.UseItem(item, position, actionHandler.GetDirection());
                }
            }
		}

		public bool isHurting()
		{
			return false;
		}

		public void TakeDamage(float damage)
		{
			health -= damage;
			Debug.WriteLine("Health: " + health);
		}

		public void GainHealth(float heal)
		{
			health += heal;
			if (health > maxHealth) {
				health = maxHealth;
			}
		}

		public void IncreaseMaxHealth(float hearts) {
			maxHealth += hearts;
			if (maxHealth > 16.0f) {
				maxHealth = 16.0f;
			}
		}

		public float GetHealth()
		{
			return health;
		}

		public float GetMaxHealth() {
			return maxHealth;
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

        public void Update()
        {
            // Animates attack or item use, then resets to idle
            if (animTimer >= 0)
            {
                animTimer--;
            }
            if (animTimer == 0)
            {
                sprite = PlayerSpriteFactory.Instance.CreateWalkSprite(direction);
                Idle();
            }

			// Knockback player
			if (knockbackTimer > 0)
			{
				position += knockback;
				knockbackTimer--;
			}

            // if player is moving, Walk() updates, otherwise Idle()
            if (isMoving)
            {
                position += movement;
                isMoving = false; // Set to false, will make Link idle if Walk() does not get called again
            }
            else
            {
                Idle();
            }

            // Updates player sprite and weapon sprites
            weaponHandler.Update();
            sprite.Update();
        }

        public void Reset()
        {
            sprite = PlayerSpriteFactory.Instance.CreateWalkSprite(Direction.Down);
            actionHandler = new PlayerActionHandler(sprite);
            position = resetPosition;
            movement = new Vector2(0, 0);
            animTimer = -1;
			health = 3.0f;
			maxHealth = 3.0f;
			knockbackTimer = 0;
        }
    }
}