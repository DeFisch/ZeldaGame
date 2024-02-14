using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint0.Block {
	public class BlockSpriteFactory {
		private List<IBlock> blockList;
		private Texture2D texture;
		private Vector2 position;
		private IBlock block;
		private int cycleIndex;
		private int listLength;

		public BlockSpriteFactory(Texture2D texture) {
			blockList = new List<IBlock>();
			this.texture = texture;
			position = new Vector2(200, 150);
			block = null;
			cycleIndex = 0;
			listLength = 0;
		}

		public void AddToList(string blockType) {
			switch (blockType) {
				case "Stair":
					block = new Stair(texture, position);
					break;

				case "Walls":
					block = new Walls(texture, position);
					break;

				case "Ground":
					block = new Ground(texture, position);
					break;

				case "Obstacle":
					block = new Obstacle(texture, position);
					break;

				default:
					break;
			}
			blockList.Add(block);
		}

		public void AddBlock () 
		{
			AddToList("Stair");
			AddToList("Walls");
			AddToList("Ground");
			AddToList("Obstacle");
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
			block = blockList[cycleIndex];
			block.Draw(spriteBatch);
		}
	}
}
