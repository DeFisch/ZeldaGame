using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Enemy;

namespace ZeldaGame.Enemy;

public class Goriya : IEnemy {
	enum State { Idle, Walking, Attacking, Dead };
	enum Direction { Up, Down, Left, Right };
	private int frameID = 0;
	private Texture2D texture;
	private Vector2 position;
	private State state;
	private Direction direction;
    private EnemyProjectileFactory enemyProjectileFactory;
	private Vector2 scale;
	private int[,] character_sprites = new int[,] { {222, 11, 16, 16 } , { 239, 11, 16, 16 } , { 256, 11, 16, 16 } , { 273, 11, 16, 16 } }; // x, y, width, height
	private int speed = 2;
    private int projectile_speed = 2;
    private int sprite_id = 0;
    private int rand_seed;
	private int health = 3;
	private int dead_timer = 0;
	private int iFrame = -100;
	private float damage = 0.5f;
	public Goriya(Texture2D texture, Vector2 position, EnemyProjectileFactory enemyProjectileFactory, string color, Vector2 scale) {
		this.texture = texture;
		this.position = position;
		state = State.Walking;
		direction = Direction.Down;
        this.enemyProjectileFactory = enemyProjectileFactory;
        rand_seed = new Random().Next();
        if (color == "blue") {
            character_sprites[0, 1] = 28;
            character_sprites[1, 1] = 28;
            character_sprites[2, 1] = 28;
            character_sprites[3, 1] = 28;
        }
		this.scale = scale;
	}

    public float DoDamage()
    {
        return damage;
    }

    public void Draw(SpriteBatch spriteBatch) {
		Rectangle sourceRectangle = new Rectangle(character_sprites[sprite_id, 0], character_sprites[sprite_id, 1], character_sprites[sprite_id, 2], character_sprites[sprite_id, 3]);
		Rectangle destinationRectangle = new Rectangle((int)position.X, (int)position.Y, (int)(character_sprites[sprite_id, 2] * scale.X), (int)(character_sprites[sprite_id, 3] * scale.Y));
		SpriteEffects sprite_effect = SpriteEffects.None;
		if (direction == Direction.Left)
			sprite_effect = SpriteEffects.FlipHorizontally;
        if ((direction == Direction.Up || direction == Direction.Down) && (frameID / 20) % 2 == 0)
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

		if (health <= 0){
			state = State.Dead;
		}

        // animate sprite
        if (direction == Direction.Up){
            sprite_id = 1;
        }else if (direction == Direction.Down){
            sprite_id = 0;
        }else{
            sprite_id = frameID/20 % 2 == 0 ? 2 : 3;
        }
		if (state == State.Walking)
			Walk();
		if (state == State.Idle)
			Idle();
        if (state == State.Attacking)
            Attack();
		if (state == State.Dead)
			Dead();
		frameID++;
	}


	private void Dead() {
		float vel = -7.81f;
		vel += 0.605f * dead_timer;
		position.Y += vel;
		dead_timer++;
	}

	public bool IsFinished() {
		return dead_timer > 60;
	}

    private void Attack() {
        if (frameID % 30 == 0) {
            ShootProjectile();
        }
    }

    private void ShootProjectile() {
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
        enemyProjectileFactory.CreateProjectile(EnemyProjectileFactory.ProjectileType.Boomerang, new Vector2(GetRectangle().Center.X, GetRectangle().Center.Y), projectile_direction);
    }

	private void Idle() {
		if (frameID % 120 == 0) 
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
				if (position.Y < 600 - character_sprites[0,3] * scale.Y)
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
				if (position.X < 800 - character_sprites[0,2] * scale.X)
					position.X += speed;
				else
					direction = Direction.Left;
				break;
		}
	}

	public bool TakeDamage(int damage)
    {
		if (frameID - iFrame < 60)
			return false;
		health -= damage;
		iFrame = frameID;
        return true;
    }

    public int GetHealth()
    {
        return health;
    }

    public Rectangle GetRectangle(){
		return new Rectangle((int)position.X, (int)position.Y, (int)(character_sprites[0, 2] * scale.X), (int)(character_sprites[0, 3] * scale.Y));
	}

	public void Collide(Rectangle rectangle)
	{
		int dx = rectangle.Width;
		int dy = rectangle.Height;
		switch(direction){
			case Direction.Up:
				position.Y += dy;
				break;
			case Direction.Down:
				position.Y -= dy;
				break;
			case Direction.Left:
				position.X += dx;
				break;
			case Direction.Right:
				position.X -= dx;
				break;
		}
		Direction old_direction = direction;
		while (direction == old_direction)
			direction = (Direction)(new Random().Next(0, 4));
	}
}
