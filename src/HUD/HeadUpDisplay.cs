using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ZeldaGame.Player;
using ZeldaGame.Map;
using static ZeldaGame.Globals;

namespace ZeldaGame.HUD
{
    public class HeadUpDisplay
    {
        public HUDMapHandler HUDMapHandler;
        private CollisionHandler collision;
        public HUDInventory HUDInventory;
        private PlayerInfoHUD playerInfoHUD;
        private Texture2D texture;
        private Rectangle inventorySR;
        private Rectangle mapSR;
        private Rectangle playerInfoSR;
        private Rectangle inventoryDR;
        private Rectangle mapDR;
        private Rectangle playerInfoDR;
        private bool isDisplayed = false;

        public HeadUpDisplay (Texture2D texture, MapHandler map, CollisionHandler collisionHandler, IPlayer Link, SpriteFont font)
        {
            this.texture = texture;
            this.collision = collisionHandler;
            playerInfoHUD = new PlayerInfoHUD(texture, map, font, collisionHandler, Link, this);
            HUDInventory = new HUDInventory(texture, Link);
            HUDMapHandler = new HUDMapHandler(texture, map, collisionHandler);
            inventorySR = new Rectangle(1, 11, 256, 88);
            mapSR = new Rectangle(258, 112, 256, 88);
            playerInfoSR = new Rectangle(258, 11, 256, 56);
            inventoryDR = new Rectangle(0, 0, (int)(inventorySR.Width * scale.X), (int)(inventorySR.Height * scale.Y));;
            mapDR = new Rectangle(0, (int)(inventorySR.Height * scale.Y), (int)(mapSR.Width * scale.X), (int)(mapSR.Height * scale.Y));
            playerInfoDR = new Rectangle(0, (int)(inventorySR.Height * scale.Y) + (int)(mapSR.Height * scale.Y), (int)(playerInfoSR.Width * scale.X), (int)(playerInfoSR.Height * scale.Y));
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (isDisplayed) 
            {
                spriteBatch.Draw(texture, inventoryDR, inventorySR, Color.White);
                spriteBatch.Draw(texture, mapDR, mapSR, Color.White);
                spriteBatch.Draw(texture, playerInfoDR, playerInfoSR, Color.White);
                DrawMapItem(spriteBatch);
                DrawCompassItem(spriteBatch);
                playerInfoHUD.Draw(spriteBatch, isDisplayed);
            }
            HUDMapHandler.Draw(spriteBatch, isDisplayed);
            HUDInventory.Draw(spriteBatch, isDisplayed);

        }

        public void Update () => HUDMapHandler.Update();

        public void Display() => isDisplayed = !isDisplayed;

        public bool isVisible() => isDisplayed;

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
            HUDInventory.Reset();
            isDisplayed = false;
        }
    }
}
