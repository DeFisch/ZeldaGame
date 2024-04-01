using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ZeldaGame.Player;
using ZeldaGame.Map;

namespace ZeldaGame.HUD
{
    public class HeadUpDisplay
    {
        private HUDMapHandler HUDMapHandler;
        private CollisionHandler collision;
        public HUDInventory HUDInventory;
        private PlayerInfoHUD playerInfoHUD;
        private Texture2D texture;
        private Vector2 scale;
        private Rectangle inventorySR;
        private Rectangle mapSR;
        private Rectangle playerInfoSR;
        private Rectangle inventoryDR;
        private Rectangle mapDR;
        private Rectangle playerInfoDR;
        private int isDisplayed = -1;

        public HeadUpDisplay (Texture2D texture, Vector2 scale, MapHandler map, CollisionHandler collisionHandler, IPlayer Link, SpriteFont font)
        {
            this.texture = texture;
            this.scale = scale;
            this.collision = collisionHandler;
            playerInfoHUD = new PlayerInfoHUD(texture, scale, map, font, collisionHandler, Link, this);
            HUDInventory = new HUDInventory(texture, scale);
            HUDMapHandler = new HUDMapHandler(texture, scale, map, collisionHandler);
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
                DrawMapItem(spriteBatch);
                DrawCompassItem(spriteBatch);
                playerInfoHUD.Draw(spriteBatch, isDisplayed);
            }
            HUDMapHandler.Draw(spriteBatch, isDisplayed);
            HUDInventory.Draw(spriteBatch, isDisplayed);

        }

        public void Update ()
        {
            HUDMapHandler.Update();
        }

        public void Display()
        {
            isDisplayed *= -1;
        }

        public bool isVisible()
        {
            return isDisplayed == 1;
        }

        public void DrawMapItem(SpriteBatch spriteBatch)
        {
            if(collision.itemActionHandler.isMapObtained())
            {
                Rectangle mapItemSR = new Rectangle(601, 156, 8, 16);
                Rectangle mapItemDR = new Rectangle((int)(48 * scale.X), (int)(112 * scale.Y), (int)(mapItemSR.Width * scale.X), (int)(mapItemSR.Height * scale.Y));
                spriteBatch.Draw(texture, mapItemDR, mapItemSR, Color.White);
            }
        }

        public void DrawCompassItem(SpriteBatch spriteBatch)
        {
            if(collision.itemActionHandler.isCompassObtained())
            {
                Rectangle compassItemSR = new Rectangle(612, 156, 15, 15);
                Rectangle compassItemDR = new Rectangle((int)(44 * scale.X), (int)(152 * scale.Y), (int)(compassItemSR.Width * scale.X), (int)(compassItemSR.Height * scale.Y));
                spriteBatch.Draw(texture, compassItemDR, compassItemSR, Color.White);
            }
        }

        public void Reset()
        {
            HUDMapHandler.Reset();
            isDisplayed = -1;
        }
    }
}
