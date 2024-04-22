using Microsoft.Xna.Framework;
using ZeldaGame.Block;
using System.Diagnostics;
using ZeldaGame.Map;
using ZeldaGame.Player;
using ZeldaGame.Enemy;
using System.Collections.Generic;
using ZeldaGame.Items;
using ZeldaGame.NPCs;
using System.Linq;
using static ZeldaGame.Globals;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace ZeldaGame;
public class CollisionHandler {
    private readonly Game1 game;
    private readonly EnemyCollisionHandler enemyCollisionHandler;
    public ItemActionHandler itemActionHandler;
    public CollisionHandler(Game1 game) {
        this.game = game;
        enemyCollisionHandler = new EnemyCollisionHandler(game);
        itemActionHandler = new ItemActionHandler(game);
    }

    private void PlayerMapCollision()
    {
        foreach (Rectangle box in game.map.getAllObjectRectangles())
        {
            if (game.Link.GetHitBox().Intersects(box))
            {
                game.Link.OnCollision(box);
            }
        }
    }

    private void PlayerProjectileMapCollision()
    {
        Dictionary<IPlayerProjectile, Rectangle> activeProjectiles = game.Link.GetProjectileHitBoxes();
        foreach (Rectangle box in game.map.getAllObjectRectangles(includeWater: false))
        {
            foreach (IPlayerProjectile projectile in activeProjectiles.Keys)
            {
                if (projectile.GetType() == typeof(BoomerangSprite) || projectile.GetType() == typeof(BlueBoomerangSprite)
                    || projectile.GetType() == typeof(FireballSprite)
                    || ((projectile.GetType() == typeof(WoodSwordHorizontalProjectileSprite)
                    || projectile.GetType() == typeof(WoodSwordVerticalProjectileSprite)) && projectile.HasCollided()))
                    continue; // Boomerang and flame can pass through walls
                {
                    if (activeProjectiles[projectile].Intersects(box))
                    {
                        projectile.Collided();
                    }
                }
            }
        }
        // Check if projectile collides with window boundaries
        foreach (IPlayerProjectile projectile in activeProjectiles.Keys)
        {
            if (activeProjectiles[projectile].Width < -15)
                continue;
            if (activeProjectiles[projectile].X < 0 || activeProjectiles[projectile].X > game.windowSize.X || activeProjectiles[projectile].Y < 150 || activeProjectiles[projectile].Y > game.windowSize.Y)
            {
                projectile.Collided();
            }
        }
    }

    private void PlayerBombCollision()
    {
        if (game.Link.isHurting())
            return;
        Dictionary<IPlayerProjectile, Rectangle> activeProjectiles = game.Link.GetProjectileHitBoxes();
        foreach (IPlayerProjectile projectile in activeProjectiles.Keys)
        {
            if (projectile.GetType() == typeof(BombSprite))
            {
                if (activeProjectiles[projectile].Intersects(game.Link.GetHitBox()))
                {
                    game.Link.TakeDamage(projectile.ProjectileDamage());
                    game.Link = new PlayerHurt(game.Link, game);
                    Globals.audioLoader.Play("LOZ_Link_Hurt");
                    projectile.Collided();
                }
            }
        }
    }

    private void PlayerProjectileEnemyCollision()
    {
        Dictionary<IPlayerProjectile, Rectangle> activeProjectiles = game.Link.GetProjectileHitBoxes();
        List<IEnemy> enemies = game.enemyFactory.GetAllEnemies();
        List<IEnemy> shotEnemies = new();
        foreach (IEnemy enemy in enemies)
        {
            foreach (IPlayerProjectile projectile in activeProjectiles.Keys)
            {
                if (activeProjectiles[projectile].Intersects(enemy.GetHitBox()))
                {
                    // Keese can only be damaged by boomerang
                    if (enemy.GetType() == typeof(Keese) && projectile.GetType() != typeof(BoomerangSprite) && projectile.GetType() != typeof(BlueBoomerangSprite))
                        continue;
                    // If not colliding with sword explosion, do Collided()
                    if (!((projectile.GetType() == typeof(WoodSwordHorizontalProjectileSprite)
                    || projectile.GetType() == typeof(WoodSwordVerticalProjectileSprite)) && projectile.HasCollided()))
                        projectile.Collided();
                    enemy.Knockback(projectile.GetDirection());
                    if (enemy.TakeDamage(projectile.ProjectileDamage()))
                        Globals.audioLoader.Play("LOZ_Enemy_Hit");
                    shotEnemies.Add(enemy);
                }
            }
        }
    }

