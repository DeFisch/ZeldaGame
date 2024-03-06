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
		private int[]collCheck;
		private int switchCheck = 0;
        private string [,] map;
		private MapHandler mapHandler;
        private string item_char;
		private int initx;
		private int inity;
		List<IItemSprite>[,] arrayOfLists;

        public ItemSpriteFactory(Texture2D texture, Texture2D texture2, Vector2 scale, IPlayer player, MapHandler mapHandler) {
			objectList = new List<IItemSprite>();
			this.texture = texture;
			this.texture2 = texture2;
			this.player = (Player1)player;
			pos = new Vector2(300, 150);
			this.scale = scale;
			this.mapHandler = mapHandler;
            arrayOfLists = new List<IItemSprite>[7, 6];

            for (int l = 0; l < 7; l++)
            {
                for (int m = 0; m < 6; m++)
                {
                    arrayOfLists[l, m] = new List<IItemSprite>();
                }
            }
        }

		public void GetMapItems()
		{
			map = mapHandler.get_map_info();
			initx = mapHandler.x;
			inity = mapHandler.y;
			if (arrayOfLists[initx, inity].Count() != 0)
			{
				objectList = arrayOfLists[initx, inity];

			}
			else
			{
				//string item_char;
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
				arrayOfLists[initx, inity] = objectList;
            }
			collCheck = new int[objectList.Count];
			switchCheck = 0;
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
			int count = objectList.Count;
			if (switchCheck == 0)
			{
				for (int k = 0; k < count; k++)
				{
					Rectangle itemdest = objectList[k].GetHitBox();
					Rectangle playrect = player.GetPlayerHitBox();
					if (itemdest.Intersects(playrect) || collCheck[k] == 1)
					{
						collCheck[k] = 1;
						objectList.RemoveAt(k);
						objectList.Insert(k, new BlankItem(this.texture, this.pos));
					}
					else
					{
						objectList[k].Draw(spriteBatch, pos, Color.White, scale);
					}
				}
			}
        }

		public void Reset()
		{
            pos = new Vector2(300, 150);
            cycler = 0;
            index = 0;
        }

		public bool IsMapChanged()
		{
			if (mapHandler.x != initx || mapHandler.y != inity)
			{
				switchCheck = 1;
				arrayOfLists[initx, inity] = new List<IItemSprite>(objectList);
				objectList.Clear();
				GetMapItems();
				return true;
            }
			else
			{
                arrayOfLists[initx, inity] = objectList;
                return false;
            }
		}
	}
}
