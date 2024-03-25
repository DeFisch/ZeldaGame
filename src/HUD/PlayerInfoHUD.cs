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
        private SpriteFont font;
        private Vector2 scale;
        private Rectangle playerInfoSR;
        private Rectangle playerInfoDR;
        private Rectangle rubyBlankSR;
        private Rectangle rubyBlankDR;
        private int countInt;
        private string countString;
        private ItemActionHandler itemActionHandler;
        private CollisionHandler collisionHandler;
        private bool isDisplayed;
        private HUDMapHandler mapHandler;


        public PlayerInfoHUD(Texture2D texture, Vector2 scale, MapHandler map, bool isDisplayed, SpriteFont font, CollisionHandler collisionHandler)
        {
            this.texture = texture;
            this.scale = scale;
            this.font = font;
            this.collisionHandler = collisionHandler;
            playerInfoSR = new Rectangle(258, 11, 256, 56);
            playerInfoDR = new Rectangle(0, 0, 0, 0);
            rubyBlankSR = new Rectangle(353, 11, 23, 15);
            rubyBlankDR = new Rectangle(300, 50, 85, 25);
            itemActionHandler = collisionHandler.itemActionHandler;
            this.isDisplayed = isDisplayed;
            mapHandler = new HUDMapHandler(texture, scale, map);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            playerInfoDR = new Rectangle(0, 0, (int)(playerInfoSR.Width * scale.X), (int)(playerInfoSR.Height * scale.Y));
            countInt = (itemActionHandler.inventoryCounts[0] * 5) + itemActionHandler.inventoryCounts[1];
            countString = "x" + countInt.ToString();
            spriteBatch.Draw(texture, playerInfoDR, playerInfoSR, Color.White);
            spriteBatch.Draw(texture, rubyBlankDR, rubyBlankSR, Color.Red);
            spriteBatch.DrawString(font, countString, new Vector2(300, 50), Color.White);

            if (!isDisplayed)
            {
                mapHandler.Draw(spriteBatch, -1);
            }

        }


        public void Update()
        {

        }
    }
}

