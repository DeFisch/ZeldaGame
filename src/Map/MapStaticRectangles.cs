using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections;
using System.Collections.Generic;

namespace ZeldaGame.Map
{
    public class MapStaticRectangles
    {
        private String[,] mapLsit;
        private MapHandler map;
        private List<Rectangle> rectangles;
        private int x = 0, y = 0;

        public MapStaticRectangles (Game1 game)
        {
            this.map = game.map;
            this.mapLsit = game.map.get_map_info();
        }

        public List<Rectangle> RectanglesList()
        {
            
            for (int i = 0; i < 12; i++) {
                for (int j = 0; j < 7; j++) {
                    if(!mapLsit.Equals('_'))
                    {
                        x = i * 50;
                        y = j * 50;
                        rectangles.Add(new Rectangle(x,y,50,50));
                    }
                }
            }
            return rectangles;
        }
    }
}