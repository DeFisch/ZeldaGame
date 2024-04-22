using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace ZeldaGame.Enemy.Projectiles;
public interface IEnemyProjectile
{
    string GetDirection();
    bool Completed();
    void Collided();
    float DoDamage();
    void Update();
    void Draw(SpriteBatch spriteBatch);
    Rectangle GetRectangle();
}
