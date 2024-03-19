﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ZeldaGame.Player;

namespace ZeldaGame.HUD
{
    public class HeadUpDisplay
    {
        private Texture2D texture;
        private Vector2 scale;
        private Vector2 window_size;
        private Rectangle inventorySR;
        private Rectangle mapSR;
        private Rectangle playerInfoSR;
        private Rectangle inventoryDR;
        private Rectangle mapDR;
        private Rectangle playerInfoDR;
        private int isDisplayed = -1;

        public HeadUpDisplay (Texture2D texture, Vector2 scale, Vector2 window_size )
        {
            this.texture = texture;
            this.scale = scale;
            this.window_size = window_size;
            inventorySR = new Rectangle(1, 11, 256, 88);
            mapSR = new Rectangle(258, 112, 256, 88);
            playerInfoSR = new Rectangle(258, 11, 256, 56);
            inventoryDR = new Rectangle(0, 0, 0, 0);
            mapDR = new Rectangle(0, 0, 0, 0);
            playerInfoDR = new Rectangle(0, 0, 0, 0);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (isDisplayed == 1) 
            {
                inventoryDR = new Rectangle(0, 0, (int)(inventorySR.Width * scale.X), (int)(inventorySR.Height * scale.Y));
                spriteBatch.Draw(texture, inventoryDR, inventorySR, Color.White);

                mapDR = new Rectangle(0, (int)(inventorySR.Height * scale.Y), (int)(mapSR.Width * scale.X), (int)(mapSR.Height * scale.Y));
                spriteBatch.Draw(texture, mapDR, mapSR, Color.White);

                playerInfoDR = new Rectangle(0, (int)(inventorySR.Height * scale.Y) + (int)(mapSR.Height * scale.Y), (int)(playerInfoSR.Width * scale.X), (int)(playerInfoSR.Height * scale.Y));
                spriteBatch.Draw(texture, playerInfoDR, playerInfoSR, Color.White);
            }
        }

        public void Update ()
        {

        }

        public void Display()
        {
            isDisplayed *= -1;
        }
    }
}