    private void EnemyPlayerCollision() {
        foreach (IEnemy enemy in game.enemyFactory.GetAllEnemies()) {
            if (enemy.GetHitBox().Intersects(game.Link.GetHitBox()) && !game.Link.isHurting()) {
                Globals.audioLoader.Play("LOZ_Link_Hurt");
                game.Link.OnCollision(enemy.GetHitBox());
                game.Link.TakeDamage(enemy.DoDamage());
                game.Link.Knockback();
                game.Link = new PlayerHurt(game.Link, game);
            }
        }
    }

    private bool EnemyProjectileShieldCollision(IEnemyProjectile projectile) {
        string projDirection = projectile.GetDirection();
        string playerDirection = game.Link.GetDirection().ToString();
        bool isShielded = false;

        if (game.Link.IsIdle() == true && projDirection.Equals(playerDirection) == true) {
            game.enemyFactory.enemyProjectileFactory.ClearProjectile(projectile);
            isShielded = true;
        }
        projectile.Collided();
        return isShielded;
    }

    private void EnemyProjectilePlayerCollision() {
        List<IEnemyProjectile> projectiles = game.enemyFactory.GetAllProjectiles();
        for (int i = projectiles.Count - 1; i >= 0; i--)
        {
            if (projectiles[i].GetRectangle().Intersects(game.Link.GetHitBox()) && !game.Link.isHurting())
            {
                if (EnemyProjectileShieldCollision(projectiles[i]) == false) {

                    Globals.audioLoader.Play("LOZ_Link_Hurt");
                    game.Link.OnCollision(projectiles[i].GetRectangle());
                    game.Link.TakeDamage(projectiles[i].DoDamage());
                    game.Link.Knockback();
                    game.Link = new PlayerHurt(game.Link, game);
                }
            }
        }
    }

    private void EnemyPlayerToPlayerCollision()
    {
        if (game.Link.GetHitBox().Intersects(game.playerEnemy.GetHitBox()) && !game.Link.isHurting())
        {
            game.Link.OnCollision(game.playerEnemy.GetHitBox());
            game.Link.Knockback();
            game.playerEnemy.OnCollision(game.Link.GetHitBox());
        }
    }

    private void ItemPlayerCollision()
    {
        foreach (IItemSprite item in game.itemFactory.GetAllItems().ToList())
        {
            if (item.GetHitBox().Intersects(game.Link.GetHitBox()))
            {
                itemActionHandler.InventoryCounts(item);

                if (game.NPCFactory.IsInNPCRoom(game.map.getMapXY()) && ItemActionHandler.inventoryCounts[1] < 2 && item is Key)
                {
                    Debug.WriteLine("Cannot buy Key as you don't have enough rupees.");
                }
                else if (game.NPCFactory.IsInNPCRoom(game.map.getMapXY()) && ItemActionHandler.inventoryCounts[1] < 5 && item is LifePotion)
                {
                    Debug.WriteLine("Cannot buy Life Potion as you don't have enough rupees.");
                }
                else
                {
                    switch (item)
                    {
                        case Key keyItem when game.NPCFactory.IsInNPCRoom(game.map.getMapXY()) && ItemActionHandler.inventoryCounts[1] >= 2:
                            ItemActionHandler.inventoryCounts[1] -= 2;
                            ItemActionHandler.inventoryCounts[2] += 0;
                            audioLoader.Play("LOZ_Get_Item");
                            game.itemFactory.RemoveItem(keyItem);
                            Debug.WriteLine("Bought 1 key using 2 rupees.");
                            break;

                        case LifePotion lifePotion when game.NPCFactory.IsInNPCRoom(game.map.getMapXY()) && ItemActionHandler.inventoryCounts[1] >= 5:
                            ItemActionHandler.inventoryCounts[9] = 0;
                            ItemActionHandler.inventoryCounts[1] -= 5;
                            game.Link.GainHealth(game.Link.GetMaxHealth());
                            audioLoader.Play("LOZ_Get_Item");
                            game.itemFactory.RemoveItem(lifePotion);
                            Debug.WriteLine("Bought life potion using 5 rupees.");
                            break;

                        default:
                            audioLoader.Play("LOZ_Get_Item");
                            Debug.WriteLine("Obtained Item \"" + item.GetID() + "\"");
                            game.itemFactory.RemoveItem(item);
                            break;
                    }
                }
            }
        }
    }

