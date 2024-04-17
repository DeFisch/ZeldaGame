using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using ZeldaGame.Map;

namespace ZeldaGame.HUD
{
    public class HUDMapTeleport
    {
        private HUDMapType mapType;
        private MapHandler map;
        private Rectangle selectionSR;
        private Rectangle selectionDR;
        private List<Rectangle> roomDRList;
        private List<int> roomVisitedIndex;
        private Dictionary<Vector2, bool> roomVisited;
        private Vector2 targetRoom;
        private int cycleIndex;

        public HUDMapTeleport(HUDMapType mapType, MapHandler map)
        {
            this.mapType = mapType;
            this.map = map;
            selectionSR = new Rectangle(600, 126, 8, 8);
            cycleIndex = 0;
            roomVisitedIndex = new List<int>();
            mapType.SetLists("Orange");
            roomVisited = mapType.getRoomVisited();
            roomDRList = mapType.getDestinationRectangleList();
            SetSelection();
            selectionDR = roomDRList[roomVisitedIndex[cycleIndex]];
        }

        public void Draw(Texture2D texture, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, selectionDR, selectionSR, Color.White);
        }

        public void Update()
        {
            mapType.SetLists("Orange");
            roomVisited = mapType.getRoomVisited();
            roomDRList = mapType.getDestinationRectangleList();
            SetSelection();
        }

        public void CycleList(int cycleDirection)
        {
            int listLength = roomVisitedIndex.Count;
            if (cycleDirection == 1)
                cycleIndex = (cycleIndex + 1) % listLength;
            else if (cycleDirection == 0)
                cycleIndex = (cycleIndex - 1 + listLength) % listLength;
            selectionDR = roomDRList[roomVisitedIndex[cycleIndex]];
        }

        public void SetSelection()
        {
            roomVisitedIndex.Clear();
            int i = 0;
            foreach(bool isVisited in roomVisited.Values)
            {
                if(isVisited)
                    roomVisitedIndex.Add(i);
                i++;
            }
        }

        public void Teleport()
        {
            int i = 0;
            foreach(Vector2 rooms in roomVisited.Keys)
            {
                if(i == roomVisitedIndex[cycleIndex])
                    targetRoom = rooms;
                i++;
            }
            map.switch_map((int)targetRoom.Y, (int)targetRoom.X);
        }
    }
}
