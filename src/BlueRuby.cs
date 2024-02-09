using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeldaGame
{
    public class BlueRuby : ISprite
    {
        private Texture2D texture;
        private Vector2 pos;
        public BlueRuby(Texture2D texture, Vector2 pos) { 
            this.texture = texture;
            this.pos = pos; 
        
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {
            Rectangle sourceRectangle = new Rectangle(72, 16, 5, 25);
            Rectangle destinationRectangle = new Rectangle((int)pos.X, (int)pos.Y, 10, 50);
        }
        public void Update()
        {

        }
    }
}
