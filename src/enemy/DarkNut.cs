using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ZeldaGame.Items;
using static ZeldaGame.Globals;

namespace ZeldaGame.Enemy;
public class DarkNut : IEnemy {
	enum State { Idle, Walking, Attacking, Dead };
	private int frameID = 0;
	private Texture2D texture;
	private Vector2 position;
	private State state;
	private Direction direction;
	private Vector2 scale;
    private Vector2 knockback;
    private int knockbackTimer;
    private readonly int knockbackScale = 8;
    private int[,] character_sprites = new int[,] { {1,90,16,16}, {18,90,16,16}, {35,90,16,16}, {52,90,16,16}, {69,90,16,16}}; // x, y, width, height
	private int speed = 2;
    private int sprite_id = 0;
	private int health = 3;
	private int dead_timer = 0;
	private int iFrame = -100;
	private float damage = 0.5f;

	public DarkNut(Texture2D texture, Vector2 position, string color, Vector2 scale) {
		this.texture = texture;
		this.position = position;
		state = State.Walking;
		direction = Direction.Down;
        if (color == "blue") {
            character_sprites[0, 1] = 107;
            character_sprites[1, 1] = 107;
            character_sprites[2, 1] = 107;
            character_sprites[3, 1] = 107;
            character_sprites[4, 1] = 107;
        }
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
		Rectangle sourceRectangle = new Rectangle(character_sprites[sprite_id, 0], character_sprites[sprite_id, 1], character_sprites[sprite_id, 2], character_sprites[sprite_id, 3]);
		Rectangle destinationRectangle = new Rectangle((int)position.X, (int)position.Y, (int)(character_sprites[sprite_id, 2] * scale.X), (int)(character_sprites[sprite_id, 3] * scale.Y));
		SpriteEffects sprite_effect = SpriteEffects.None;
		if (direction == Direction.Left)
			sprite_effect = SpriteEffects.FlipHorizontally;
        if (direction == Direction.Up && (frameID / 20) % 2 == 0)
            sprite_effect = SpriteEffects.FlipHorizontally;
		spriteBatch.Draw(texture, destinationRectangle, sourceRectangle, Color.White, rotation: 0, new Vector2(0, 0), effects: sprite_effect, 1);
	}

	public void Update() {
		if (ItemActionHandler.inventoryCounts[3] != 0)
		{
			state = State.Idle;
		} else
		 	state = State.Walking;
		if (health <= 0){
			state = State.Dead;
		}
        // animate sprite
        if (frameID % 20 == 0 && direction != Direction.Up) {
            sprite_id = sprite_id % 3 == 0 ? sprite_id+1 : sprite_id-1;
        }

        if ((direction == Direction.Left || direction == Direction.Right) && sprite_id < 3)
            sprite_id = 3; // switch to horizontal sprite
		if ((direction == Direction.Down) && sprite_id  > 1)
			sprite_id = 0; // switch to down sprite
        if (direction == Direction.Up)
            sprite_id = 2; // switch to up sprite

        if (ItemActionHandler.inventoryCounts[3] != 0)
        {
            state = State.Idle;
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

	private void Idle() {
		if (frameID / 60 == 0)
			direction = Direction.Left;
		else
			direction = Direction.Right;
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

	private void Dead() {
		float vel = -7.81f;
		vel += 0.605f * dead_timer;
		position.Y += vel;
		dead_timer++;
	}

	public bool IsFinished() {
		return dead_timer > 60;
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
		return new Rectangle((int)position.X, (int)position.Y, (int)(character_sprites[sprite_id, 2] * scale.X), (int)(character_sprites[sprite_id, 3] * scale.Y));
	}

	public void OnCollision(Rectangle rectangle)
	{
        Rectangle collisionOverlap = Rectangle.Intersect(GetHitBox(), rectangle);

        if (collisionOverlap.Width > collisionOverlap.Height)
        {
            if (collisionOverlap.Center.Y < GetHitBox().Center.Y)
                position.Y += collisionOverlap.Height;
            else
                position.Y -= collisionOverlap.Height;
        }
        else
        {
            if (collisionOverlap.Center.X < GetHitBox().Center.X)
                position.X += collisionOverlap.Width;
            else
                position.X -= collisionOverlap.Width;
        }

        Direction old_direction = direction;
        while (direction == old_direction)
            direction = (Direction)(new Random().Next(0, 4));
    }
}
