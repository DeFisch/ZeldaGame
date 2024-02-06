using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace ZeldaGame.Player;

public class EnemyFactory {
	private List<IPlayer> players;
	private Texture2D texture;
	private Vector2 window_size;

	public EnemyFactory(Texture2D texture, Vector2 window_size) {
		players = new List<IPlayer>();
		this.texture = texture;
		this.window_size = window_size;
	}

	public void AddEnemy(string player_name, string color_variation = null) {
		IPlayer player = null;
		switch (player_name) {
			case "Player1":
				player = new Player1(texture, window_size);
				break;
			default:
				break;
		}
		players.Add(player);
	}

	public void ClearEnemies() {
		players.Clear();
	}
	public void Draw(SpriteBatch spriteBatch) {
		foreach (IPlayer player in players) {
			player.Draw(spriteBatch);
		}
	}
	public void Update() {
		foreach (IPlayer player in players) {
			player.Update();
		}
	}
}