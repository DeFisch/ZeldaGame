using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeldaGame.Items
{
    public class YellowRuby : ISprite
    {
        private Texture2D texture;
        private Vector2 pos;
        public YellowRuby(Texture2D texture, Vector2 pos)
        {
            this.texture = texture;
            this.pos = pos;

        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {
            Rectangle sourceRectangle = new Rectangle(72, 0, 8, 16);
            Rectangle destinationRectangle = new Rectangle((int)pos.X, (int)pos.Y, 16, 42);
            spriteBatch.Draw(texture, destinationRectangle, sourceRectangle, Color.White);
        }
        public void Update()
        {

        }
    }
}
