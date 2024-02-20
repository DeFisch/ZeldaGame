using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeldaGame.Items {
	public class ItemSpriteFactory {
		List<ISprite> objectList;
		Texture2D texture;
		Texture2D texture2;
		Vector2 pos;
		private int cycler = 0;
		private int index = 0;

		public ItemSpriteFactory(Texture2D texture, Texture2D texture2) {
			objectList = new List<ISprite>();
			this.texture = texture;
			this.texture2 = texture2;
			pos = new Vector2(300, 150);
		}

		public void ObjectList() {
			objectList.Add(new BlueRuby(this.texture, pos));
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
			objectList.Add(new FireItem(this.texture2, pos));
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

			objectList[index].Draw(spriteBatch, pos, Color.White);
		}

		public void Reset()
		{
            pos = new Vector2(300, 150);
            cycler = 0;
            index = 0;
        }
	}
}
