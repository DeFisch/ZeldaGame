using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ZeldaGame.Enemy;

public class KeeseGoriya : IEnemy
{
    enum State { Idle, Walking, Attacking, Dead };
    private int frameID = 0;
    private Texture2D texture;
    private Vector2 position;
    private State state;
    private static int[,] character_sprites = new int[,] { { 183, 11, 16, 16 }, {200, 11, 16, 16} }; // x, y, width, height
    private int scale = 2;
    private double speedX = 0;
    private double speedY = 0;
    private double general_speed = 4;
    public KeeseGoriya(Texture2D texture, Vector2 window_size, string color)
    {
        this.texture = texture;
        this.position = new Vector2(new Random().Next(0, (int)window_size.X - character_sprites[0,2] * scale), new Random().Next(0, (int)window_size.Y - character_sprites[0,3] * scale));
        state = State.Walking;
        this.speedX = new Random().NextDouble() * general_speed - general_speed / 2;
        this.speedY = new Random().NextDouble() * general_speed - general_speed / 2;
        if (color == "blue")
        {
            character_sprites[0, 1] = 28;
            character_sprites[1, 1] = 28;
        }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        int sprite_id = (frameID / 18) % 2;
        Rectangle sourceRectangle = new Rectangle(character_sprites[sprite_id,0], character_sprites[sprite_id,1], character_sprites[sprite_id,2], character_sprites[sprite_id,3]);
        Rectangle destinationRectangle = new Rectangle((int)position.X, (int)position.Y, character_sprites[sprite_id,2] * scale, character_sprites[sprite_id,3] * scale);
        spriteBatch.Draw(texture, destinationRectangle, sourceRectangle, Color.White);
    }

    public void Update()
    {

        if (state == State.Walking)
            Walk();
        if (state == State.Idle)
            Idle();
        frameID++;
    }

    private void Idle()
    {
        // Empty
    }

    public void Walk(){
        int sprite_id = (frameID / 18) % 2;
        if (frameID % 120 == 0)
        {
            speedX = new Random().NextDouble() * general_speed - general_speed / 2;
            speedY = new Random().NextDouble() * general_speed - general_speed / 2;
        }
        if (position.X > 0 && position.X < 800 - character_sprites[sprite_id,2] * scale && position.Y > 0 && position.Y < 600 - character_sprites[sprite_id,3] * scale){
            position.X += (float)speedX;
            position.Y += (float)speedY;
        }
        else{
            speedX = -speedX;
            speedY = -speedY;
            position.X += (float)speedX;
            position.Y += (float)speedY;
        }
    }
}
