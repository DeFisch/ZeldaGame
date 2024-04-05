using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace ZeldaGame.Items
{
    public interface IItemSprite : ICollidible, ISprite
    {
        new void Update();
        new void Draw(SpriteBatch spriteBatch, Vector2 location, Color color, Vector2 scale);
        new public Rectangle GetHitBox();

        public String GetID();

        new public void OnCollision(Rectangle intersect); //Dictates what should happen to Link after collision
    }
}
