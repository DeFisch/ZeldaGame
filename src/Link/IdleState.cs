using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ZeldaGame;

public class IdleState : ISprite
{
    private Game1.State state;
    private Game1.Direction direction;
    private Game1.Health health;

    public IdleState(Game1.Direction direction, Game1.Health health)
    {
        this.state = Game1.State.Idle;
        this.direction = direction;
        this.health = health;
    }

    public Game1.Direction Direction()
    {
        return this.direction;
    }

    public Game1.Health Health()
    {
        return this.health;
    }

    public void Update()
    {
        // No implementation
    }

    public void Draw(SpriteBatch spriteBatch, Vector2 location)
    {
        // Draw idle sprite depending on direction of sprite
    }
}
