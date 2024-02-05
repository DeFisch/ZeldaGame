using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class AttackingState : ILinkState, ISprite
{
    public void Direction()
    {
        // Get current direction
    }

    public void Health()
    {
        // Get current health
    }

    public void Interact()
    {
        // Animate and make an attack based off current direction
    }

    public void Draw(SpriteBatch spriteBatch, Vector2 location)
    {
        throw new NotImplementedException();
    }

    public void Update()
    {
        throw new NotImplementedException();
    }
}

