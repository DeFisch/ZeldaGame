using System;
using System.Collections.Generic;
using Enemy;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ZeldaGame.Items;
namespace ZeldaGame.Enemy;


public class EnemyFactory {
	private List<IEnemy> enemies;
	private List<IEnemy> dead_enemies = new List<IEnemy>();
	private Texture2D[] textures;
	private Vector2 scale;
	public int current_enemy = 0;
	public string[] enemy_types = new string[] { "Stalfos", "Gibdo", "Keese", "WizzRobe", "DarkNut", "Goriya", "Aquamentus" };
	public EnemyProjectileFactory enemyProjectileFactory;
	ItemSpriteFactory itemSpriteFactory;
	private Vector2 window_size;
	public EnemyFactory(Texture2D[] textures, Vector2 scale, Vector2 window_size, ItemSpriteFactory itemSpriteFactory) {
		enemies = new List<IEnemy>();
		this.itemSpriteFactory = itemSpriteFactory;
		this.window_size = window_size;
		this.textures = textures;
		enemyProjectileFactory = new EnemyProjectileFactory(textures);
		this.scale = scale;
	}

	// create new enemy based on enemy name
	public void AddEnemy(string enemy_name, string[,] map) {
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

	// overloaded method to add pre-defined enemies
	public void AddEnemy(IEnemy enemy) {
		enemies.Add(enemy);
	}

	public void RemoveDeadEnemies()
	{
		for (int i = enemies.Count - 1; i >= 0; i--)
		{
			if (enemies[i].GetHealth() <= 0)
			{
				if (new Random().Next(0, 5) != -1) // 20% chance to drop item
				{
					Point position = enemies[i].GetRectangle().Center;
					//translate pixel coordinate back to map coordinate
					double tile_width = window_size.X / 16.0;
					double tile_height = window_size.Y / 11.0;
					int x = (int)(position.X / tile_width) - 2;
					int y = (int)(position.Y / tile_height) - 2;
					string[] available_items = itemSpriteFactory.GetAvailableItems();
					itemSpriteFactory.AddItem(available_items[new Random().Next(0, available_items.Length)], new Vector2(x, y));
				}
				dead_enemies.Add(enemies[i]);
				enemies.RemoveAt(i);
			}
		}
	}

	public void ClearEnemies() {
		enemies = new List<IEnemy>();
		enemyProjectileFactory.ClearAllProjectiles();
	}
	public void Draw(SpriteBatch spriteBatch) {
		foreach (IEnemy enemy in enemies) {
			if (enemy != null)
				enemy.Draw(spriteBatch);
		}
		for (int i = dead_enemies.Count - 1; i >= 0; i--)
		{
			if (dead_enemies[i].IsFinished())
				dead_enemies.RemoveAt(i);
			else
				dead_enemies[i].Draw(spriteBatch);
		}
		enemyProjectileFactory.DrawProjectiles(spriteBatch);
	}
	public void Update() {
		foreach (IEnemy enemy in enemies) {
			if (enemy != null)
				enemy.Update();
		} foreach (IEnemy enemy in dead_enemies)
		{
			if (enemy != null)
				enemy.Update();
		}
		enemyProjectileFactory.UpdateProjectiles();
		RemoveDeadEnemies();
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