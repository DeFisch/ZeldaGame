

using Microsoft.Xna.Framework;

namespace ZeldaGame.Enemy.Commands {
	public class previousEnemyCommand : ICommand {
		private Game1 MyGame;
		private string[] enemy_types;
		private int max_enemies = 1;
		// Constructor
		public previousEnemyCommand(Game1 myGame) {
			MyGame = myGame;
			enemy_types = MyGame.enemyFactory.enemy_types;
		}

		public void Execute() {
			MyGame.enemyFactory.ClearEnemies();
			MyGame.enemyFactory.current_enemy = MyGame.enemyFactory.current_enemy - 1 < 0 ? enemy_types.Length - 1 : MyGame.enemyFactory.current_enemy - 1;
			for (int i = 0; i < max_enemies; i++) {
				string color_variation = i % 2 == 0 ? "red" : "blue";
				MyGame.enemyFactory.AddEnemy(enemy_types[MyGame.enemyFactory.current_enemy], new Vector2(120, 120), color_variation: color_variation);
			}
		}
	}
}