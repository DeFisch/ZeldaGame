using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ZeldaGame.Player;

namespace ZeldaGame.HUD
{
    public class PlayerInfoHUD
    {
        private Texture2D texture;
        private Vector2 scale;
        private Vector2 window_size;
        private Rectangle playerInfoSR;
        private Rectangle playerInfoDR;


        public PlayerInfoHUD(Texture2D texture, Vector2 scale, Vector2 window_size)
        {
            this.texture = texture;
            this.scale = scale;
            this.window_size = window_size;
            playerInfoSR = new Rectangle(258, 11, 256, 56);
            playerInfoDR = new Rectangle(0, 0, 0, 0);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            playerInfoDR = new Rectangle(0, 0, (int)(playerInfoSR.Width * scale.X), (int)(playerInfoSR.Height * scale.Y));
            spriteBatch.Draw(texture, playerInfoDR, playerInfoSR, Color.White);
        }

        public void Update()
        {

        }
    }
}
