using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace ZeldaGame.Enemy;

public class EnemyFactory
{
    private List<IEnemy> enemies;
    private Texture2D texture;

    public EnemyFactory(Texture2D texture)
    {
        enemies = new List<IEnemy>();
        this.texture = texture;
    }

    public void AddEnemy(string enemy_name, Vector2 position)
    {
        IEnemy enemy = null;
        switch (enemy_name)
        {
            case "Stalfos":
                enemy = new Stalfos(texture, position);
                break;
            default:
                break;
        }
        enemies.Add(enemy);
    }

    public void ClearEnemies()
    {
        enemies.Clear();
    }
    public void Draw(SpriteBatch spriteBatch)
    {
        foreach (IEnemy enemy in enemies)
        {
            enemy.Draw(spriteBatch);
        }
    }
    public void Update()
    {
        foreach (IEnemy enemy in enemies)
        {
            enemy.Update();
        }
    }
}