using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ZeldaGame.Items;
using ZeldaGame.Player;
using ZeldaGame.Map;
using System.Diagnostics;

namespace ZeldaGame.HUD
{
    public class PlayerInfoHUD
    {
        private Texture2D texture;
        private SpriteFont font;
        private Vector2 scale;

        private Rectangle fullHeartSR;
        private Rectangle halfHeartSR;
        private Rectangle emptyHeartSR;
        private Rectangle firstHeartDR;
        private Rectangle secondHeartDR;
        private Rectangle thirdHeartDR;
        private Rectangle firstHeartSR;
        private Rectangle secondHeartSR;
        private Rectangle thirdHeartSR;

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
        private Game1 myGame;


        public PlayerInfoHUD(Texture2D texture, Vector2 scale, MapHandler map, bool isDisplayed, SpriteFont font, CollisionHandler collisionHandler, Game1 myGame)
        {
            this.texture = texture;
            this.scale = scale;
            this.font = font;
            this.collisionHandler = collisionHandler;
            this.myGame = myGame;

            fullHeartSR = new Rectangle(645, 117, 8, 8);
            halfHeartSR = new Rectangle(636, 117, 8, 8);
            emptyHeartSR = new Rectangle(627, 117, 8, 8);
            firstHeartDR = new Rectangle(550, 95, 70, 60);
            secondHeartDR = new Rectangle(620, 95, 70, 60);
            thirdHeartDR = new Rectangle(690, 95, 70, 60);

            playerInfoSR = new Rectangle(258, 11, 256, 56);
            playerInfoDR = new Rectangle(0, 0, 0, 0);
            allBlankSR = new Rectangle(353, 11, 23, 15);
            rubyBlankDR = new Rectangle(300, 50, 85, 25);
            keyBlankDR = new Rectangle(300, 100, 85, 25);
            bombBlankDR = new Rectangle(300, 125, 85, 25);

            itemActionHandler = collisionHandler.itemActionHandler;
            this.isDisplayed = isDisplayed;
            mapHandler = new HUDMapHandler(texture, scale, map, collisionHandler);
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

            rubyCountInt = (itemActionHandler.inventoryCounts[0] * 5) + itemActionHandler.inventoryCounts[1];
            rubyCountString = "x" + rubyCountInt.ToString();


            spriteBatch.Draw(texture, playerInfoDR, playerInfoSR, Color.White);
            spriteBatch.Draw(texture, keyBlankDR, allBlankSR, Color.White);
            spriteBatch.Draw(texture, bombBlankDR, allBlankSR, Color.White);
            RubyCount(spriteBatch);
            KeyCount(spriteBatch);
            BombCount(spriteBatch);

            DrawHealth(spriteBatch);

            if (!isDisplayed)
            {
                mapHandler.Draw(spriteBatch, -1);
            }

        }


        public void Update()
        {

        }

        private void DrawHealth(SpriteBatch spriteBatch)
        {
            switch (myGame.Link.GetHealth())
            {
                case 3: firstHeartSR = fullHeartSR; secondHeartSR = fullHeartSR; thirdHeartSR = fullHeartSR; break;
                case 2.5f: firstHeartSR = fullHeartSR; secondHeartSR = fullHeartSR; thirdHeartSR = halfHeartSR; break;
                case 2: firstHeartSR = fullHeartSR; secondHeartSR = fullHeartSR; thirdHeartSR = emptyHeartSR; break;
                case 1.5f: firstHeartSR = fullHeartSR; secondHeartSR = halfHeartSR; thirdHeartSR = emptyHeartSR; break;
                case 1: firstHeartSR = fullHeartSR; secondHeartSR = emptyHeartSR; thirdHeartSR = emptyHeartSR; break;
                case 0.5f: firstHeartSR = halfHeartSR; secondHeartSR = emptyHeartSR; thirdHeartSR = emptyHeartSR; break;
                case 0: firstHeartSR = emptyHeartSR; secondHeartSR = emptyHeartSR; thirdHeartSR = emptyHeartSR; break;
            }
            spriteBatch.Draw(texture, firstHeartDR, firstHeartSR, Color.White);
            spriteBatch.Draw(texture, secondHeartDR, secondHeartSR, Color.White);
            spriteBatch.Draw(texture, thirdHeartDR, thirdHeartSR, Color.White);
        }
    }
}

