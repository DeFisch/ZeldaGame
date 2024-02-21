using System;
using Enemy;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ZeldaGame.Enemy;

public class WizzRobe : IEnemy {
	enum State { Idle, Walking, Attacking, Dead };
	enum Direction { Up, Down, Left, Right };
	private int frameID = 0;
	private Texture2D texture;
	private Vector2 position;
	private State state;
	private Direction direction;
	private int[,] character_sprites = new int[,] { {126, 90, 16, 16 } , { 143, 90, 16, 16 } , { 160, 90, 16, 16 } , { 177, 90, 16, 16 } }; // x, y, width, height
	private int scale = 2;
	private int speed = 2;
	private int projectile_speed = 5;
    private int sprite_id = 0;
	private int rand_seed;
	private int health = 3;

	private EnemyProjectileFactory enemyProjectileFactory;
	public WizzRobe(Texture2D texture, Vector2 window_size, EnemyProjectileFactory enemyProjectileFactory, string color) {
		rand_seed = new Random().Next();
		this.texture = texture;
		position = new Vector2(new Random().Next(0, (int)window_size.X - character_sprites[0,2] * scale), new Random().Next(0, (int)window_size.Y - character_sprites[0,3] * scale));
		state = State.Walking;
		direction = Direction.Down;
		this.enemyProjectileFactory = enemyProjectileFactory;
        if (color == "red") {
            character_sprites[0, 1] = 107;
            character_sprites[1, 1] = 107;
            character_sprites[2, 1] = 107;
            character_sprites[3, 1] = 107;
        }
	}

	public void Draw(SpriteBatch spriteBatch) {
		Rectangle sourceRectangle = new Rectangle(character_sprites[sprite_id, 0], character_sprites[sprite_id, 1], character_sprites[sprite_id, 2], character_sprites[sprite_id, 3]);
		Rectangle destinationRectangle = new Rectangle((int)position.X, (int)position.Y, character_sprites[sprite_id, 2] * scale, character_sprites[sprite_id, 3] * scale);
		SpriteEffects sprite_effect = SpriteEffects.None;
		if (direction == Direction.Left)
			sprite_effect = SpriteEffects.FlipHorizontally;
		spriteBatch.Draw(texture, destinationRectangle, sourceRectangle, Color.White, rotation: 0, new Vector2(0, 0), effects: sprite_effect, 1);
	}

	public void Update() {

		if ((frameID + rand_seed) / 60 % 3 == 0) {
			state = State.Walking;
		} else if ((frameID + rand_seed) / 60 % 3 == 1) {
			state = State.Idle;
		} else {
			state = State.Attacking;
		}

        // animate sprite
        if (frameID % 20 == 0) {
            sprite_id = sprite_id % 2 == 0 ? sprite_id+1 : sprite_id-1;
        }

        if ((direction == Direction.Left || direction == Direction.Right || direction == Direction.Down) && sprite_id/2 == 1)
            sprite_id = 0; // switch to horizontal sprite
		if ((direction == Direction.Up) && sprite_id/2 == 0)
			sprite_id = 2; // switch to vertical sprite
            
		if (state == State.Walking)
			Walk();
		if (state == State.Idle)
			Idle();
		if (state == State.Attacking)
			Attack();
		frameID++;
	}

	private void Attack() {
		if( frameID % 20 == 0 ){
			ShootProjectile();
		}
	}

	public void ShootProjectile(){
		Vector2 projectile_direction;
		switch(direction){
			case Direction.Up:
				projectile_direction = new Vector2(0, -this.projectile_speed);
				break;
			case Direction.Down:
				projectile_direction = new Vector2(0, this.projectile_speed);
				break;
			case Direction.Left:
				projectile_direction = new Vector2(-this.projectile_speed, 0);
				break;
			default:
				projectile_direction = new Vector2(this.projectile_speed, 0);
				break;
		}
		enemyProjectileFactory.CreateProjectile(EnemyProjectileFactory.ProjectileType.Fireball, position, projectile_direction);
	}

	private void Idle() {
		if (frameID / 30 == 0)
			direction = (Direction)(new Random().Next(0, 4));
	}

	public void Walk() {
		if (frameID % 120 == 0) direction = (Direction)(new Random().Next(0, 4));
		switch (direction) {
			case Direction.Up:
				if (position.Y > 0)
					position.Y -= speed;
				else
					direction = Direction.Down;
				break;
			case Direction.Down:
				if (position.Y < 600 - character_sprites[0,3] * scale)
					position.Y += speed;
				else
					direction = Direction.Up;
				break;
			case Direction.Left:
				if (position.X > 0)
					position.X -= speed;
				else
					direction = Direction.Right;
				break;
			case Direction.Right:
				if (position.X < 800 - character_sprites[0,2] * scale)
					position.X += speed;
				else
					direction = Direction.Left;
				break;
		}
	}

	public void TakeDamage(int damage)
    {
		this.health -= damage;
    }
}
