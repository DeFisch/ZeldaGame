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
        private MapHandler map;
        private List<Rectangle> dRectangleList;
        private List<Rectangle> sRectangleList;
        private Rectangle sourceRectangle;
        private float xPosition = 0, yPosition = 0;
        private float width = 0, height = 0;

        public MapStaticRectangles (MapHandler map)
        {
            this.map = map;
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
            dRectangleList.Clear();
            sRectangleList.Clear();

            string[,] mapList = map.get_map_info();
            addBoundaryRectangles(dRectangleList, sRectangleList, window_size); // add boundary rectangles
            
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

        public void addBoundaryRectangles(List<Rectangle> dRectangleList, List<Rectangle> sRectangleList, Vector2 window_size)
        {
            Vector2 roomLocation = map.getMapXY();
            if(map.is_map_available((int)roomLocation.X, (int)roomLocation.Y - 1)) {
                // if there is a room above
                dRectangleList.Add(new Rectangle(0, 0, (int)(window_size.X*0.46875), (int)(0.18*window_size.Y)));
                dRectangleList.Add(new Rectangle((int)(window_size.X*0.53125), 0, (int)(window_size.X*0.46875), (int)(0.18*window_size.Y)));
                sRectangleList.Add(getSourceRectangle("w"));
                sRectangleList.Add(getSourceRectangle("w"));
            }else {
                // if there is no room above
                dRectangleList.Add(new Rectangle(0, 0, (int)window_size.X, (int)(0.18*window_size.Y)));
                sRectangleList.Add(getSourceRectangle("w"));
            }
            if(map.is_map_available((int)roomLocation.X, (int)roomLocation.Y + 1)) {
                // if there is a room below
                dRectangleList.Add(new Rectangle(0, (int)(window_size.Y*0.82), (int)(window_size.X*0.46875), (int)(0.18*window_size.Y)));
                dRectangleList.Add(new Rectangle((int)(window_size.X*0.53125), (int)(window_size.Y*0.82), (int)(window_size.X*0.46875), (int)(0.18*window_size.Y)));
                sRectangleList.Add(getSourceRectangle("w"));
                sRectangleList.Add(getSourceRectangle("w"));
            }else {
                // if there is no room below
                dRectangleList.Add(new Rectangle(0, (int)(window_size.Y*0.82), (int)window_size.X, (int)(0.18*window_size.Y)));
                sRectangleList.Add(getSourceRectangle("w"));
            }
            if(map.is_map_available((int)roomLocation.X - 1, (int)roomLocation.Y)) {
                // if there is a room to the left
                dRectangleList.Add(new Rectangle(0, 0, (int)(0.125*window_size.X), (int)(window_size.Y*0.45)));
                dRectangleList.Add(new Rectangle(0, (int)(window_size.Y*0.55), (int)(0.125*window_size.X), (int)(window_size.Y*0.45)));
                sRectangleList.Add(getSourceRectangle("w"));
                sRectangleList.Add(getSourceRectangle("w"));
            }else {
                // if there is no room to the left
                dRectangleList.Add(new Rectangle(0, 0, (int)(0.125*window_size.X), (int)window_size.Y));
                sRectangleList.Add(getSourceRectangle("w"));
            }
            if(map.is_map_available((int)roomLocation.X + 1, (int)roomLocation.Y)) {
                // if there is a room to the right
                dRectangleList.Add(new Rectangle((int)(window_size.X*0.875), 0, (int)(0.125*window_size.X), (int)(window_size.Y*0.45)));
                dRectangleList.Add(new Rectangle((int)(window_size.X*0.875), (int)(window_size.Y*0.55), (int)(0.125*window_size.X), (int)(window_size.Y*0.45)));
                sRectangleList.Add(getSourceRectangle("w"));
                sRectangleList.Add(getSourceRectangle("w"));
            }else {
                // if there is no room to the right
                dRectangleList.Add(new Rectangle((int)(window_size.X*0.875), 0, (int)(0.125*window_size.X), (int)window_size.Y));
                sRectangleList.Add(getSourceRectangle("w"));
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