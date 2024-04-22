using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ZeldaGame.Player;
using static System.Net.Mime.MediaTypeNames;

namespace ZeldaGame.Enemy.Projectiles
{
    public class FireBall : IEnemyProjectile
    {
        private Rectangle[] fireball_sprites = new Rectangle[] { new Rectangle(238, 154, 8, 16), new Rectangle(247, 154, 8, 16), new Rectangle(256, 154, 8, 16), new Rectangle(265, 154, 8, 16) };
        private Vector2 direction;
        private Vector2 location;
        private Texture2D texture;
        private int currentFrame = 0;
        private int frameID = 0;
        private Rectangle sourceRectangle;
        private float damage = 0.5f;
        private bool collided;
        
        public FireBall(Texture2D texture, Vector2 location, Vector2 direction)
        {
            this.direction = direction;
            this.location = location;
            sourceRectangle = fireball_sprites[currentFrame];
            this.location -= sourceRectangle.Size.ToVector2() * Globals.scale / 2;
            this.texture = texture;
            collided = false;
        }

        public bool Completed()
        {
            if ( frameID > 1000 || collided)
            {
                return true;
            }
            return false;
        }

        public void Collided()
        {
            collided = true;
        }

        public float DoDamage()
        {
            return damage;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            sourceRectangle = fireball_sprites[currentFrame];
            Rectangle destinationRectangle = new Rectangle((int)location.X, (int)location.Y, (int)(sourceRectangle.Width * Globals.scale.X), (int)(sourceRectangle.Height * Globals.scale.Y));
            spriteBatch.Draw(texture, destinationRectangle, sourceRectangle, Color.White);
        }

        public Rectangle GetRectangle()
        {
            return new Rectangle((int)location.X, (int)location.Y, (int)(fireball_sprites[currentFrame].Width * Globals.scale.X), (int)(fireball_sprites[currentFrame].Height * Globals.scale.Y));
        }

		public string GetDirection() {
			string dirStr = "None";
			Vector2 normDirection = direction;
			normDirection.Normalize();
			switch (normDirection) {
				case Vector2(1, 0):
					dirStr = "Left";
					break;
				case Vector2(-1, 0):
					dirStr = "Right";
					break;
				case Vector2(0, 1):
					dirStr = "Up";
					break;
				case Vector2(0, -1):
					dirStr = "Down";
					break;
			}

			return dirStr;
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
