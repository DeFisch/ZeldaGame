using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
public interface IEnemyProjectile
{
    bool Completed();
    void Update();
    void Draw(SpriteBatch spriteBatch);
    Rectangle GetRectangle();
}
