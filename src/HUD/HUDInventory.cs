using System.Collections.Generic;
using Enemy.Projectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ZeldaGame.Items;
using System.Diagnostics;
using static ZeldaGame.Globals;
using ZeldaGame.Player;

namespace ZeldaGame.HUD
{
    public class HUDInventory
    {
        
        private readonly IPlayer player;
        private readonly List<Rectangle> equipSRList;
        private readonly List<Rectangle> equipDRList;
		private readonly List<Rectangle> swordSRList;
		private int cycleIndex;
        private Texture2D texture;
        private Vector2 scale;
        private Rectangle equipSelectionSR;
        private Rectangle equipSelectionDR;
		private Rectangle targetEquipDRTop;
        private Rectangle targetEquipDRBottom;
		private readonly List<PlayerProjectiles> equips = new() 
        {   PlayerProjectiles.WoodenArrow, PlayerProjectiles.BlueArrow, PlayerProjectiles.Boomerang, 
            PlayerProjectiles.BlueBoomerang, PlayerProjectiles.Bomb, PlayerProjectiles.Fireball };

        public HUDInventory(Texture2D texture, Vector2 scale, IPlayer player)
        {
            this.player = player;
            this.texture = texture;
            this.scale = scale;
            cycleIndex = 0;
            equipSRList = new List<Rectangle>();
            equipDRList = new List<Rectangle>();
            swordSRList = new List<Rectangle>();

			SetEquip();
            SetSword();
        }

        public void Draw(SpriteBatch spriteBatch, bool isDisplayed)
        {
            if (isDisplayed)
            {
                spriteBatch.Draw(texture, equipSelectionDR, equipSelectionSR, Color.White);
                spriteBatch.Draw(texture, targetEquipDRTop, equipSRList[cycleIndex], Color.White);
                spriteBatch.Draw(texture, targetEquipDRBottom, equipSRList[cycleIndex], Color.White);
                for (int i = 0; i < equipSRList.Count; i++)
                    spriteBatch.Draw(texture, equipDRList[i], equipSRList[i], Color.White);
            }
        }

        public Rectangle CurrentEquipSprite() => equipSRList[cycleIndex];
        public Rectangle CurrentSwordSprite() => swordSRList[(int)player.GetSword()];
		public PlayerProjectiles CurrentEquip() => equips[cycleIndex];

        public void CycleList(int cycleDirection)
        {
            int listLength = equipSRList.Count;
            if (cycleDirection == 1)
                cycleIndex = (cycleIndex + 1) % listLength;
            else if (cycleDirection == 0)
                cycleIndex = (cycleIndex - 1 + listLength) % listLength;
            SetEquipSelection(cycleIndex);
        }

        public void SetEquip()
        {
            Rectangle ArrowSR = new (615, 137, 8, 16);
            Rectangle BlueArrowSR = new (624, 137, 8, 16);
            Rectangle BoomerangSR = new (584, 137, 8, 16);
            Rectangle BlueBoomerangSR = new(593, 137, 8, 16);
            Rectangle BombSR = new (604, 137, 8, 16);
            Rectangle FireballSR = new (653, 137, 8, 16);

            Rectangle ArrowDR = new ((int)(132 * scale.X), (int)(48 * scale.Y), (int)(8 * scale.X), (int)(16 * scale.Y));
            Rectangle BlueArrowDR = new ((int)(156 * scale.X), (int)(48 * scale.Y), (int)(8 * scale.X), (int)(16 * scale.Y));
            Rectangle BoomerangDR = new ((int)(180 * scale.X), (int)(48 * scale.Y), (int)(8 * scale.X), (int)(16 * scale.Y));
            Rectangle BlueBoomerangDR = new ((int)(204 * scale.X), (int)(48 * scale.Y), (int)(8 * scale.X), (int)(16 * scale.Y));
            Rectangle BombDR = new ((int)(132 * scale.X), (int)(64 * scale.Y), (int)(8 * scale.X), (int)(16 * scale.Y));
            Rectangle FireballDR = new ((int)(156 * scale.X), (int)(64 * scale.Y), (int)(8 * scale.X), (int)(16 * scale.Y));

            targetEquipDRTop = new Rectangle((int)(68 * scale.X), (int)(48 * scale.Y), (int)(8 * scale.X), (int)(16 * scale.Y));
            targetEquipDRBottom = new Rectangle((int)(128 * scale.X), (int)(200 * scale.Y), (int)(8 * scale.X), (int)(16 * scale.Y));
            equipSelectionDR = new Rectangle((int)(128 * scale.X), (int)(48 * scale.Y), (int)(16 * scale.X), (int)(16 * scale.Y));
            equipSelectionSR = new Rectangle(519, 137, 16, 16);

            equipSRList.Add(ArrowSR);
            equipSRList.Add(BlueArrowSR);
            equipSRList.Add(BoomerangSR);
            equipSRList.Add(BlueBoomerangSR);
            equipSRList.Add(BombSR);
            equipSRList.Add(FireballSR);

            equipDRList.Add(ArrowDR);
            equipDRList.Add(BlueArrowDR);
            equipDRList.Add(BoomerangDR);
            equipDRList.Add(BlueBoomerangDR);
            equipDRList.Add(BombDR);
            equipDRList.Add(FireballDR);
        }

        public void SetSword() {
            Rectangle NoSwordSR = new (410, 35, 8, 16);
			Rectangle WoodSwordSR = new (555, 137, 8, 16);
			Rectangle WhiteSwordSR = new (564, 137, 8, 16);
			Rectangle MagicSwordSR = new (573, 137, 8, 16);

            swordSRList.Add(NoSwordSR);
            swordSRList.Add(WoodSwordSR);
            swordSRList.Add(WhiteSwordSR);
            swordSRList.Add(MagicSwordSR);
		}

        public void SetEquipSelection(int selection)
        {
            switch (selection)
            {
                case 0:
                    equipSelectionDR = new Rectangle((int)(128 * scale.X), (int)(48 * scale.Y), (int)(16 * scale.X), (int)(16 * scale.Y));
                    break;
                case 1:
                    equipSelectionDR = new Rectangle((int)(152 * scale.X), (int)(48 * scale.Y), (int)(16 * scale.X), (int)(16 * scale.Y));
                    break;
                case 2:
                    equipSelectionDR = new Rectangle((int)(176 * scale.X), (int)(48 * scale.Y), (int)(16 * scale.X), (int)(16 * scale.Y));
                    break;
                case 3:
                    equipSelectionDR = new Rectangle((int)(200 * scale.X), (int)(48 * scale.Y), (int)(16 * scale.X), (int)(16 * scale.Y));
                    break;
                case 4:
                    equipSelectionDR = new Rectangle((int)(128 * scale.X), (int)(64 * scale.Y), (int)(16 * scale.X), (int)(16 * scale.Y));
                    break;
                case 5: 
                    equipSelectionDR = new Rectangle((int)(152 * scale.X), (int)(64 * scale.Y), (int)(16 * scale.X), (int)(16 * scale.Y));
                    break;
                default:
                    return;
            }
        }

        public void Reset()
        {
            cycleIndex = 0;
            SetEquipSelection(cycleIndex);
        }

    }
}
