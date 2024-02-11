using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using ZeldaGame.Items;

namespace ZeldaGame
{
    public class ObjectSpriteFactory
    {
        List<ISprite> objectList;
        BlueRuby blueRuby;
        YellowRuby yellowRuby;
        Texture2D texture;
        Vector2 pos;
        private int cycler = 0;
        private int index = 0;

        public ObjectSpriteFactory(Texture2D texture) {
           objectList = new List<ISprite>();
           this.texture = texture;
           this.pos = new Vector2(300, 150);
           blueRuby = new BlueRuby(this.texture, this.pos);
           yellowRuby = new YellowRuby(this.texture, this.pos);
        }

        public void ObjectList()
        {
            objectList.Add(blueRuby);
            objectList.Add(yellowRuby);

        }

        public void Cycle(int lastOrNext)
        {
            if (lastOrNext == 0) //Cycling backwards
            {
                cycler--;
                if (cycler < 0)
                {
                    cycler = objectList.Count - 1;
                }
                index = cycler % objectList.Count;
            }
            if (lastOrNext == 1)
            {
                cycler++;
                index = cycler % objectList.Count;
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            //blueRuby.Draw(spriteBatch, this.pos);
            objectList[index].Draw(spriteBatch, this.pos);
        }
    }
}
