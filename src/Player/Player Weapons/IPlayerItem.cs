using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

public interface IPlayerItem
{
    public void Update();

    public void Draw(SpriteBatch spriteBatch, Vector2 location);
}
