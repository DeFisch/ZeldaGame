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
        private Rectangle noHeartSR;

        private List<Rectangle> heartRowsDR;
		private List<Rectangle> heartRowsSR;

		private Rectangle playerInfoSR;
        private Rectangle playerInfoDR;
        private Rectangle currentWeaponDR;

        private int rubyCountInt;
        private string rubyCountString;
        private int keyCountInt;
        private string keyCountString;
        private int bombCountInt;
        private string bombCountString;
        private ItemActionHandler itemActionHandler;
        private CollisionHandler collisionHandler;
        private HUDMapHandler mapHandler;
        private IPlayer Link;
        private HeadUpDisplay headUpDisplay;


        public PlayerInfoHUD(Texture2D texture, Vector2 scale, MapHandler map, SpriteFont font, CollisionHandler collisionHandler, IPlayer Link, HeadUpDisplay headUpDisplay)
        {
            this.texture = texture;
            this.scale = scale;
            this.font = font;
            this.collisionHandler = collisionHandler;
            this.Link = Link;
            this.headUpDisplay = headUpDisplay;

            fullHeartSR = new Rectangle(645, 117, 8, 8);
            halfHeartSR = new Rectangle(636, 117, 8, 8);
            emptyHeartSR = new Rectangle(627, 117, 8, 8);
            noHeartSR = new Rectangle(426, 43, 8, 8);

            heartRowsDR = new List<Rectangle>();
            heartRowsSR = new List<Rectangle>();


			playerInfoSR = new Rectangle(258, 11, 256, 56);
            playerInfoDR = new Rectangle(0, 0, 0, 0);
            currentWeaponDR = new Rectangle((int)(128 * scale.X), (int)(24 * scale.Y), (int)(8 * scale.X), (int)(16 * scale.Y));

            itemActionHandler = collisionHandler.itemActionHandler;
            mapHandler = new HUDMapHandler(texture, scale, map, collisionHandler);
        }

        public void RubyCount(SpriteBatch spriteBatch, int isDisplayed)
        {
            rubyCountInt = (ItemActionHandler.inventoryCounts[0] * 5) + ItemActionHandler.inventoryCounts[1];
            rubyCountString = "x" + rubyCountInt.ToString();
            if(isDisplayed == -1)
                spriteBatch.DrawString(font, rubyCountString, new Vector2(300, 50), Color.White);
            if(isDisplayed == 1)
                spriteBatch.DrawString(font, rubyCountString, new Vector2(96 * scale.X, 192 * scale.Y), Color.White);
        }

        public void KeyCount(SpriteBatch spriteBatch, int isDisplayed)
        {
            keyCountInt = ItemActionHandler.inventoryCounts[2];
            keyCountString = "x" + keyCountInt.ToString();
            if(isDisplayed == -1)
                spriteBatch.DrawString(font, keyCountString, new Vector2(300, 100), Color.White);
            if(isDisplayed == 1)
                spriteBatch.DrawString(font, keyCountString, new Vector2(96 * scale.X, 208 * scale.Y), Color.White);
        }

        public void BombCount(SpriteBatch spriteBatch, int isDisplayed)
        {
            bombCountInt = ItemActionHandler.inventoryCounts[8];
            bombCountString = "x" + bombCountInt.ToString();
            if(isDisplayed == -1)
                spriteBatch.DrawString(font, bombCountString, new Vector2(300, 125), Color.White);
            if(isDisplayed == 1)
                spriteBatch.DrawString(font, bombCountString, new Vector2(96 * scale.X, 216 * scale.Y), Color.White);
        }

        public void Draw(SpriteBatch spriteBatch, int isDisplayed)
        {
            playerInfoDR = new Rectangle(0, 0, (int)(playerInfoSR.Width * scale.X), (int)(playerInfoSR.Height * scale.Y));
            
            if (isDisplayed == -1)
            {
                spriteBatch.Draw(texture, playerInfoDR, playerInfoSR, Color.White);
                mapHandler.Draw(spriteBatch, isDisplayed);
                spriteBatch.Draw(texture, currentWeaponDR, headUpDisplay.HUDInventory.currentWeapon(), Color.White);
            }

            rubyCountInt = (ItemActionHandler.inventoryCounts[0] * 5) + ItemActionHandler.inventoryCounts[1];
            rubyCountString = "x" + rubyCountInt.ToString();

            RubyCount(spriteBatch, isDisplayed);
            KeyCount(spriteBatch, isDisplayed);
            BombCount(spriteBatch, isDisplayed);
            DrawHealth(spriteBatch, isDisplayed);     
        }

        public void SetHealthDR(int isDisplayed)
        {
            heartRowsDR.Clear();
            heartRowsSR.Clear();
            for (int i = 0; i < 2; i++) {
                for (int j = 0; j < 8; j++) {
                    if(isDisplayed == -1)
                        heartRowsDR.Add(new Rectangle((int)((176 + (8 * j)) * scale.X), (int)((32 + (8 * i)) * scale.Y), (int)(8 * scale.X), (int)(8 * scale.Y)));
                    if(isDisplayed == 1)
                        heartRowsDR.Add(new Rectangle((int)((176 + (8 * j)) * scale.X), (int)((208 + (8 * i)) * scale.Y), (int)(8 * scale.X), (int)(8 * scale.Y)));
                    heartRowsSR.Add(new Rectangle());
                }
            }
        }

        private void DrawHealth(SpriteBatch spriteBatch, int isDisplayed)
        {
            SetHealthDR(isDisplayed);
            for (int i = 0; i < 16; i++) {
                if (i >= Link.GetMaxHealth()) { //if index is greater than / equal to maxHealth
                    heartRowsSR[i] = noHeartSR;
                }
                else if (i >= Link.GetHealth()) { //if index is greater than / equal to currHealth
                    heartRowsSR[i] = emptyHeartSR;
                }
                else if (i + 0.5f == Link.GetHealth()) { // if index equals currHealth
                    heartRowsSR[i] = halfHeartSR;
                }
                else {
                    heartRowsSR[i] = fullHeartSR;
                }
                if(isDisplayed == -1)
                    spriteBatch.Draw(texture, heartRowsDR[i], heartRowsSR[i], Color.White);
                if(isDisplayed == 1)
                    spriteBatch.Draw(texture, heartRowsDR[i], heartRowsSR[i], Color.White);
            }
        }
    }
}

