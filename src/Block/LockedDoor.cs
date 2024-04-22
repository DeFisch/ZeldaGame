using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using ZeldaGame.Map;
using ZeldaGame.Player;
using System.Data.Common;

namespace ZeldaGame.Block {
	public class LockedDoor : IBlock {
		private Vector2 position;
        private PushableBlock pushableBlock;
        private MapHandler map;
        private IPlayer player;
        private Rectangle sourceRectangle;
        private Rectangle destinationRectangle;
        private bool isDrawn;

        public LockedDoor(PushableBlock pushableBlock, MapHandler map, IPlayer player, Vector2 position) {
			this.position = position;
            this.pushableBlock = pushableBlock;
            this.map = map;
            this.player = player;
            sourceRectangle = new Rectangle(258, 339, 16, 16);
        }

		public void Update() {
            if (isDrawn && player.GetHitBox().Intersects(destinationRectangle))
                player.OnCollision(destinationRectangle);
		}

		public void Draw(SpriteBatch spriteBatch, Texture2D texture, Vector2 scale) {
            if(!pushableBlock.isOpen() && map.getMapXY().Equals(new Vector2(1,2)))
            {
		        destinationRectangle = new Rectangle((int)position.X, (int)position.Y, (int)(sourceRectangle.Width * scale.X), (int)(sourceRectangle.Height * scale.Y));
			    spriteBatch.Draw(texture, destinationRectangle, sourceRectangle, Color.White);
                isDrawn = true;
            }
            else
                isDrawn = false;
		}
	}
}