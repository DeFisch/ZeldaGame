using Microsoft.Xna.Framework;
using System.Collections.Generic;
using ZeldaGame.Map;


namespace ZeldaGame.HUD
{
    public class HUDMapType
    {
        private HUDMapLoader map;
        private MapHandler mapHandler;
        private string[,] mapList;
        private Vector2 roomPosition;
        private Dictionary<Vector2,bool> isVisited;
        private Dictionary<Vector2,bool> defaultRoomCheck;
        private Vector2 scale;
        private List<Rectangle> dRectangleList;
        private List<Rectangle> sRectangleList;
        private Rectangle sourceRectangle;
        private float xPosition = 0, yPosition = 0;
        private float width = 0, height = 0;

        public HUDMapType(Vector2 scale, MapHandler mapHandler)
        {
            this.scale = scale;
            this.map = new HUDMapLoader();
            this.mapHandler = mapHandler;
            this.roomPosition = mapHandler.getMapXY();
            isVisited = defaultRoomVisited();
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
                        xPosition = i * width + scale.X * 128;
                        yPosition = j * height + scale.Y * 96;
                        isRoomVisited(i-1,j-2);
                        if(isVisited[new Vector2(i-1,j-2)]) 
                            sRectangleList.Add(map.getSourceRectangle_O(mapList[j, i]));
                        else 
                            sRectangleList.Add(new Rectangle(360, 140, 8, 8));

                    }
                    else if(map_name.Equals("Top_Blue"))
                    {
                        width = scale.X * 8;
                        height = scale.Y * 4;
                        xPosition = i * width + scale.X * 16;
                        yPosition = j * height + scale.Y * 16;
                        sRectangleList.Add(map.getSourceRectangle_B(mapList[j, i]));
                    }
                    else if(map_name.Equals("Bottom_Blue"))
                    {
                        width = scale.X * 8;
                        height = scale.Y * 4;
                        xPosition = i * width + scale.X * 16;
                        yPosition = j * height + scale.Y * (16 + 176);
                        sRectangleList.Add(map.getSourceRectangle_B(mapList[j, i]));
                    }
                    dRectangleList.Add(new Rectangle((int)xPosition, (int)yPosition, (int)width, (int)height));

                }
            }
        }  

        public void isRoomVisited(int xPosition, int yPosition) 
        {
            roomPosition = mapHandler.getMapXY();
            if (roomPosition.Equals(new Vector2(xPosition,yPosition)))
                isVisited[new Vector2(xPosition,yPosition)] = true;
        }

        public Dictionary<Vector2, bool> defaultRoomVisited()
        {
            defaultRoomCheck = new Dictionary<Vector2, bool>();
            for (int i = -1; i < 7; i++)
                for (int j = -2; j < 6; j++)
                    defaultRoomCheck.Add(new Vector2(i,j),false);
            return defaultRoomCheck;
        }
        
        public void Reset()
        {
            isVisited = defaultRoomVisited();
        }
    }
}