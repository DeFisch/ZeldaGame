using System.Collections.Generic;
using Enemy.Projectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ZeldaGame.Items;
using System.Diagnostics;
using static ZeldaGame.Globals;

namespace ZeldaGame.HUD
{
    public class HUDInventory
    {
        private List<Rectangle> weaponSRList;
        private List<Rectangle> weaponDRList;
        private int cycleIndex;
        private Texture2D texture;
        private Vector2 scale;
        private Rectangle selectionSR;
        private Rectangle selectionDR;
        private Rectangle targetWeaponDRTop;
        private Rectangle targetWeaponDRBottom;
        private List<PlayerProjectiles> weapons = new() 
        {   PlayerProjectiles.WoodenArrow, PlayerProjectiles.BlueArrow, PlayerProjectiles.Boomerang, 
            PlayerProjectiles.BlueBoomerang, PlayerProjectiles.Bomb, PlayerProjectiles.Fireball };


        public HUDInventory(Texture2D texture, Vector2 scale)
        {
            this.texture = texture;
            this.scale = scale;
            cycleIndex = 0;
            weaponSRList = new List<Rectangle>();
            weaponDRList = new List<Rectangle>();
            SetWeapen();
        }

        public void Draw(SpriteBatch spriteBatch, bool isDisplayed)
        {
            if (isDisplayed)
            {
                spriteBatch.Draw(texture, selectionDR, selectionSR, Color.White);
                spriteBatch.Draw(texture, targetWeaponDRTop, weaponSRList[cycleIndex], Color.White);
                spriteBatch.Draw(texture, targetWeaponDRBottom, weaponSRList[cycleIndex], Color.White);
                for (int i = 0; i < weaponSRList.Count; i++)
                    spriteBatch.Draw(texture, weaponDRList[i], weaponSRList[i], Color.White);
            }
        }

        public Rectangle CurrentWeaponSprite() => weaponSRList[cycleIndex];

        public PlayerProjectiles CurrentWeapon() => weapons[cycleIndex];

        public void CycleList(int cycleDirection)
        {
            int listLength = weaponSRList.Count;
            if (cycleDirection == 1)
                cycleIndex = (cycleIndex + 1) % listLength;
            else if (cycleDirection == 0)
                cycleIndex = (cycleIndex - 1 + listLength) % listLength;
            SetSelection(cycleIndex);
        }

        public void SetWeapen()
        {
            Rectangle ArrowSR = new Rectangle(615, 137, 8, 16);
            Rectangle BlueArrowSR = new Rectangle(624, 137, 8, 16);
            Rectangle BoomerangSR = new Rectangle(584, 137, 8, 16);
            Rectangle BlueBoomerangSR = new Rectangle(593, 137, 8, 16);
            Rectangle BombSR = new Rectangle(604, 137, 8, 16);
            Rectangle FireballSR = new Rectangle(653, 137, 8, 16);

            Rectangle ArrowDR = new Rectangle((int)(132 * scale.X), (int)(48 * scale.Y), (int)(8 * scale.X), (int)(16 * scale.Y));
            Rectangle BlueArrowDR = new Rectangle((int)(156 * scale.X), (int)(48 * scale.Y), (int)(8 * scale.X), (int)(16 * scale.Y));
            Rectangle BoomerangDR = new Rectangle((int)(180 * scale.X), (int)(48 * scale.Y), (int)(8 * scale.X), (int)(16 * scale.Y));
            Rectangle BlueBoomerangDR = new Rectangle((int)(204 * scale.X), (int)(48 * scale.Y), (int)(8 * scale.X), (int)(16 * scale.Y));
            Rectangle BombDR = new Rectangle((int)(132 * scale.X), (int)(64 * scale.Y), (int)(8 * scale.X), (int)(16 * scale.Y));
            Rectangle FireballDR = new Rectangle((int)(156 * scale.X), (int)(64 * scale.Y), (int)(8 * scale.X), (int)(16 * scale.Y));

            targetWeaponDRTop = new Rectangle((int)(68 * scale.X), (int)(48 * scale.Y), (int)(8 * scale.X), (int)(16 * scale.Y));
            targetWeaponDRBottom = new Rectangle((int)(128 * scale.X), (int)(200 * scale.Y), (int)(8 * scale.X), (int)(16 * scale.Y));
            selectionDR = new Rectangle((int)(128 * scale.X), (int)(48 * scale.Y), (int)(16 * scale.X), (int)(16 * scale.Y));
            selectionSR = new Rectangle(519, 137, 16, 16);

            weaponSRList.Add(ArrowSR);
            weaponSRList.Add(BlueArrowSR);
            weaponSRList.Add(BoomerangSR);
            weaponSRList.Add(BlueBoomerangSR);
            weaponSRList.Add(BombSR);
            weaponSRList.Add(FireballSR);

            weaponDRList.Add(ArrowDR);
            weaponDRList.Add(BlueArrowDR);
            weaponDRList.Add(BoomerangDR);
            weaponDRList.Add(BlueBoomerangDR);
            weaponDRList.Add(BombDR);
            weaponDRList.Add(FireballDR);
        }

        public void SetSelection(int selection)
        {
            switch (selection)
            {
                case 0:
                    selectionDR = new Rectangle((int)(128 * scale.X), (int)(48 * scale.Y), (int)(16 * scale.X), (int)(16 * scale.Y));
                    break;
                case 1:
                    selectionDR = new Rectangle((int)(152 * scale.X), (int)(48 * scale.Y), (int)(16 * scale.X), (int)(16 * scale.Y));
                    break;
                case 2:
                    selectionDR = new Rectangle((int)(176 * scale.X), (int)(48 * scale.Y), (int)(16 * scale.X), (int)(16 * scale.Y));
                    break;
                case 3:
                    selectionDR = new Rectangle((int)(200 * scale.X), (int)(48 * scale.Y), (int)(16 * scale.X), (int)(16 * scale.Y));
                    break;
                case 4:
                    selectionDR = new Rectangle((int)(128 * scale.X), (int)(64 * scale.Y), (int)(16 * scale.X), (int)(16 * scale.Y));
                    break;
                case 5: 
                    selectionDR = new Rectangle((int)(152 * scale.X), (int)(64 * scale.Y), (int)(16 * scale.X), (int)(16 * scale.Y));
                    break;
                default:
                    return;
            }
        }

        public void Reset()
        {
            cycleIndex = 0;
            SetSelection(cycleIndex);
        }

    }
}
