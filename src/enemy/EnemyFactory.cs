using System.Collections.Generic;
using Enemy;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace ZeldaGame.Enemy;

public class EnemyFactory {
	private List<IEnemy> enemies;
	private Texture2D[] textures;
	private Vector2 scale;
	public int current_enemy = 0;
	public string[] enemy_types = new string[] { "Stalfos", "Gibdo", "Keese", "WizzRobe", "DarkNut", "Goriya", "Aquamentus" };
	public EnemyProjectileFactory enemyProjectileFactory;
	public EnemyFactory(Texture2D[] textures, Vector2 scale) {
		enemies = new List<IEnemy>();
		this.textures = textures;
		enemyProjectileFactory = new EnemyProjectileFactory(textures);
		this.scale = scale;
	}

	public void AddEnemy(string enemy_name, Vector2 position, string color_variation = null) {
		IEnemy enemy = null;
		switch (enemy_name) {
			case "Stalfos":
				enemy = new Stalfos(textures[0], position, scale);
				break;
			case "Gibdo":
				enemy = new Gibdo(textures[0], position, scale);
				break;
			case "Keese":
				enemy = new Keese(textures[0], position, color_variation, scale);
				break;
			case "WizzRobe":
				enemy = new WizzRobe(textures[0], position, enemyProjectileFactory, color_variation, scale);
				break;
			case "DarkNut":
				enemy = new DarkNut(textures[0], position, color_variation, scale);
				break;
			case "Goriya":
				enemy = new Goriya(textures[0], position, enemyProjectileFactory, color_variation, scale);
				break;
			case "Aquamentus":
				if (enemies.Count == 0) // Only one Aquamentus allowed
					enemy = new Aquamentus(textures[1], position, enemyProjectileFactory, scale);
				break; 
			default:
				break;
		}
		enemies.Add(enemy);
	}

	public void ClearEnemies() {
		enemies.Clear();
		enemyProjectileFactory.ClearAllProjectiles();
	}
	public void Draw(SpriteBatch spriteBatch) {
		foreach (IEnemy enemy in enemies) {
			if (enemy != null)
				enemy.Draw(spriteBatch);
		}
		enemyProjectileFactory.DrawProjectiles(spriteBatch);
	}
	public void Update() {
		foreach (IEnemy enemy in enemies) {
			if (enemy != null)
				enemy.Update();
		}
		enemyProjectileFactory.UpdateProjectiles();
	}

	public void Reset()
	{
		ClearEnemies();
		AddEnemy("Stalfos", new Vector2(120, 120));
    }

	public List<IEnemy> GetAllEnemies() {
		return enemies;
	}

	public List<IEnemyProjectile> GetAllProjectiles() {
		return enemyProjectileFactory.GetProjectiles();
	}
}