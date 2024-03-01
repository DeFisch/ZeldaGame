using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace ZeldaGame.Enemy {
    public class EnemyCollisionHandler {
        private Game1 game;
        public EnemyCollisionHandler(Game1 game) {
            this.game = game;
        }

        public void Update() {
            EnemyFactory enemyFactory = game.enemyFactory;
            List<Rectangle> staticRectangles = game.map.getAllObjectRectangles();
            foreach (IEnemy enemy in enemyFactory.GetAllEnemies()) {
                foreach(Rectangle rectangle in staticRectangles) {
                    if(enemy.GetRectangle().Intersects(rectangle)) {
                        enemy.Collide(Rectangle.Intersect(enemy.GetRectangle(), rectangle));
                    }
                }
            }
        }
    }
}