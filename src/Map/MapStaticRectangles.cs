using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint0.Block;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection.Metadata;
using ZeldaGame.Enemy;

namespace ZeldaGame.Map
{
    public class MapStaticRectangles
    {
        private String[,] mapList;
        private List<Rectangle> dRectangleList;
        private List<Rectangle> sRectangleList;
        private Rectangle sourceRectangle;
        private Rectangle destinationRectangle;
        private float xPosition = 0, yPosition = 0;
        private float width = 0, height = 0;

        public MapStaticRectangles (MapHandler map)
        {
            mapList = map.get_map_info();
            sourceRectangle = new Rectangle(306, 49, 16, 16);
            dRectangleList = new List<Rectangle>();
            sRectangleList = new List<Rectangle>();
        }

        public List<Rectangle> getDestinationRectangleList()
        {
            return dRectangleList;
        }

        public List<Rectangle> getSourceRectangleList()
        {
            return sRectangleList;
        }

        public void SetLists(Vector2 window_size)
        {
            for (int i = 0; i < 12; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    if (!mapList[j, i].Equals("-"))
                    {
                        width = window_size.X / 16;
                        height = window_size.Y / 11;
                        xPosition = (i * width) + (2 * width);
                        yPosition = (j * height) + (2 * height);
                        dRectangleList.Add(new Rectangle((int)xPosition, (int)yPosition, (int)width, (int)height));
                        sRectangleList.Add(getSourceRectangle(mapList[j, i]));
                    }
                }
            }
        }

        public Rectangle getSourceRectangle(string blockType)
        {
            sourceRectangle = new Rectangle(306, 49, 16, 16);
            switch (blockType)
            {
                case "b":
                    sourceRectangle = new Rectangle(354, 81, 16, 16);
                    break;
                case "w":
                    sourceRectangle = new Rectangle(563, 49, 16, 16);
                    break;
                case "st":
                    sourceRectangle = new Rectangle(386, 81, 16, 16);
                    break;
                case "f1":
                    sourceRectangle = new Rectangle(1366, 258, 16, 16);
                    break;
                case "d1":
                    sourceRectangle = new Rectangle(1430, 242, 16, 16);
                    break;
                case "f2":
                    sourceRectangle = new Rectangle(611, 934, 16, 16);
                    break;
                case "d2":
                    sourceRectangle = new Rectangle(659, 934, 16, 16);
                    break;
                default:
                    break;
            }
            return sourceRectangle;
        }

    }
}