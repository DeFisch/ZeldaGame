using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeldaGame.Map;
using ZeldaGame.Player;
using static ZeldaGame.Game1;

namespace ZeldaGame.Items
{
    public class ItemActionHandler
    {
        public int[] inventoryCounts;
        private bool getMap;
        public ItemActionHandler() {
            inventoryCounts = new int[10];
            inventoryCounts[9] = 8;
            getMap = false;
        }  
        public void InventoryCounts(IItemSprite item)
        {
            switch (item)
            {
                case BlueRuby:
                    inventoryCounts[0]++;
                    break;
                case YellowRuby:
                    inventoryCounts[1]++;
                    break;
                case Key:
                    inventoryCounts[2]++;
                    break;
                case Clock:
                    inventoryCounts[3]++;
                    break;
                case Compass:
                    inventoryCounts[4]++;
                    break;
                case Heart:
                    inventoryCounts[5]++;
                    break;
                case HeartContainer:
                    inventoryCounts[6]++;
                    break;
                case Triforce:
                    inventoryCounts[7]++;
                    break;
                case Bow:
                    inventoryCounts[8]++;
                    break;
                case Bomb:
                    if (inventoryCounts[9] + 4 < 8)
                    {
                        inventoryCounts[9] = inventoryCounts[9] + 4;
                    }
                    break;
                case Map:
                    getMap = true;
                    break;
                default:
                    inventoryCounts[10]++;
                    break;

            }

        }

        public bool isMapObtained() => getMap;

        public void Reset()
        {
            inventoryCounts = new int[10];
            inventoryCounts[9] = 8;
            getMap = false;
        }

    }
}
