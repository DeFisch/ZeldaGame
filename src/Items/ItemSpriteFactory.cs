using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeldaGame.Map;
using ZeldaGame.Player;
using static ZeldaGame.Game1;

namespace ZeldaGame.Items {
	public class ItemSpriteFactory {
		List<IItemSprite> objectList;
		Texture2D texture;
		Texture2D texture2;
		Player1 player;
		Vector2 pos;
		Vector2 scale;
		private int cycler = 0;
		private int index = 0;
		BlueRuby blueRuby;
		private int[]collCheck;
        private string [,] map;
		private MapHandler mapHandler;

        public ItemSpriteFactory(Texture2D texture, Texture2D texture2, Vector2 scale, IPlayer player, MapHandler mapHandler) {
			objectList = new List<IItemSprite>();
			this.texture = texture;
			this.texture2 = texture2;
			this.player = (Player1)player;
			pos = new Vector2(300, 150);
			this.scale = scale;
			this.mapHandler = mapHandler;
			blueRuby = new BlueRuby(this.texture, pos);
		}

		public void GetMapItems()
		{
			map = mapHandler.get_map_info();
			string item_char;
            for (int i = 0; i < map.GetLength(0); i++) //Rows
            {
                for (int j = 0; j < map.GetLength(1); j++) //Columns
                {
					item_char = map[i, j];
					pos = new Vector2(j, i);
					switch (item_char)
					{
						case "br":
                            objectList.Add(new BlueRuby(this.texture, pos));
							break;
						case "yr":
							objectList.Add(new YellowRuby(this.texture, pos));
							break;
						case "k":
							objectList.Add(new Key(this.texture, pos));
							break;
						case "cl":
                            objectList.Add(new Clock(this.texture, pos));
                            break;
						case "co":
							objectList.Add(new Compass(this.texture, pos));
                            break;
						case "h":
							objectList.Add(new Heart(this.texture, pos));
                            break;
                        case "hc":
                            objectList.Add(new HeartContainer(this.texture, pos));
                            break;
                        case "tr":
                            objectList.Add(new Triforce(this.texture, pos));
                            break;
                        default:
							break;
					}
                }
            }
			collCheck = new int[objectList.Count];
        }
		public void ObjectList() {
			//objectList.Add(new BlueRuby(this.texture, pos));
			/*objectList.Add(blueRuby);
			objectList.Add(new YellowRuby(this.texture, pos));
			objectList.Add(new Heart(this.texture, pos));
			objectList.Add(new Triforce(this.texture, pos));
			objectList.Add(new HeartContainer(this.texture, pos));
			objectList.Add(new Compass(this.texture, pos));
			objectList.Add(new Map(this.texture, pos));
			objectList.Add(new Key(this.texture, pos));
			objectList.Add(new Clock(this.texture, pos));
			objectList.Add(new Boomerang(this.texture, pos));
			objectList.Add(new Bow(this.texture, pos));
			objectList.Add(new Bomb(this.texture, pos));
			objectList.Add(new FairyItem(this.texture, pos));
			objectList.Add(new FireItem(this.texture2, pos));*/
		}

		public void Cycle(int lastOrNext) {
			if (lastOrNext == 0) //Cycling backwards
			{
				collCheck[index] = 0;
				cycler--;
				if (cycler < 0) {
					cycler = objectList.Count - 1;
				}
				index = cycler % objectList.Count;
			}
			if (lastOrNext == 1) {
				collCheck[index] = 0;
                cycler++;
				index = cycler % objectList.Count;
			}
		}
		public void Draw(SpriteBatch spriteBatch) {
			for (int k = 0; k < objectList.Count; k++)
			{
				Rectangle itemdest = objectList[k].GetHitBox();
				Rectangle playrect = player.GetPlayerHitBox();
				if (itemdest.Intersects(playrect) || collCheck[k] == 1)
				{
					collCheck[k] = 1;
				}
				else
				{
					objectList[k].Draw(spriteBatch, pos, Color.White, scale);
				}
			}
        }//GetPlayerHitBox

		public void Reset()
		{
            pos = new Vector2(300, 150);
            cycler = 0;
            index = 0;
        }
	}
}
