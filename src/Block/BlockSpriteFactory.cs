using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Sprint0.Block
{
    public class BlockSpriteFactory
    {
        private List<IBlock> blockList;
        private Texture2D texture;
        private Vector2 position;
        private IBlock block;
        int cycleIndex = 0;

        public BlockSpriteFactory(Texture2D texture)
        {
            blockList = new List<IBlock>();
            this.texture = texture; 
            position = new Vector2(200, 150);
            block = null;
        }

        public void AddBlocks(String blockType)
        {
            switch (blockType)
            {
                case "Stair":
                    block = new Stair(texture, position);
                    break;

                case "Walls":
                    block = new Walls(texture, position); 
                    break;

                default:
                    break;
            }
            blockList.Add(block);
        }

        public void cycleList()
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            block = blockList[cycleIndex];
            block.Draw(spriteBatch);
        }
    }
}
