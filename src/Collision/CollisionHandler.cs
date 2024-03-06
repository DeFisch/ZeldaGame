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
    private readonly Game1 game;
    private readonly EnemyCollisionHandler enemyCollisionHandler;
    public CollisionHandler(Game1 game) {
        this.game = game;
        enemyCollisionHandler = new EnemyCollisionHandler(game);
    }

    public void PlayerMapCollision()
    {
        foreach (Rectangle box in game.map.getAllObjectRectangles())
        {
            if (game.Link.GetPlayerHitBox().Intersects(box))
            {
                game.Link.Colliding(box);
            }
        }
    }

    public void PlayerProjectileMapCollision()
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

    public void PlayerProjectileEnemyCollision()
    {
        Dictionary<IPlayerProjectile, Rectangle> activeProjectiles = game.Link.GetProjectileHitBoxes();
        List<IEnemy> enemies = game.enemyFactory.GetAllEnemies();
        List<IEnemy> shotEnemies = new();

        foreach (IEnemy enemy in enemies)
        {
            foreach (IPlayerProjectile projectile in activeProjectiles.Keys)
            {
                if (activeProjectiles[projectile].Intersects(enemy.GetRectangle()))
                {
                    projectile.Collided();
                    enemy.TakeDamage(projectile.ProjectileDamage());
                    shotEnemies.Add(enemy);
                }
            }
        }
    }

    public void EnemyProjectilePlayerCollision()
    {
        foreach (IEnemyProjectile projectile in game.enemyFactory.GetAllProjectiles())
        {
            if (projectile.GetRectangle().Intersects(game.Link.GetPlayerHitBox()))
            {
                game.Link = new HurtPlayer(game.Link, game);
            }
        }
    }

    public void Update() {
        PlayerProjectileEnemyCollision();
        PlayerProjectileMapCollision();
        PlayerMapCollision();
        EnemyProjectilePlayerCollision();
        game.map.PlayerDoorCollision(new Vector2(game.windowSize.X, game.windowSize.Y), game.Link);
        game.NPCFactory.PlayerNPCCollision(game.Link);
        enemyCollisionHandler.Update();
        game.blockSpriteFactory.Update();
    }
}