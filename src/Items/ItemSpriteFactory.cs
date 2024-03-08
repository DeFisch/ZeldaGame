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
		private int k = 0;
		private List<int> collCheck;
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
			collCheck = new List<int>(objectList.Count);

            for (int l = 0; l < 7; l++)
            {
                for (int m = 0; m < 6; m++)
                {
                    arrayOfLists[l, m] = new List<IItemSprite>();
                }
            }
        }

		public string[] GetAvailableItems()
		{
			return new string[] { "br", "yr", "cl", "h" };
		}

		public void AddItem(string item_char, Vector2 pos) {
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
				case "bo":
					objectList.Add(new Bow(this.texture, pos));
					break;
				default:
					break;
			}
			collCheck.Add(0);
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
						AddItem(item_char, pos);
					}
				}
				arrayOfLists[initx, inity] = objectList;
            }
			switchCheck = 0;
        }

		public bool ItemCollisionHandler()
		{
			Rectangle itemdest = objectList[k].GetHitBox();
            Rectangle playrect = player.GetPlayerHitBox();
            if (playrect.Contains(itemdest.Center) || collCheck[k] == 1)
            {
                collCheck[k] = 1;
                objectList.RemoveAt(k);
                collCheck.RemoveAt(k);
                objectList.Insert(k, new BlankItem(this.texture, this.pos));
				return true;
            }
			return false;
        }
		public void Draw(SpriteBatch spriteBatch) {
			int count = objectList.Count;
			if (switchCheck == 0)
			{
				for (k = 0; k < count; k++)
				{
					bool check = ItemCollisionHandler();
					if (check == false) { 
						objectList[k].Draw(spriteBatch, pos, Color.White, scale);
					}
				}
			}
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

        public void Reset()
        {
            pos = new Vector2(300, 150);
            for (int l = 0; l < 7; l++)
                for (int m = 0; m < 6; m++)
                    arrayOfLists[l, m] = new List<IItemSprite>();
            objectList.Clear();
            collCheck = new List<int>(objectList.Count);
            GetMapItems();
        }
    }
}
