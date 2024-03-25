using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ZeldaGame.Items;
using ZeldaGame.Player;
using ZeldaGame.Map;

namespace ZeldaGame.HUD
{
    public class PlayerInfoHUD
    {
        private Texture2D texture;
        private Vector2 scale;
        private Vector2 window_size;
        private Rectangle playerInfoSR;
        private Rectangle playerInfoDR;
        private bool isDisplayed;
        private HUDMapHandler mapHandler;

        public PlayerInfoHUD(Texture2D texture, Vector2 scale, MapHandler map, bool isDisplayed)
        {
            this.texture = texture;
            this.scale = scale;
            playerInfoSR = new Rectangle(258, 11, 256, 56);
            playerInfoDR = new Rectangle(0, 0, 0, 0);
            this.isDisplayed = isDisplayed;
            mapHandler = new HUDMapHandler(texture, scale, map);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            playerInfoDR = new Rectangle(0, 0, (int)(playerInfoSR.Width * scale.X), (int)(playerInfoSR.Height * scale.Y));
            spriteBatch.Draw(texture, playerInfoDR, playerInfoSR, Color.White);
            if (!isDisplayed)
                mapHandler.Draw(spriteBatch, -1);

        }

        public void Update()
        {

        }
    }
}
