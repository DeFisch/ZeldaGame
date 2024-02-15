using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace ZeldaGame.Enemy;

public class EnemyFactory {
	private List<IEnemy> enemies;
	private Texture2D[] textures;
	private Vector2 window_size;
	public string[] enemy_types = new string[] { "Stalfos", "Gibdo", "Keese", "WizzRobe", "DarkNut" };

	public EnemyFactory(Texture2D[] textures, Vector2 window_size) {
		enemies = new List<IEnemy>();
		this.textures = textures;
		this.window_size = window_size;
	}

	public void AddEnemy(string enemy_name, string color_variation = null) {
		IEnemy enemy = null;
		switch (enemy_name) {
			case "Stalfos":
				enemy = new Stalfos(textures[0], window_size);
				break;
			case "Gibdo":
				enemy = new Gibdo(textures[0], window_size);
				break;
			case "Keese":
				enemy = new Keese(textures[0], window_size, color_variation);
				break;
			case "WizzRobe":
				enemy = new WizzRobe(textures[0], window_size, color_variation);
				break;
			case "DarkNut":
				enemy = new DarkNut(textures[0], window_size, color_variation);
				break;
			default:
				break;
		}
		enemies.Add(enemy);
	}

	public void ClearEnemies() {
		enemies.Clear();
	}
	public void Draw(SpriteBatch spriteBatch) {
		foreach (IEnemy enemy in enemies) {
			if (enemy != null)
				enemy.Draw(spriteBatch);
		}
	}
	public void Update() {
		foreach (IEnemy enemy in enemies) {
			if (enemy != null)
				enemy.Update();
		}
	}

	public void Reset()
	{
		ClearEnemies();
        for (int i = 0; i < 5; i++) 
        {
            AddEnemy("Stalfos");
        }
    }
}