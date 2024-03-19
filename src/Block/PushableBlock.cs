using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using ZeldaGame.Player;
using ZeldaGame.Map;
using System;

namespace ZeldaGame.Block {
	public class PushableBlock {
        private MapHandler map;
        private IPlayer player;
        private Vector2 room;
        private bool isPushing;
        private bool pushed;
        private bool isDrawn;
        private int xPosition,yPosition;
        private Vector2 initialPosition;
        private float dx = 0, dy = 0;
    	private Rectangle sourceRectangle;
        private Rectangle destinationRectangle;

		public PushableBlock(MapHandler map, IPlayer player, Vector2 initial_position) {
            this.map = map;
            this.player = player;
            this.room = new Vector2(9,9);
            this.initialPosition = initial_position;
            xPosition = (int)initialPosition.X;
            yPosition = (int)initialPosition.Y;
            isPushing = false;
            pushed = false;
            isDrawn = false;
            sourceRectangle = new Rectangle(370, 65, 16, 16);
            destinationRectangle = new Rectangle(0,0,0,0);
		}

		public void Update() 
        {
            if(isDrawn)
            {
                PlayerIsPushing();
                if(isPushing && !pushed)
                {
                    pushDirection();
                    pushed = true;
                }
            }
            CollisionAfterPush();
		}

		public void Draw(SpriteBatch spriteBatch, Texture2D texture, Vector2 window_size, Vector2 scale) {
            room = map.getMapXY();
            if (!roomCheck().Equals(3))
            {
                destinationRectangle = new Rectangle((int)(window_size.X/16* xPosition), (int)(window_size.Y/11*yPosition), (int)(sourceRectangle.Width * scale.X), (int)(sourceRectangle.Height * scale.Y));
		        spriteBatch.Draw(texture, destinationRectangle, sourceRectangle, Color.White);
                isDrawn = true;
            }
		}

        public void PlayerIsPushing()
        {
            Rectangle playerHitBox = player.GetPlayerHitBox();
            dx = playerHitBox.X - destinationRectangle.X;
            dy = playerHitBox.Y - destinationRectangle.Y;
            if(playerHitBox.Intersects(destinationRectangle)){
                if (!isPushing && roomCheck().Equals(2))
                    Globals.audioLoader.Play("LOZ_Door_Unlock");
                isPushing = true;
            }
            else
                isPushing = false;
        }

        public void pushDirection()
        {
            if(Math.Abs(dx) > Math.Abs(dy))//check for left/right push
            {
                if(dx > 0)
                    Push_Left(); 
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

        public void CollisionAfterPush()
        {
            if(pushed && !roomCheck().Equals(3))
            {
                if (player.GetPlayerHitBox().Intersects(destinationRectangle))
                    player.OnCollision(destinationRectangle);
            }
        }

        public int roomCheck()
        {
            if(initialPosition.Equals(new Vector2(6,5)) && room.Equals(new Vector2(1,0)))
                return 1;
            else if(initialPosition.Equals(new Vector2(7,5)) && room.Equals(new Vector2(1,2)))
                return 2;
            return 3;
        }
        
        public void Push_Up() => yPosition -= 1;
        public void Push_Down() => yPosition += 1;
        public void Push_Left() => xPosition -= 1;
        public void Push_Right() => xPosition += 1;
        public Rectangle GetRectangle() => destinationRectangle;
        public bool isOpen() => pushed;
        public void Reset()
        {
            isPushing = false;
            pushed = false;
            isDrawn = false;
            xPosition = (int)initialPosition.X;
            yPosition = (int)initialPosition.Y;
        }
	}
}