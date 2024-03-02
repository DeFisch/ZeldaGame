using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ZeldaGame.Block;
using System.Diagnostics;
using ZeldaGame.Map;
using ZeldaGame.Player;
using ZeldaGame.Enemy;
using System.Collections.Generic;

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
                game.Link.Colliding(box);
            }
        }
    }

    public void UpdateProjectileCollision()
    {
        Dictionary<IPlayerProjectile, Rectangle> activeProjectiles = game.Link.GetProjectileHitBoxes();
        foreach (Rectangle box in game.map.getAllObjectRectangles())
        {
            foreach (IPlayerProjectile projectile in activeProjectiles.Keys)
            {
                if (activeProjectiles[projectile].Intersects(box))
                {
                    projectile.Collided();
                }
            }
        }
    }

    public void Update() {
        UpdateProjectileCollision();
        UpdatePlayerCollision();
        game.map.PlayerDoorCollision(new Vector2(game.windowSize.X, game.windowSize.Y), game.Link);
        game.NPCFactory.PlayerNPCCollision(game.Link);
        enemyCollisionHandler.Update();
    }
}