using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint0.Block {
	public class BlockSpriteFactory {
		private List<IBlock> blockList;
		private Texture2D texture;
		private Vector2 position;
		private int cycleIndex;
		private int listLength;
		private Vector2 resetPosition;

		public BlockSpriteFactory(Texture2D texture) {
			blockList = new List<IBlock>();
			this.texture = texture;
			position = new Vector2(200, 150);
			cycleIndex = 0;
			listLength = 0;
			resetPosition = position;
		}

		public void AddBlock () 
		{
            blockList.Add(new Stair(texture, position));
            blockList.Add(new Walls(texture, position));
            blockList.Add(new Ground(texture, position));
            blockList.Add(new Obstacle(texture, position));
            blockList.Add(new Water(texture, position));
            blockList.Add(new Sand(texture, position));
		}
		

		public void cycleList(int cycleDirection) {
			listLength = blockList.Count;
			if (cycleDirection == 1 ) {
				cycleIndex = (cycleIndex + 1) % listLength;
			}
			else if (cycleDirection == 0) {
				cycleIndex = (cycleIndex - 1 + listLength) % listLength;
			}
		}

		public void Draw(SpriteBatch spriteBatch) {
            blockList[cycleIndex].Draw(spriteBatch);
		}

		public void Reset()
		{
			position = resetPosition;
			cycleIndex = 0;
			listLength = 0;
		}
	}
}
