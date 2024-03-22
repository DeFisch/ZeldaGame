using Microsoft.Xna.Framework;
using System.Collections.Generic;


namespace ZeldaGame.HUD
{
    public class HUDMapType
    {
        private HUDMapLoader map;
        private string[,] mapList;
        private Vector3 map_size;
        private Vector2 scale;
        private List<Rectangle> dRectangleList;
        private List<Rectangle> sRectangleList;
        private Rectangle sourceRectangle;
        private float xPosition_O = 0, yPosition_O = 0;
        private float width = 0, height = 0;

        public HUDMapType(Vector3 map_size, Vector2 scale)
        {
            this.map_size = map_size;
            this.scale = scale;
            map = new HUDMapLoader();
            mapList = map.load_map();
            sourceRectangle = new Rectangle(0, 0, 0, 0);
            dRectangleList = new List<Rectangle>();
            sRectangleList= new List<Rectangle>();
        }

        public List<Rectangle> getDestinationRectangleList() => dRectangleList;

        public List<Rectangle> getSourceRectangleList() => sRectangleList;


        public void SetLists(string map_name)
        {
            dRectangleList.Clear();
            sRectangleList.Clear();

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (map_name.Equals("Orange"))
                    {
                        width = scale.X * 8;
                        height = scale.Y * 8;
                        xPosition_O = i * width + scale.X * 128;
                        yPosition_O = j * height + scale.Y * 96;
                        sRectangleList.Add(getSourceRectangle_O(mapList[j, i]));
                    }
                    else if(map_name.Equals("Top_Blue"))
                    {
                        width = scale.X * 8;
                        height = scale.Y * 4;
                        xPosition_O = i * width + scale.X * 16;
                        yPosition_O = j * height + scale.Y * 16;
                        sRectangleList.Add(getSourceRectangle_B(mapList[j, i]));
                    }
                    else if(map_name.Equals("Bottom_Blue"))
                    {
                        width = scale.X * 8;
                        height = scale.Y * 4;
                        xPosition_O = i * width + scale.X * 16;
                        yPosition_O = j * height + scale.Y * (16 + 176);
                        sRectangleList.Add(getSourceRectangle_B(mapList[j, i]));
                    }
                    dRectangleList.Add(new Rectangle((int)xPosition_O, (int)yPosition_O, (int)width, (int)height));

                }
            }
        }        

        public Rectangle getSourceRectangle_O(string blockType)
        {
            sourceRectangle = new Rectangle(360, 140, 8, 8);
            switch (blockType)
            {
                case "0":
                    sourceRectangle = new Rectangle(528, 108, 8, 8);
                    break;
                case "1":
                    sourceRectangle = new Rectangle(537, 108, 8, 8);
                    break;
                case "2":
                    sourceRectangle = new Rectangle(546, 108, 8, 8);
                    break;
                case "3":
                    sourceRectangle = new Rectangle(555, 108, 8, 8);
                    break;
                case "4":
                    sourceRectangle = new Rectangle(564, 108, 8, 8);
                    break;
                case "5":
                    sourceRectangle = new Rectangle(573, 108, 8, 8);
                    break;
                case "6":
                    sourceRectangle = new Rectangle(582, 108, 8, 8);
                    break;
                case "7":
                    sourceRectangle = new Rectangle(591, 108, 8, 8);
                    break;
                case "9":
                    sourceRectangle = new Rectangle(609, 108, 8, 8);
                    break;
                case "a":
                    sourceRectangle = new Rectangle(618, 108, 8, 8);
                    break;
                case "b":
                    sourceRectangle = new Rectangle(627, 108, 8, 8);
                    break;
                case "e":
                    sourceRectangle = new Rectangle(654, 108, 8, 8);
                    break;
                default:
                    sourceRectangle = new Rectangle(360, 140, 8, 8);
                    break;
            }
            return sourceRectangle;
        }

        public Rectangle getSourceRectangle_B(string blockType)
        {
            sourceRectangle = new Rectangle(360, 140, 7, 3);
            switch (blockType)
            {
                case "-":
                    sourceRectangle = new Rectangle(663, 111, 8, 4);
                    break;
                default:
                    sourceRectangle = new Rectangle(663, 108, 8, 4);
                    break;
            }
            return sourceRectangle;
        }
    }
}