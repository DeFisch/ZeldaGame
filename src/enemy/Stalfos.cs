using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ZeldaGame.Enemy;

public class Stalfos : IEnemy {
	enum State { Idle, Walking, Attacking, Dead };
	enum Direction { Up, Down, Left, Right };
	private int frameID = 0;
	private Texture2D texture;
	private Vector2 position;
	private State state;
	private Direction direction;
	private static int[] character_sprites = new int[] { 1, 59, 16, 16 }; // x, y, width, height
	private int scale = 2;
	private int speed = 2;
	public Stalfos(Texture2D texture, Vector2 window_size) {
		this.texture = texture;
		this.position = new Vector2(new Random().Next(0, (int)window_size.X - character_sprites[2] * scale), new Random().Next(0, (int)window_size.Y - character_sprites[3] * scale));
		state = State.Walking;
		direction = Direction.Down;
	}

	public void Draw(SpriteBatch spriteBatch) {
		Rectangle sourceRectangle = new Rectangle(character_sprites[0], character_sprites[1], character_sprites[2], character_sprites[3]);
		Rectangle destinationRectangle = new Rectangle((int)position.X, (int)position.Y, character_sprites[2] * scale, character_sprites[3] * scale);
		SpriteEffects sprite_effect = SpriteEffects.None;
		if (direction == Direction.Left)
			sprite_effect = SpriteEffects.FlipHorizontally;
		if ((direction == Direction.Up || direction == Direction.Down) && (frameID / 30) % 2 == 0)
			sprite_effect = SpriteEffects.FlipHorizontally;
		spriteBatch.Draw(texture, destinationRectangle, sourceRectangle, Color.White, rotation: 0, new Vector2(0, 0), effects: sprite_effect, 1);
	}

	public void Update() {

		if (state == State.Walking)
			Walk();
		if (state == State.Idle)
			Idle();
		frameID++;
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
					direction = Direction.Right;
				break;
			case Direction.Down:
				if (position.Y < 600 - character_sprites[3] * scale)
					position.Y += speed;
				else
					direction = Direction.Left;
				break;
			case Direction.Left:
				if (position.X > 0)
					position.X -= speed;
				else
					direction = Direction.Up;
				break;
			case Direction.Right:
				if (position.X < 800 - character_sprites[2] * scale)
					position.X += speed;
				else
					direction = Direction.Down;
				break;
		}
	}
}
