using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Diagnostics;
using ZeldaGame.Player;
using static ZeldaGame.Globals;

public class SwordProjectileExplosion : IPlayerProjectile
{
    private readonly List<SpriteEffects> effects = new() { SpriteEffects.None, SpriteEffects.FlipHorizontally, SpriteEffects.FlipVertically, SpriteEffects.FlipVertically | SpriteEffects.FlipHorizontally };
    private Texture2D texture;

    private List<Vector2> positions;
    private Vector2 positionTL;
    private Vector2 positionTR;
    private Vector2 positionBL;
    private Vector2 positionBR;

    private int hitBoxReturnIndex;
    private readonly int projectileSpeed = 3;
    private readonly int damage = 3;
    private bool isActive;

    private List<Rectangle> destinationRectangles;
    private Rectangle destinationRectangleTL;
    private Rectangle destinationRectangleTR;
    private Rectangle destinationRectangleBL;
    private Rectangle destinationRectangleBR;
    private Rectangle sourceRectangle;

    public SwordProjectileExplosion(Texture2D texture, Vector2 position)
    {
        this.texture = texture;
        hitBoxReturnIndex = 0;
        destinationRectangleTL = new();
        destinationRectangleTR = new();
        destinationRectangleBL = new();
        destinationRectangleBR = new();
        destinationRectangles = new() { destinationRectangleTL, destinationRectangleTR, destinationRectangleBL, destinationRectangleBR };
        positions = new() { positionTL, positionTR, positionBL, positionBR };
        for (int i = 0; i < positions.Count; i++)
            positions[i] = position;
        isActive = true;
    }

    public Rectangle GetHitBox()
    {
        return destinationRectangles[hitBoxReturnIndex];
    }

    public bool IsActive()
    {
        return isActive;
    }

    public int ProjectileDamage()
    {
        return damage;
    }

    public void Draw(SpriteBatch spriteBatch, Vector2 scale)
    {
        sourceRectangle = new Rectangle(27, 154, 8, 16);
        for (int i = 0; i < destinationRectangles.Count; i++)
        {
            destinationRectangles[i] = new Rectangle((int)positions[i].X, (int)positions[i].Y, (int)(sourceRectangle.Width * scale.X), (int)(sourceRectangle.Height * scale.Y));
            spriteBatch.Draw(texture, destinationRectangles[i], sourceRectangle, Color.White, rotation: 0, new Vector2(0, 0), effects: effects[i], 1);
        }
    }

    public void Update()
    {
        positions[0] += new Vector2(-1, -1) * projectileSpeed;
        positions[1] += new Vector2(1, -1) * projectileSpeed;
        positions[2] += new Vector2(-1, 1) * projectileSpeed;
        positions[3] += new Vector2(1, 1) * projectileSpeed;

        hitBoxReturnIndex++;
        if (hitBoxReturnIndex == 4)
            hitBoxReturnIndex = 0;
    }

    public bool HasCollided()
    {
        throw new System.NotImplementedException();
    }

    public void Collided()
    {
        // No implementation
    }

    public Direction GetDirection()
    {
        throw new System.NotImplementedException();
    }
}