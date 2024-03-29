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
        private Rectangle allBlankSR;
        private Rectangle rubyBlankDR;
        private Rectangle keyBlankDR;
        private Rectangle bombBlankDR;
        private int rubyCountInt;
        private string rubyCountString;
        private int keyCountInt;
        private string keyCountString;
        private int bombCountInt;
        private string bombCountString;
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
            allBlankSR = new Rectangle(353, 11, 23, 15);
            rubyBlankDR = new Rectangle(300, 50, 85, 25);
            keyBlankDR = new Rectangle(300, 100, 85, 25);
            bombBlankDR = new Rectangle(300, 125, 85, 25);

            itemActionHandler = collisionHandler.itemActionHandler;
            this.isDisplayed = isDisplayed;
            mapHandler = new HUDMapHandler(texture, scale, map);
        }

        public void RubyCount(SpriteBatch spriteBatch)
        {
            rubyCountInt = (ItemActionHandler.inventoryCounts[0] * 5) + ItemActionHandler.inventoryCounts[1];
            rubyCountString = "x" + rubyCountInt.ToString();
            spriteBatch.Draw(texture, rubyBlankDR, allBlankSR, Color.White);
            spriteBatch.DrawString(font, rubyCountString, new Vector2(300, 50), Color.White);
        }

        public void KeyCount(SpriteBatch spriteBatch)
        {
            keyCountInt = ItemActionHandler.inventoryCounts[2];
            keyCountString = "x" + keyCountInt.ToString();
            spriteBatch.Draw(texture, keyBlankDR, allBlankSR, Color.White);
            spriteBatch.DrawString(font, keyCountString, new Vector2(300, 100), Color.White);
        }

        public void BombCount(SpriteBatch spriteBatch)
        {
            bombCountInt = ItemActionHandler.inventoryCounts[9];
            bombCountString = "x" + bombCountInt.ToString();
            spriteBatch.Draw(texture, bombBlankDR, allBlankSR, Color.White);
            spriteBatch.DrawString(font, bombCountString, new Vector2(300, 125), Color.White);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            playerInfoDR = new Rectangle(0, 0, (int)(playerInfoSR.Width * scale.X), (int)(playerInfoSR.Height * scale.Y));
            spriteBatch.Draw(texture, playerInfoDR, playerInfoSR, Color.White);
            spriteBatch.Draw(texture, keyBlankDR, allBlankSR, Color.White);
            spriteBatch.Draw(texture, bombBlankDR, allBlankSR, Color.White);
            RubyCount(spriteBatch);
            KeyCount(spriteBatch);
            BombCount(spriteBatch);
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

