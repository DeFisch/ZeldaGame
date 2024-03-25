using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections;
using System.Collections.Generic;
using ZeldaGame.Map;

namespace ZeldaGame.HUD
{
    public class HUDMapHandler
    {
        private HUDMapType mapType;
        private Texture2D texture;
        private List<Rectangle> sourceRectangle;
        private List<Rectangle> targetRectangle;

        public HUDMapHandler(Texture2D texture, Vector2 scale, MapHandler map)
        {
            this.texture = texture;
            sourceRectangle = new List<Rectangle>();
            targetRectangle = new List<Rectangle>();
            mapType = new HUDMapType(scale, map);
        }

        public void Draw(SpriteBatch spriteBatch, int isDisplayed)
        {
            if (isDisplayed == 1)
            {
                DrawHUDMap("Bottom_Blue", spriteBatch);
                DrawHUDMap("Orange", spriteBatch);
            }
            if (isDisplayed == -1)
            {
                DrawHUDMap("Top_Blue", spriteBatch);
            }
        }

        public void DrawHUDMap(string map_name, SpriteBatch spriteBatch)
        {
            mapType.SetLists(map_name);
            sourceRectangle = mapType.getSourceRectangleList();
            targetRectangle = mapType.getDestinationRectangleList();
            for (int i = 0; i < sourceRectangle.Count; i++)
                spriteBatch.Draw(texture, targetRectangle[i], sourceRectangle[i], Color.White);
        }

        public void Update()
        {
            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                    mapType.isRoomVisited(i-1,j-2);
        }

        public void Reset()
        {
            mapType.Reset();
        }
    }
}
