﻿using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;


namespace Sprint0.Block
{
    public class Stair: IBlock
    {
        private Texture2D texture;
        private Vector2 position;
        
        public Stair(Texture2D texture, Vector2 position)
        {
            this.texture = texture; 
            this.position = position;
        }

        public void Update() 
        { 
        
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            Rectangle sourceRectangle = new Rectangle(386, 81, 16, 16);
            Rectangle destinationRectangle = new Rectangle((int)position.X,(int)position.Y, 32, 32);
            spriteBatch.Draw(texture, destinationRectangle, sourceRectangle, Color.White);

        }


    }
}
