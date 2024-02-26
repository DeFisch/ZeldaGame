using Microsoft.Xna.Framework;

namespace ZeldaGame.Enemy.Commands {
	public class nextEnemyCommand : ICommand {
		private Game1 MyGame;
		private string[] enemy_types;
		private int max_enemies = 1;
		// Constructor
		public nextEnemyCommand(Game1 myGame) {
			MyGame = myGame;
			enemy_types = MyGame.enemyFactory.enemy_types;
		}

		public void Execute() {
			MyGame.enemyFactory.ClearEnemies();
			MyGame.enemyFactory.current_enemy = (MyGame.enemyFactory.current_enemy + 1) % enemy_types.Length;
			for (int i = 0; i < max_enemies; i++) {
				string color_variation = i % 2 == 0 ? "red" : "blue";
				MyGame.enemyFactory.AddEnemy(enemy_types[MyGame.enemyFactory.current_enemy], new Vector2(120, 120), color_variation);
			}
		}
	}
}