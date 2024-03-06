using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ZeldaGame.Enemy;

public class Keese : IEnemy {
	enum State { Idle, Walking, Attacking, Dead };
	private int frameID = 0;
	private Texture2D texture;
	private Vector2 position;
	private State state;
	private Vector2 scale;
	private int[,] character_sprites = new int[,] { { 183, 11, 16, 16 }, { 200, 11, 16, 16 } }; // x, y, width, height
	private double speedX = 0;
	private double speedY = 0;
	private int health = 1;
	private double general_speed = 4;
	private int dead_timer = 0;
	public Keese(Texture2D texture, Vector2 position, string color, Vector2 scale) {
		this.texture = texture;
		this.position = position;
		state = State.Walking;
		this.speedX = new Random().NextDouble() * general_speed - general_speed / 2;
		this.speedY = new Random().NextDouble() * general_speed - general_speed / 2;
		if (color == "blue") {
			character_sprites[0, 1] = 28;
			character_sprites[1, 1] = 28;
		}
		this.scale = scale;
	}

	public void Draw(SpriteBatch spriteBatch) {
		int sprite_id = (frameID / 18) % 2;
		Rectangle sourceRectangle = new Rectangle(character_sprites[sprite_id, 0], character_sprites[sprite_id, 1], character_sprites[sprite_id, 2], character_sprites[sprite_id, 3]);
		Rectangle destinationRectangle = new Rectangle((int)position.X, (int)position.Y, (int)(character_sprites[sprite_id, 2] * scale.X), (int)(character_sprites[sprite_id, 3] * scale.Y));
		spriteBatch.Draw(texture, destinationRectangle, sourceRectangle, Color.White);
	}

	public void Update() {
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
		if (position.X > 0 && position.X < 800 - character_sprites[sprite_id, 2] * scale.X && position.Y > 0 && position.Y < 600 - character_sprites[sprite_id, 3] * scale.Y) {
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
	
	public void TakeDamage(int damage)
    {
		this.health -= damage;
    }

    public int GetHealth()
    {
        return health;
    }

    public Rectangle GetRectangle()
	{
		int sprite_id = (frameID / 18) % 2;
		return new Rectangle((int)position.X, (int)position.Y, (int)(character_sprites[sprite_id, 2] * scale.X), (int)(character_sprites[sprite_id, 3] * scale.Y));
	}

	public void Collide(Rectangle rectangle)
	{
		// Intentionally left empty
	}
}
