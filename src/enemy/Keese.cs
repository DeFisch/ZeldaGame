using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ZeldaGame.Items;
using static ZeldaGame.Player.PlayerActionHandler;

namespace ZeldaGame.Enemy;

public class Keese : IEnemy {
	enum State { Idle, Walking, Attacking, Dead };
	private int frameID = 0;
	private Texture2D texture;
	private Vector2 position;
	private State state;
	private Vector2 scale;
    private Vector2 knockback;
    private int knockbackTimer;
    private readonly int knockbackScale = 8;
    private int[,] character_sprites = new int[,] { { 183, 11, 16, 16 }, { 200, 11, 16, 16 } }; // x, y, width, height
	private Vector3 map_size;
	private double speedX = 0;
	private double speedY = 0;
	private int health = 1;
	private double general_speed = 4;
	private int dead_timer = 0;
	private int iFrame = -100;
    private float damage = 0.5f;

    public Keese(Texture2D texture, Vector2 position, string color, Vector3 map_size, Vector2 scale) {
		this.texture = texture;
		this.position = position;
		state = State.Walking;
		speedX = new Random().NextDouble() * general_speed - general_speed / 2;
		speedY = new Random().NextDouble() * general_speed - general_speed / 2;
		if (color == "blue") {
			character_sprites[0, 1] = 28;
			character_sprites[1, 1] = 28;
		}
		this.map_size = map_size;
		this.scale = scale;
    }

    public float DoDamage()
    {
        return damage;
    }

    public void Knockback(Direction knockbackDirection)
    {
        switch (knockbackDirection)
        {
            case Direction.Up:
                knockback = new Vector2(0, -knockbackScale); break;
            case Direction.Down:
                knockback = new Vector2(0, knockbackScale); break;
            case Direction.Left:
                knockback = new Vector2(-knockbackScale, 0); break;
            case Direction.Right:
                knockback = new Vector2(knockbackScale, 0); break;
        }
        knockbackTimer = 10;
    }

    public void Draw(SpriteBatch spriteBatch) {
		int sprite_id = (frameID / 18) % 2;
		Rectangle sourceRectangle = new Rectangle(character_sprites[sprite_id, 0], character_sprites[sprite_id, 1], character_sprites[sprite_id, 2], character_sprites[sprite_id, 3]);
		Rectangle destinationRectangle = new Rectangle((int)position.X, (int)position.Y, (int)(character_sprites[sprite_id, 2] * scale.X), (int)(character_sprites[sprite_id, 3] * scale.Y));
		spriteBatch.Draw(texture, destinationRectangle, sourceRectangle, Color.White);
	}

	public void Update() {
        if (ItemActionHandler.inventoryCounts[3] > 0)
        {
            state = State.Idle;
        }else {
            state = State.Walking;

        }
		if (health <= 0) {
			state = State.Dead;
		}
        if (state == State.Walking)
			Walk();
		if (state == State.Idle)
			Idle();
		if (state == State.Dead)
			Dead();
		frameID++;

        // Knockback enemy
        if (knockbackTimer > 0)
        {
            position += knockback;
            knockbackTimer--;
        }
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

	private void Idle() {
		// Empty
	}

	public void Walk() {
		int sprite_id = (frameID / 18) % 2;
		if (frameID % 120 == 0) {
			speedX = new Random().NextDouble() * general_speed - general_speed / 2;
			speedY = new Random().NextDouble() * general_speed - general_speed / 2;
		}
		if (position.X + speedX < map_size.X - character_sprites[sprite_id,2] * scale.X &&
		position.X + speedX > 0 &&
		position.Y + speedY > map_size.Z &&
		position.Y + speedY < map_size.Y + map_size.Z - character_sprites[sprite_id,3] * scale.Y) {
			position.X += (float)speedX;
			position.Y += (float)speedY;
		}
		else {
			speedX = -speedX;
			speedY = -speedY;
			position.X += (float)speedX;
			position.Y += (float)speedY;
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

    public Rectangle GetHitBox()
	{
		int sprite_id = (frameID / 18) % 2;
		return new Rectangle((int)position.X, (int)position.Y, (int)(character_sprites[sprite_id, 2] * scale.X), (int)(character_sprites[sprite_id, 3] * scale.Y));
	}

	public void OnCollision(Rectangle rectangle)
	{
		// Intentionally left empty
	}
}
