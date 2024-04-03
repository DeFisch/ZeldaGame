using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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