    static private void PlayerDoorCollision(Vector3 map_size, IPlayer player, PlayerEnemy enemy, MapHandler map){
        Rectangle playerHitBox = player.GetHitBox();
        Vector2 playerCenterpoint = new Vector2(playerHitBox.X + playerHitBox.Width / 2, playerHitBox.Y + playerHitBox.Height / 2);
        Rectangle up_door = new Rectangle((int)(0.46875 * map_size.X), (int)(map_size.Z), (int)(0.0625 * map_size.X), (int)(0.18 * map_size.Y));
        Rectangle down_door = new Rectangle((int)(0.46875 * map_size.X), (int)((0.82 * map_size.Y) + map_size.Z), (int)(0.0625 * map_size.X), (int)(0.18 * map_size.Y));
        Rectangle left_door = new Rectangle(0, (int)((0.45 * map_size.Y) + map_size.Z), (int)(0.125 * map_size.X), (int)(0.1 * map_size.Y));
        Rectangle right_door = new Rectangle((int)(0.875 * map_size.X), (int)((0.45 * map_size.Y) + map_size.Z), (int)(0.125 * map_size.X), (int)(0.1 * map_size.Y));
        if (up_door.Contains(playerCenterpoint)) {
            map.move_up();
            player.SetPlayerPosition(new Vector2((int)(map_size.X/2), (int)((0.8*map_size.Y) + map_size.Z)));
            enemy.SetEnemyPlayerPosition(new Vector2((int)(map_size.X / 2), (int)((0.2 * map_size.Y) + map_size.Z)));
		}
		else if (down_door.Contains(playerCenterpoint)){
            map.move_down();
            player.SetPlayerPosition(new Vector2((int)(map_size.X/2), (int)((0.2*map_size.Y) + map_size.Z)));
            enemy.SetEnemyPlayerPosition(new Vector2((int)(map_size.X/ 2), (int)((0.8 * map_size.Y) + map_size.Z)));
        }
		else if (left_door.Contains(playerCenterpoint)){
            map.move_left();
            player.SetPlayerPosition(new Vector2((int)(0.85*map_size.X), (int)((map_size.Y/2) + map_size.Z)));
            enemy.SetEnemyPlayerPosition(new Vector2((int)(0.15 * map_size.X), (int)((map_size.Y / 2) + map_size.Z)));
		}
		else if (right_door.Contains(playerCenterpoint)){
            map.move_right();
            player.SetPlayerPosition(new Vector2((int)(0.15*map_size.X), (int)((map_size.Y/2) + map_size.Z)));
            enemy.SetEnemyPlayerPosition(new Vector2((int)(0.85 * map_size.X), (int)((map_size.Y / 2) + map_size.Z)));
		}

        //Room_0_1 Stair collision
        Rectangle stair = new Rectangle((int)(map_size.X / 2), (int)((map_size.Y / 11 * 5) + map_size.Z), (int)(map_size.X / 16), (int)(map_size.Y / 11));
        if (map.getMapXY().Equals(new Vector2(1, 0)))
        {
            if (stair.Contains(playerCenterpoint))
            {
                Globals.audioLoader.Play("LOZ_Stairs");
                map.switch_map(0, 0);
                map.x = 0;
                map.y = 0;
                player.SetPlayerPosition(new Vector2(175, 415));
            }
        }

        //Room_0_0 back to room_0_1
        Rectangle invisibleDoor = new Rectangle((int)(map_size.X / 16 * 3), (int)(map_size.Z), (int)(map_size.X / 16), (int)(map_size.Y / 3));
        if (map.getMapXY().Equals(new Vector2(0, 0)))
        {
            if (invisibleDoor.Contains(playerCenterpoint))
            {
                Globals.audioLoader.Play("LOZ_Stairs");
                map.switch_map(0, 1);
                map.x = 1;
                map.y = 0;
                player.SetPlayerPosition(new Vector2(375, 480));
            }
        }
    }

    private void EnemyObjectCollision()
    {
        enemyCollisionHandler.Update();
    }

    private void PushableBlockCollision()
    {
        Dictionary<IPlayerProjectile, Rectangle> activeProjectiles = game.Link.GetProjectileHitBoxes();
        List<IEnemy> enemies = game.enemyFactory.GetAllEnemies();
        List<PushableBlock> pushableBlock = game.pushableBlockHandler.GetPushableBlocksList();

        foreach (PushableBlock block in pushableBlock)
        {
            foreach (IEnemy enemy in enemies)
                if (enemy.GetHitBox().Intersects(block.GetRectangle()))
                    enemy.OnCollision(Rectangle.Intersect(enemy.GetHitBox(), block.GetRectangle()));

            foreach (IPlayerProjectile projectile in activeProjectiles.Keys)
                if (activeProjectiles[projectile].Intersects(block.GetRectangle()))
                    projectile.Collided();
        }
    }

