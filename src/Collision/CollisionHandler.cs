using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ZeldaGame.Block;
using System.Diagnostics;
using ZeldaGame.Map;
using ZeldaGame.Player;
using ZeldaGame.Enemy;

namespace ZeldaGame;

public class CollisionHandler {
    private Game1 game;
    EnemyCollisionHandler enemyCollisionHandler;
    public CollisionHandler(Game1 game) {
        this.game = game;
        enemyCollisionHandler = new EnemyCollisionHandler(game);
    }

    public void UpdatePlayerCollision()
    {
        foreach (Rectangle box in game.map.getAllObjectRectangles())
        {
            if (game.Link.GetPlayerHitBox().Intersects(box))
            {
                Debug.WriteLine("Colliding");
                game.Link.Colliding(box);
            }
        }
    }

    public void Update() {
        UpdatePlayerCollision();
        game.map.PlayerDoorCollision(new Vector2(game.windowSize.X, game.windowSize.Y), game.Link);
        game.NPCFactory.PlayerNPCCollision(game.Link);
        enemyCollisionHandler.Update();
    }
}