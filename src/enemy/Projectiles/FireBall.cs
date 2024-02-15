using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ZeldaGame.Player;

namespace Enemy.Projectiles
{
    public class FireBall : IEnemyProjectile
    {
        private Rectangle[] fireball_sprites = new Rectangle[] { new Rectangle(238, 154, 8, 16), new Rectangle(247, 154, 8, 16), new Rectangle(256, 154, 8, 16), new Rectangle(265, 154, 8, 16) };
        private Vector2 direction;
        private Vector2 location;
        private Texture2D texture;
        private int scale = 2;
        private int currentFrame = 0;
        private int frameID = 0;
        private Rectangle sourceRectangle;
        
        public FireBall(Texture2D texture, Vector2 location, Vector2 direction)
        {
            this.direction = direction;
            this.location = location;
            this.texture = texture;
        }

        public bool Completed()
        {
            if ( frameID > 1000)
            {
                return true;
            }
            return false;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            sourceRectangle = fireball_sprites[currentFrame];
            Rectangle destinationRectangle = new Rectangle((int)location.X, (int)location.Y, sourceRectangle.Width * scale, sourceRectangle.Height * scale);
            spriteBatch.Draw(texture, destinationRectangle, sourceRectangle, Color.White);
        }

        public Vector2 GetLocation()
        {
            return location;
        }

        public void Update()
        {
            frameID++;
            if (frameID % 8 == 0)
            {
                currentFrame = (currentFrame + 1) % 4;
            }
            location += direction;
        }
    }
}
