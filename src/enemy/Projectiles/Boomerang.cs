
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Enemy.Projectiles
{
    public class Boomerang : IEnemyProjectile
    {
        private Rectangle[] Boomerang_sprites = new Rectangle[] { new Rectangle (290,11,8,16), new Rectangle (299,11,8,16), new Rectangle (308,11,8,16) };
        private Vector2 direction;
        private Vector2 location;
        private Texture2D texture;
        private int scale = 2;
        private int currentFrame = 0;
        private int frameID = 0;
        private int duration = 180;
        private SpriteEffects sprite_effect = SpriteEffects.None;
        private Rectangle sourceRectangle;
        
        public Boomerang(Texture2D texture, Vector2 location, Vector2 direction)
        {
            this.direction = direction;
            this.location = location;
            this.texture = texture;
        }

        public bool Completed()
        {
            if ( frameID > duration)
            {
                return true;
            }
            return false;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            sourceRectangle = Boomerang_sprites[currentFrame];
            Rectangle destinationRectangle = new Rectangle((int)location.X, (int)location.Y, sourceRectangle.Width * scale, sourceRectangle.Height * scale);
            spriteBatch.Draw(texture, destinationRectangle, sourceRectangle, Color.White, rotation: 0, new Vector2(0, 0), effects: sprite_effect, 1);
        }

        public Vector2 GetLocation()
        {
            return location;
        }

        public void Update()
        {
            frameID++;
            if (frameID / 2 % 8 == 0){
                currentFrame = 1;
                sprite_effect = SpriteEffects.None;
            }else if (frameID / 2 % 8 == 1){
                currentFrame = 2;
            }else if (frameID / 2 % 8 == 2){
                currentFrame = 1;
                sprite_effect = SpriteEffects.FlipHorizontally;
            }else if (frameID / 2 % 8 == 3){
                currentFrame = 0;
                sprite_effect = SpriteEffects.FlipHorizontally;
            }else if (frameID / 2 % 8 == 4){
                currentFrame = 1;
                sprite_effect = SpriteEffects.FlipHorizontally | SpriteEffects.FlipVertically;
            }else if (frameID / 2 % 8 == 5){
                currentFrame = 2;
                sprite_effect = SpriteEffects.FlipVertically;
            }else if (frameID / 2 % 8 == 6){
                currentFrame = 1;
                sprite_effect = SpriteEffects.FlipVertically;
            }else
                currentFrame = 0;
            
            double speed_scale = -4*(1/(1+Math.Exp(-(10.0/duration)*(frameID-0.6*duration)))) + 2; // logistic function to smooth out animation
            location += direction * (float)speed_scale;
        }
    }
}
