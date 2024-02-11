using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace ZeldaGame.Enemy;

public class EnemyFactory
{
    private List<IEnemy> enemies;
    private Texture2D texture;
    private Vector2 window_size;
    public string[] enemy_types = new string[] { "Stalfos", "Gibdo", "KeeseGoriya"};

    public EnemyFactory(Texture2D texture, Vector2 window_size)
    {
        enemies = new List<IEnemy>();
        this.texture = texture;
        this.window_size = window_size;
    }

    public void AddEnemy(string enemy_name, string color_variation = null)
    {
        IEnemy enemy = null;
        switch (enemy_name)
        {
            case "Stalfos":
                enemy = new Stalfos(texture, window_size);
                break;
            case "Gibdo":
                enemy = new Gibdo(texture, window_size);
                break;
            case "KeeseGoriya":
                enemy = new KeeseGoriya(texture, window_size, color_variation);
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
            if(enemy != null)
                enemy.Draw(spriteBatch);
        }
    }
    public void Update()
    {
        foreach (IEnemy enemy in enemies)
        {
            if(enemy != null)
                enemy.Update();
        }
    }
}