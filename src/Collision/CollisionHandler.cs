using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint0.Block;
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
    }

    public void UpdatePlayerCollision()
    {
        foreach (Rectangle box in game.map.getAllObjectRectangles())
        {
            if (game.Link.GetPlayerHitBox().Intersects(box))
            {
                game.Link.Colliding();
            }
        }
        
    }

    public void Update() {
        UpdatePlayerCollision();
        game.map.PlayerDoorCollision(new Vector2(game.window_width, game.window_height), game.Link);
        game.NPCFactory.PlayerNPCCollision(game.Link);
        enemyCollisionHandler.Update();
    }
}