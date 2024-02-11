
using ZeldaGame;
public class previousEnemyCommand : ICommand {
	private Game1 MyGame;
	private string[] enemy_types = new string[] { "Stalfos", "Gibdo", "KeeseGoriya"};
	private int current_enemy = 0;
	private int max_enemies = 5;
	// Constructor
	public previousEnemyCommand(Game1 myGame) {
		MyGame = myGame;
		//enemy_types = MyGame.enemyFactory.enemy_types;
	}

	public void Execute() {
		//MyGame.enemyFactory.ClearEnemies();
		current_enemy = current_enemy - 1 < 0 ? enemy_types.Length - 1 : current_enemy - 1;
		for(int i = 0; i < max_enemies; i++){
			//MyGame.enemyFactory.AddEnemy(enemy_types[current_enemy]);
		}
	}
}
