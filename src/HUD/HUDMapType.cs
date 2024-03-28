using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Diagnostics;
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
        private Dictionary<Vector2,bool> currentPosition;
        private Dictionary<Vector2,bool> defaultRoomCheck;
        private Vector2 scale;
        private List<Rectangle> dRectangleList;
        private List<Rectangle> sRectangleList;
        private Dictionary<string, Rectangle> playerPosition;
        private float xPosition = 0, yPosition = 0;
        private float width = 0, height = 0;
        private bool isMapObtained;
        private CollisionHandler collisionHandler;

        public HUDMapType(Vector2 scale, MapHandler mapHandler, CollisionHandler collisionHandler)
        {
            this.scale = scale;
            this.map = new HUDMapLoader();
            this.mapHandler = mapHandler;
            this.roomPosition = mapHandler.getMapXY();
            this.collisionHandler = collisionHandler;
            this.isMapObtained = collisionHandler.itemActionHandler.isMapObtained();
            isVisited = defaultRoomVisited();
            currentPosition = defaultRoomVisited();
            mapList = map.load_map();
            dRectangleList = new List<Rectangle>();
            sRectangleList= new List<Rectangle>();
            playerPosition = new Dictionary<string, Rectangle>();
        }

        public List<Rectangle> getDestinationRectangleList() => dRectangleList;
        public List<Rectangle> getSourceRectangleList() => sRectangleList;
        public Dictionary<string, Rectangle> playerCurrentPosition() => playerPosition;

        public void SetLists(string map_name)
        {
            dRectangleList.Clear();
            sRectangleList.Clear();
            playerPosition.Clear();
            isMapObtained = collisionHandler.itemActionHandler.isMapObtained();

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
                        if(currentPosition[new Vector2(i-1,j-2)])
                            playerPosition.Add("Orange", new Rectangle((int)xPosition +8, (int)yPosition +8, (int)(scale.X * 3), (int)(scale.Y * 3)));
                            

                    }
                    else if(map_name.Equals("Top_Blue"))
                    {
                        width = scale.X * 8;
                        height = scale.Y * 4;
                        xPosition = i * width + scale.X * 16;
                        yPosition = j * height + scale.Y * 16;
                        if(isMapObtained)
                            sRectangleList.Add(map.getSourceRectangle_B(mapList[j, i]));
                        isRoomVisited(i-1,j-2);
                        if(currentPosition[new Vector2(i-1,j-2)])
                            playerPosition.Add("Top_Blue", new Rectangle((int)xPosition + 7, (int)yPosition, (int)(scale.X * 3), (int)(scale.Y * 3)));
                    }
                    else if(map_name.Equals("Bottom_Blue"))
                    {
                        width = scale.X * 8;
                        height = scale.Y * 4;
                        xPosition = i * width + scale.X * 16;
                        yPosition = j * height + scale.Y * (16 + 176);
                        if(isMapObtained)
                            sRectangleList.Add(map.getSourceRectangle_B(mapList[j, i]));
                        isRoomVisited(i-1,j-2);
                        if(currentPosition[new Vector2(i-1,j-2)])
                            playerPosition.Add("Bottom_Blue", new Rectangle((int)xPosition + 7, (int)yPosition, (int)(scale.X * 3), (int)(scale.Y * 3)));
                    }
                    dRectangleList.Add(new Rectangle((int)xPosition, (int)yPosition, (int)width, (int)height));

                }
            }
        }  

        public void isRoomVisited(int xPosition, int yPosition) 
        {
            roomPosition = mapHandler.getMapXY();
            currentPosition = defaultRoomVisited();
            if (roomPosition.Equals(new Vector2(xPosition,yPosition)))
            {
                isVisited[new Vector2(xPosition,yPosition)] = true;
                currentPosition[new Vector2(xPosition,yPosition)] = true;
            }
                
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