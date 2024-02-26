using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections;
using System.Collections.Generic;

namespace ZeldaGame.Map
{
    public class MapStaticRectangles
    {
        private String[,] mapList;
        private MapHandler map;
        private List<Rectangle> rectangles;
        private int x = 0, y = 0;

        public MapStaticRectangles (MapHandler map)
        {
            this.map = map;
            mapList = map.get_map_info();
            rectangles = new List<Rectangle>();
        }

        public List<Rectangle> RectanglesList()
        {
            
            for (int i = 0; i < 12; i++) {
                for (int j = 0; j < 7; j++) {
                    if(!mapList[j,i].Equals("-"))
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