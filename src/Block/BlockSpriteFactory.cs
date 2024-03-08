using System.Collections.Generic;
using Enemy;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ZeldaGame.Enemy;
using ZeldaGame.Map;
using ZeldaGame.Player;

namespace ZeldaGame.Block {
	public class BlockSpriteFactory {
		private Texture2D texture;
		private Vector2 position;
		private Vector2 scale;
		private Vector2 window_size;
		private Vector2 resetPosition;
		private PushableBlock pushableBlock1;
		private PushableBlock pushableBlock2;
		private List<PushableBlock> pushableBlockList;
		private LockedDoor lockedDoor;
		public BlockSpriteFactory(Texture2D texture, Vector2 scale, Vector2 window_size, IPlayer player, MapHandler map) {
			pushableBlockList = new List<PushableBlock>();
			this.texture = texture;
			this.scale = scale;
			this.pushableBlock1 = new PushableBlock(map,player, new Vector2(6,5));
			this.pushableBlock2 = new PushableBlock(map,player, new Vector2(7,5));
			this.lockedDoor = new LockedDoor(pushableBlock2,map,player, new Vector2(window_size.X/16,window_size.Y/11*5));
			this.window_size = window_size;
			position = new Vector2(200, 150);
			resetPosition = position;
		}

		public void AddBlock () 
		{
			pushableBlockList.Add(pushableBlock1);
			pushableBlockList.Add(pushableBlock2);
		}

		public List<PushableBlock> GetPushableBlocksList() => pushableBlockList;

		public void Draw(SpriteBatch spriteBatch) 
		{
			pushableBlock1.Draw(spriteBatch, texture, window_size, scale);
			pushableBlock2.Draw(spriteBatch, texture, window_size, scale);
			lockedDoor.Draw(spriteBatch, texture, scale);
		}

		public void Update()
		{
			pushableBlock1.Update();
			pushableBlock2.Update();
			lockedDoor.Update();
		}
		
		public void Reset()
		{
			position = resetPosition;
			pushableBlock1.Reset();
			pushableBlock2.Reset();
		}
	}
}
