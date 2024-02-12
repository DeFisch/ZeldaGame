using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static ZeldaGame.Player.PlayerStateMachine;

public interface IProjectile
{
    public Direction GetDirection();

    public void Update();

    public void Draw(SpriteBatch spriteBatch, Vector2 location);
}
