using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ZeldaGame.Items
{
    public class ItemSpriteFactory
    {
        List<ISprite> objectList;
        BlueRuby blueRuby;
        Texture2D texture;
        Vector2 pos;

        public ItemSpriteFactory(Texture2D texture)
        {
            objectList = new List<ISprite>();
            this.texture = texture;
            pos = new Vector2(300, 150);
            blueRuby = new BlueRuby(this.texture, pos);
        }

        public void ObjectList()
        {
            objectList.Add(blueRuby);

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            blueRuby.Draw(spriteBatch, pos);
        }
    }
}
