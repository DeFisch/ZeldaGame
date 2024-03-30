﻿using Microsoft.Xna.Framework;
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
        public static int[] inventoryCounts;
        private bool getMap;
        private bool getCompass;
        private Game1 game;
        private readonly int heartHealing = 1;

        public ItemActionHandler(Game1 game) {
            inventoryCounts = new int[9];
            inventoryCounts[8] = 8;
            getMap = false;
            getCompass = false;
            this.game = game;
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
                case Heart:
                    game.Link.GainHealth(heartHealing);
                    inventoryCounts[4]++;
                    break;
                case HeartContainer:
                    game.Link.GainMaxHealth(heartHealing);
                    inventoryCounts[5]++;
                    break;
                case Triforce:
                    inventoryCounts[6]++;
                    break;
                case Bow:
                    inventoryCounts[7]++;
                    break;
                case Bomb:
                    if (inventoryCounts[8] + 4 < 8)
                    {
                        inventoryCounts[8] = inventoryCounts[8] + 4;
                    }
                    else
                    {
                        inventoryCounts[8] = 8;
                    }
                    break;
                case Map:
                    getMap = true;
                    break;
                case Compass:
                    getCompass = true;
                    break;
                default:
                    break;

            }

        }

        public bool isMapObtained() => getMap;
        public bool isCompassObtained() => getCompass;

        public void Reset()
        {
            inventoryCounts = new int[9];
            inventoryCounts[8] = 8;
            getMap = false;
            getCompass = false;
        }

    }
}