    private void PlayerNPCCollision()
    {
        if (game.NPCFactory.IsInNPCRoom(game.map.getMapXY())) {
            List<INPC> npcList = game.NPCFactory.GetNPCList();
            Rectangle playerHitBox = game.Link.GetHitBox();
            for (int i = 0; i < npcList.Count; i++)
            {
                if (npcList[i].GetNPCHitBox().Intersects(playerHitBox))
                {
                    game.Link.OnCollision(npcList[i].GetNPCHitBox());
                    Debug.WriteLine("Player collides with NPC.");
                }
            }
        }
    }

    private void BombBreakableWallCollision()
    {
        Dictionary<string, Rectangle> door_type = new Dictionary<string, Rectangle>(){
            {"up", new Rectangle( (int)(game.mapSize.X * 0.46875), (int)((game.mapSize.Y * 0) + game.mapSize.Z), (int)(game.mapSize.X * 0.0625), (int)(game.mapSize.Y * 0.18))},
            {"down", new Rectangle( (int)(game.mapSize.X * 0.46875), (int)((game.mapSize.Y * 0.82) + game.mapSize.Z), (int)(game.mapSize.X * 0.0625), (int)(game.mapSize.Y * 0.18))},
            {"left", new Rectangle( 0, (int)(game.mapSize.Y * 0.45), (int)((game.mapSize.X * 0.125) + game.mapSize.Z), (int)(game.mapSize.Y * 0.1))},
            {"right", new Rectangle( (int)(game.mapSize.X * 0.875), (int)((game.mapSize.Y * 0.45) + game.mapSize.Z), (int)(game.mapSize.X * 0.125), (int)(game.mapSize.Y * 0.1))}
        };
        foreach (IPlayerProjectile projectile in game.Link.GetProjectileHitBoxes().Keys)
        {
            if (projectile.GetType() == typeof(BombSprite)) {
                foreach (string key in door_type.Keys)
                {
                    if (door_type[key].Intersects(projectile.GetHitBox()))
                    {
                        game.map.BreakWall(key);
                    }
                }
            }
        }
    }
    private void PlayerKeyedDoorCollision(IPlayer player, MapHandler map, Vector3 map_size)
    {
        Rectangle playerHitBox = player.GetHitBox();
        Rectangle up_door = new Rectangle((int)(0.46875 * map_size.X), (int)(map_size.Z), (int)(0.0625 * map_size.X), (int)(0.18 * map_size.Y));
        Rectangle down_door = new Rectangle((int)(0.46875 * map_size.X), (int)((0.82 * map_size.Y) + map_size.Z), (int)(0.0625 * map_size.X), (int)(0.18 * map_size.Y));
        Rectangle left_door = new Rectangle(0, (int)((0.45 * map_size.Y) + map_size.Z), (int)(0.125 * map_size.X), (int)(0.1 * map_size.Y));
        Rectangle right_door = new Rectangle((int)(0.875 * map_size.X), (int)((0.45 * map_size.Y) + map_size.Z), (int)(0.125 * map_size.X), (int)(0.1 * map_size.Y));
        Dictionary<string, Rectangle> door_type = new Dictionary<string, Rectangle>(){
            {"up", up_door},
            {"down", down_door},
            {"left", left_door},
            {"right", right_door}
        };
        foreach (string key in door_type.Keys)
        {
            if (door_type[key].Intersects(playerHitBox) && ItemActionHandler.inventoryCounts[2] >= 1)
            {
                if(game.map.UnlockDoor(key)){
                    Globals.audioLoader.Play("LOZ_Door_Unlock");
                    ItemActionHandler.inventoryCounts[2]--;
                }
            }
        }
    }
    public void Update() {
        ItemPlayerCollision();
		BombBreakableWallCollision();
		PlayerProjectileEnemyCollision();
        PlayerProjectileMapCollision();
        PlayerMapCollision();
        PlayerDoorCollision(game.mapSize, game.Link, game.playerEnemy, game.map);
        PlayerNPCCollision();
		EnemyPlayerCollision();
        EnemyPlayerToPlayerCollision();
        EnemyProjectilePlayerCollision();
        EnemyObjectCollision();
        PushableBlockCollision();
        PlayerBombCollision();
        PlayerKeyedDoorCollision(game.Link, game.map, game.mapSize);
    }
}