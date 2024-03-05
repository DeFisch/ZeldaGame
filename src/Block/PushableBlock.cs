using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using ZeldaGame.Player;
using ZeldaGame.Map;
using System;
using System.Diagnostics;

namespace ZeldaGame.Block {
	public class PushableBlock {
		private Texture2D texture;
        private MapHandler map;
        private IPlayer player;
		private Vector2 window_size;
        private Vector2 scale;
        private Vector2 room;
        private bool isPushing;
        private bool pushed1;
        private bool pushed2;
        private bool isDrawn;
        private int xPosition = 0,yPosition = 0;
        private float dx = 0, dy = 0;
    	private Rectangle sourceRectangle;
        private Rectangle destinationRectangle;

		public PushableBlock(Texture2D texture, Vector2 window_size, MapHandler map, IPlayer palyer, Vector2 scale) {
			this.texture = texture;
			this.window_size = window_size;
            this.map = map;
            this.player = palyer;
            this.scale = scale;
            this.room = new Vector2(9,9);
            isPushing = false;
            pushed1 = false;
            pushed2 = false;
            isDrawn = false;
            sourceRectangle = new Rectangle(370, 65, 16, 16);
            destinationRectangle = new Rectangle(0,0,0,0);
		}

		public void Update() 
        {
            if(isDrawn)
            {
                PlayerIsPushing();
                if(isPushing && !pushed1 && room.Equals(new Vector2(1,0)))
                {
                    pushDirection();
                    pushed1 = true;
                }
                if(isPushing && !pushed2 && room.Equals(new Vector2(1,2)))
                {
                    pushDirection();
                    pushed2 = true;
                }
            }
            CollisionAfterPush();
		}

		public void Draw(SpriteBatch spriteBatch) {
            room = map.getMapXY();
            if (room.Equals(new Vector2(1,0)))// pushable block in room_0_1
            {
                if (!pushed1)
                {
                    xPosition = 6;
                    yPosition = 5;
                }
                DrawBlock(spriteBatch);
            }
            else if (room.Equals(new Vector2(1,2)))// pushable block in room_2_1
            {
                if (!pushed2)
                {
                    xPosition = 7;
                    yPosition = 5;
                }
                DrawBlock(spriteBatch);
            }
		}
        public void PlayerIsPushing()
        {
            Rectangle playerHitBox = player.GetPlayerHitBox();
            dx = playerHitBox.X - destinationRectangle.X;
            dy = playerHitBox.Y - destinationRectangle.Y;
            if(playerHitBox.Intersects(destinationRectangle))
                isPushing = true;
            else
                isPushing = false;
        }
        public void pushDirection()
        {
            Debug.WriteLine(xPosition);
            if(Math.Abs(dx) > Math.Abs(dy))//check for left/right push
            {
                if(dx > 0)
                {
                    Push_Left();
                    Debug.WriteLine(xPosition);
                }      
                else if(dx < 0)
                    Push_Right();
            }
            else if (Math.Abs(dx) < Math.Abs(dy))//check for up/down push
            {
                if(dy > 0)
                    Push_Up();
                else if(dy < 0)
                    Push_Down();
            }
        }
        public void DrawBlock(SpriteBatch spriteBatch)
        {
            destinationRectangle = new Rectangle((int)(window_size.X/16* xPosition), (int)(window_size.Y/11*yPosition), (int)(sourceRectangle.Width * scale.X), (int)(sourceRectangle.Height * scale.Y));
		    spriteBatch.Draw(texture, destinationRectangle, sourceRectangle, Color.White);
            isDrawn = true;
        }
        public void CollisionAfterPush()
        {
            if((pushed1 && room.Equals(new Vector2(1,0)))||(pushed2 && room.Equals(new Vector2 (1,2))))
            {
                if (player.GetPlayerHitBox().Intersects(destinationRectangle))
                {
                    player.Colliding(destinationRectangle);
                }
            }
        }
        public void Push_Up() => yPosition -= 1;
        public void Push_Down() => yPosition += 1;
        public void Push_Left() => xPosition -= 1;
        public void Push_Right() => xPosition += 1;
        public void Reset()
        {
            isPushing = false;
            pushed1 = false;
            pushed2 = false;
            isDrawn = false;
        }
	}
}