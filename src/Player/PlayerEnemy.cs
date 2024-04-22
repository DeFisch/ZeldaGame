using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

public class PlayerEnemy
{
    private EnemyPlayerSprite sprite;
    private Vector2 position;
    private Vector2 resetPosition;
    private bool isActive;

    private readonly float speedPlayer = 3f;
    private readonly float speedAI = 1.5f;

    public PlayerEnemy(Texture2D texture, Vector2 position)
    {
        this.position = position;
        resetPosition = position;
        sprite = new EnemyPlayerSprite(texture);
    }

    public Rectangle GetHitBox()
    {
        if (isActive)
        {
            Rectangle hitbox = sprite.GetHitBox();
            hitbox.Inflate(-6, -8);
            return hitbox;
        } else
        {
            return new();   // Return no hitbox if not active
        }
    }

    public void OnCollision(Rectangle collision)
    {
        Rectangle collisionOverlap = Rectangle.Intersect(GetHitBox(), collision);

        if (collisionOverlap.Width > collisionOverlap.Height)
        {
            if (collisionOverlap.Center.Y < GetHitBox().Center.Y)
                position.Y += collisionOverlap.Height;
            else
                position.Y -= collisionOverlap.Height;
        }
        else
        {
            if (collisionOverlap.Center.X < GetHitBox().Center.X)
                position.X += collisionOverlap.Width;
            else
                position.X -= collisionOverlap.Width;
        }
    }

    public void SetEnemyPlayerPosition(Vector2 position, bool offset = true)
    {
        Rectangle sprite_hitbox = sprite.GetHitBox();
        Vector2 sprite_size = new Vector2(sprite_hitbox.Width, sprite_hitbox.Height);
        if (offset) this.position = position - sprite_size / 2;
    }

    // Player controlled movement
    public void Move(Vector2 movement)
    {
        position += movement * speedPlayer;
    }

    // AI controlled movement
    public void MoveAI(Vector2 targetPosition)
    {
        if (targetPosition.X > position.X)
            position.X += speedAI;
        else
            position.X -= speedAI;
        if (targetPosition.Y > position.Y)
            position.Y += speedAI;
        else
            position.Y -= speedAI;
    }

    public void Draw(SpriteBatch spriteBatch, Color color)
    {
        isActive = true;    // If being drawn, means it is active
        sprite.Draw(spriteBatch, position, color);
    }

    public void Update()
    {
        sprite.Update();
    }

    public void UpdateAI(Vector2 targetPosition)
    {
        sprite.Update();
        MoveAI(targetPosition);
    }

    public void Reset()
    {
        isActive = false;
        position = resetPosition;
    }
}