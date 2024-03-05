using System;
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

	public void AddEnemy(string enemy_name, string[,] map, Vector2 window_size) {
		IEnemy enemy = null;
		List<Vector2> available_locations = new List<Vector2>();
		float width = window_size.X / 16;
		float height = window_size.Y / 11;
		for (int i = 0; i < map.GetLength(0); i++) {
			for (int j = 0; j < map.GetLength(1); j++) {
				if (map[i, j] == "-") {
					available_locations.Add(new Vector2(j, i));
				}
			}
		}
		Vector2 position = available_locations[new Random().Next(0, available_locations.Count)];
		if (enemy_name == "Aquamentus") {
			position.X = 7;
			position.Y = 2.5f;
		}
		position.X = position.X * width + width * 2;
		position.Y = position.Y * height + height * 2;
		string color_variation = new Random().Next(0, 2) == 0 ? "blue" : "red";
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
				return;
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
    }

	public List<IEnemy> GetAllEnemies() {
		return enemies;
	}

	public List<IEnemyProjectile> GetAllProjectiles() {
		return enemyProjectileFactory.GetProjectiles();
	}
}