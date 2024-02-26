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
        private List<Rectangle> rectangles;
        private float xPosition = 0, yPosition = 0;
        private float width = 0, height = 0;

        public MapStaticRectangles (MapHandler map)
        {
            mapList = map.get_map_info();
            rectangles = new List<Rectangle>();
        }

        public List<Rectangle> RectanglesList(Vector2 window_size)
        {
            
            for (int i = 0; i < 12; i++) {
                for (int j = 0; j < 7; j++) {
                    if(!mapList[j,i].Equals("-"))
                    {
                        width = window_size.X / 16;
                        height = window_size.Y / 11;
                        xPosition = (i * width) + (2 * width);
                        yPosition = (j * height) + (2 * height);
                        rectangles.Add(new Rectangle((int)xPosition,(int)yPosition,(int)width,(int)height));
                    }
                }
            }
            return rectangles;
        }
    }
}