using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using ZeldaGame.Map;

namespace ZeldaGame.HUD
{
    public class HUDMapTeleport
    {
        private HUDMapType mapType;
        private Rectangle selectionSR;
        private Rectangle selectionDR;
        private List<Rectangle> roomDRList;
        private Dictionary<Vector2, bool> roomVisited;
        private int cycleIndex;

        public HUDMapTeleport(HUDMapType mapType)
        {
            this.mapType = mapType;
            selectionSR = new Rectangle(600, 126, 8, 8);
            cycleIndex = 0;
            mapType.SetLists("Orange");
            roomVisited = mapType.getRoomVisited();
            roomDRList = mapType.getDestinationRectangleList();
            selectionDR = roomDRList[cycleIndex];
        }

        public void Draw(Texture2D texture, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, selectionDR, selectionSR, Color.White);

        }

        public void CycleList(int cycleDirection)
        {
            int listLength = roomDRList.Count;
            if (cycleDirection == 1)
                cycleIndex = (cycleIndex + 1) % listLength;
            else if (cycleDirection == 0)
                cycleIndex = (cycleIndex - 1 + listLength) % listLength;
            selectionDR = roomDRList[cycleIndex];
        }
    }
}
