using System;
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
    private int sprite_id = 0;
	public WizzRobe(Texture2D texture, Vector2 window_size, string color) {
		this.texture = texture;
		position = new Vector2(new Random().Next(0, (int)window_size.X - character_sprites[0,2] * scale), new Random().Next(0, (int)window_size.Y - character_sprites[0,3] * scale));
		state = State.Walking;
		direction = Direction.Down;
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

        // animate sprite
        if (frameID % 20 == 0) {
            sprite_id = sprite_id % 2 == 0 ? sprite_id+1 : sprite_id-1;
        }

        if ((direction == Direction.Left || direction == Direction.Right) && sprite_id/2 == 1)
            sprite_id = 0; // switch to horizontal sprite
		if ((direction == Direction.Up || direction == Direction.Down) && sprite_id/2 == 0)
			sprite_id = 2; // switch to vertical sprite
            
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
}
