using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

public class PlayerEnemy
{
    private EnemyPlayerSprite sprite;
    private Vector2 position;
    private Vector2 scale;

    private readonly float speed = 3f;

    public PlayerEnemy(Texture2D texture, Vector2 position, Vector2 scale)
    {
        this.position = position;
        this.scale = scale;
        sprite = new EnemyPlayerSprite(texture);
    }

    public Rectangle GetHitBox()
    {
        Rectangle hitbox = sprite.GetHitBox();
        hitbox.Inflate(-6, -8);
        return hitbox;
    }

    public void OnCollision(Rectangle collision)
    {
        Rectangle collisionOverlap = Rectangle.Intersect(GetHitBox(), collision);

        if (collisionOverlap.Width > collisionOverlap.Height)
        {
            if (collisionOverlap.Center.Y < GetHitBox().Center.Y)
            {
                position.Y += collisionOverlap.Height;
            }
            else
            {
                position.Y -= collisionOverlap.Height;
            }
        }
        else
        {
            if (collisionOverlap.Center.X < GetHitBox().Center.X)
            {
                position.X += collisionOverlap.Width;
            }
            else
            {
                position.X -= collisionOverlap.Width;
            }
        }
    }

    public void SetEnemyPlayerPosition(Vector2 position, bool offset = true)
    {
        Rectangle sprite_hitbox = sprite.GetHitBox();
        Vector2 sprite_size = new Vector2(sprite_hitbox.Width, sprite_hitbox.Height);
        if (offset) this.position = position - sprite_size / 2;
    }

    public void Move(Vector2 movement)
    {
        position += movement * speed;
    }

    public void Draw(SpriteBatch spriteBatch, Color color)
    {
        sprite.Draw(spriteBatch, position, color, scale);
    }

    public void Update()
    {
        sprite.Update();
    }
}