using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using ZeldaGame.Map;

namespace ZeldaGame.HUD
{
    public class HUDMapHandler
    {
        private HUDMapType mapType;
        private Texture2D texture;
        private List<Rectangle> sourceRectangle;
        private List<Rectangle> targetRectangle;
        private Dictionary<string, Rectangle> playerPosition;
        private CollisionHandler collision;
        private Vector2 scale;

        public HUDMapHandler(Texture2D texture, Vector2 scale, MapHandler map, CollisionHandler collisionHandler)
        {
            this.texture = texture;
            this.collision = collisionHandler;
            this.scale = scale;
            sourceRectangle = new List<Rectangle>();
            targetRectangle = new List<Rectangle>();
            playerPosition = new Dictionary<string, Rectangle>();
            mapType = new HUDMapType(scale, map, collisionHandler);
        }

        public void Draw(SpriteBatch spriteBatch, bool isDisplayed)
        {
            if (isDisplayed)
            {
                DrawHUDMap("Bottom_Blue", spriteBatch);
                DrawHUDMap("Orange", spriteBatch);
            }
            if (!isDisplayed)
                DrawHUDMap("Top_Blue", spriteBatch);
        }

        public void DrawHUDMap(string map_name, SpriteBatch spriteBatch)
        {
            mapType.SetLists(map_name);
            sourceRectangle = mapType.getSourceRectangleList();
            targetRectangle = mapType.getDestinationRectangleList();
            playerPosition = mapType.playerCurrentPosition();
            for (int i = 0; i < sourceRectangle.Count; i++)
                spriteBatch.Draw(texture, targetRectangle[i], sourceRectangle[i], Color.White);
            if(!map_name.Equals("Orange"))
                DrawFinalRoom(map_name, spriteBatch);
            spriteBatch.Draw(texture, playerPosition[map_name], new Rectangle(519, 126, 3, 3), Color.White);
        }

        public void DrawFinalRoom(string map_name, SpriteBatch spriteBatch)
        {
            if(collision.itemActionHandler.isCompassObtained())
            {
                Rectangle targetSR = new Rectangle(537, 126, 3, 3);
                Rectangle targetDR = new Rectangle(0,0,0,0);
                if(map_name.Equals("Bottom_Blue"))
                    targetDR = new Rectangle((int)(66 * scale.X), (int)(204 * scale.Y), (int)(targetSR.Width * scale.X), (int)(targetSR.Height * scale.Y));
                else if(map_name.Equals("Top_Blue"))
                    targetDR = new Rectangle((int)(66 * scale.X), (int)(28 * scale.Y), (int)(targetSR.Width * scale.X), (int)(targetSR.Height * scale.Y));
                spriteBatch.Draw(texture, targetDR, targetSR, Color.White);
            }
        }

        public void Update()
        {
            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                    mapType.isRoomVisited(i-1,j-2);
        }

        public void Reset() => mapType.Reset();
    }
}
