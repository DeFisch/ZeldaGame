using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ZeldaGame.Items {
	public class ItemSpriteFactory {
		List<ISprite> objectList;
		YellowRuby yellowRuby;
		BlueRuby blueRuby;
		Heart heart;
		HeartContainer heartContainer;
		Triforce triforce;
		Compass compass;
		Map map;
		Key key;
		Clock clock;
		Boomerang boomerang;
		Bow bow;
		Bomb bomb;

		Texture2D texture;
		Vector2 pos;
		private int cycler = 0;
		private int index = 0;

		public ItemSpriteFactory(Texture2D texture) {
			objectList = new List<ISprite>();
			this.texture = texture;
			pos = new Vector2(300, 150);
			yellowRuby = new YellowRuby(this.texture, pos);
			blueRuby = new BlueRuby(this.texture, pos);
			heart = new Heart(this.texture, pos);
			triforce = new Triforce(this.texture, pos);
			heartContainer = new HeartContainer(this.texture, pos);
			compass = new Compass(this.texture, pos);
			map = new Map(this.texture, pos);
			key = new Key(this.texture, pos);
			clock = new Clock(this.texture, pos);
			boomerang = new Boomerang(this.texture, pos);
			bow = new Bow(this.texture, pos);
			bomb = new Bomb(this.texture, pos);
		}

		public void ObjectList() {
			objectList.Add(blueRuby);
			objectList.Add(yellowRuby);
			objectList.Add(heart);
			objectList.Add(triforce);
			objectList.Add(heartContainer);
			objectList.Add(compass);
			objectList.Add(map);
			objectList.Add(key);
			objectList.Add(clock);
			objectList.Add(boomerang);
			objectList.Add(bow);
			objectList.Add(bomb);
		}

		public void Cycle(int lastOrNext) {
			if (lastOrNext == 0) //Cycling backwards
			{
				cycler--;
				if (cycler < 0) {
					cycler = objectList.Count - 1;
				}
				index = cycler % objectList.Count;
			}
			if (lastOrNext == 1) {
				cycler++;
				index = cycler % objectList.Count;
			}
		}
		public void Draw(SpriteBatch spriteBatch) {

			objectList[index].Draw(spriteBatch, pos);
		}

		public void Reset()
		{
            pos = new Vector2(300, 150);
            cycler = 0;
            index = 0;
        }
	}
}
