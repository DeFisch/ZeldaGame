using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ZeldaGame.Block;
using System.Diagnostics;
using ZeldaGame.Map;
using ZeldaGame.Player;
using ZeldaGame.Enemy;
using System.Collections.Generic;
using ZeldaGame.Items;
using ZeldaGame.NPCs;
using System.Linq;

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
                game.Link.OnCollision(box);
                Debug.WriteLine("Player collides with wall."); 
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
					Debug.WriteLine("Projectile collides with wall.");
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
					Debug.WriteLine("Player projectile collides with enemy.");
				}
            }
        }
    }

	public void EnemyPlayerCollision() {
		foreach (IEnemy enemy in game.enemyFactory.GetAllEnemies()) {
			if (enemy.GetRectangle().Intersects(game.Link.GetPlayerHitBox()) && !game.Link.isHurting()) {
				game.Link = new HurtPlayer(game.Link, game);
				Debug.WriteLine("Enemy collides with player.");
			}
		}
	}

	public void EnemyProjectilePlayerCollision()
    {
        foreach (IEnemyProjectile projectile in game.enemyFactory.GetAllProjectiles())
        {
            if (projectile.GetRectangle().Intersects(game.Link.GetPlayerHitBox()) && !game.Link.isHurting())
            {
                game.Link = new HurtPlayer(game.Link, game);
				Debug.WriteLine("Enemy projectile collides with player.");
			}
        }
    }

    public void ItemPlayerCollision() {
        foreach (IItemSprite item in game.itemFactory.GetAllItems().ToList()) {
            if (item.GetHitBox().Intersects(game.Link.GetPlayerHitBox())) {
                game.itemFactory.RemoveItem(item);
				Debug.WriteLine("Player picks up item.");
			}
        }
    }

    public void PlayerDoorCollision(Vector2 window_size, IPlayer player, MapHandler map){
        Rectangle playerHitBox = player.GetPlayerHitBox();
        Vector2 playerCenterpoint = new Vector2(playerHitBox.X + playerHitBox.Width/2, playerHitBox.Y + playerHitBox.Height/2);
        Rectangle up_door = new Rectangle((int)(0.46875*window_size.X), 0, (int)(0.0625*window_size.X), (int)(0.18*window_size.Y));
        Rectangle down_door = new Rectangle((int)(0.46875*window_size.X), (int)(0.82*window_size.Y), (int)(0.0625*window_size.X), (int)(0.18*window_size.Y));
        Rectangle left_door = new Rectangle(0, (int)(0.45*window_size.Y), (int)(0.125*window_size.X), (int)(0.1*window_size.Y));
        Rectangle right_door = new Rectangle((int)(0.875*window_size.X), (int)(0.45*window_size.Y), (int)(0.125*window_size.X), (int)(0.1*window_size.Y));
        if (up_door.Contains(playerCenterpoint)){
            map.move_up();
            player.SetPlayerPosition(new Vector2((int)(window_size.X/2), (int)(0.8*window_size.Y)));
			Debug.WriteLine("Player enters top door.");
		}
		else if (down_door.Contains(playerCenterpoint)){
            map.move_down();
            player.SetPlayerPosition(new Vector2((int)(window_size.X/2), (int)(0.2*window_size.Y)));
			Debug.WriteLine("Player enters bottom door.");
		}
		else if (left_door.Contains(playerCenterpoint)){
            map.move_left();
            player.SetPlayerPosition(new Vector2((int)(0.8*window_size.X), (int)(window_size.Y/2)));
			Debug.WriteLine("Player enters left door.");
		}
		else if (right_door.Contains(playerCenterpoint)){
            map.move_right();
            player.SetPlayerPosition(new Vector2((int)(0.2*window_size.X), (int)(window_size.Y/2)));
			Debug.WriteLine("Player enters right door.");
		}

        //Room_0_1 Stair collision
        Rectangle stair = new Rectangle((int)(window_size.X / 2), (int)(window_size.Y / 11 * 5), (int)(window_size.X / 16), (int)(window_size.Y / 11));
        if (map.getMapXY().Equals(new Vector2(1, 0)))
        {
            if (stair.Contains(playerCenterpoint))
            {
                map.switch_map(0, 0);
                map.x = 0;
                map.y = 0;
                player.SetPlayerPosition(new Vector2(175, 240));
                Debug.WriteLine("Player enters stairs.");
            }
        }

        //Room_0_0 back to room_0_1
        Rectangle invisibleDoor = new Rectangle((int)(window_size.X / 16 * 3), 0, (int)(window_size.X / 16), (int)(window_size.Y / 3));
        if (map.getMapXY().Equals(new Vector2(0, 0)))
        {
            if (invisibleDoor.Contains(playerCenterpoint))
            {
                map.switch_map(0, 1);
                map.x = 1;
                map.y = 0;
                player.SetPlayerPosition(new Vector2(375,305));
				Debug.WriteLine("Player enters invisible door.");
			}
        }
    }

    public void EnemyObjectCollision()
    {
        enemyCollisionHandler.Update();
    }

    public void PushableBlockCollision() 
    {
        Dictionary<IPlayerProjectile, Rectangle> activeProjectiles = game.Link.GetProjectileHitBoxes();
        List<IEnemy> enemies = game.enemyFactory.GetAllEnemies();
        List<PushableBlock> pushableBlock = game.blockSpriteFactory.GetPushableBlocksList();

        foreach (PushableBlock block in pushableBlock)
        {
            foreach (IEnemy enemy in enemies)
                if(enemy.GetRectangle().Intersects(block.GetRectangle()))
                    enemy.Collide(Rectangle.Intersect(enemy.GetRectangle(), block.GetRectangle()));
            
            foreach (IPlayerProjectile projectile in activeProjectiles.Keys)
                if (activeProjectiles[projectile].Intersects(block.GetRectangle()))
                    projectile.Collided();
        }
    }

    private void PlayerNPCCollision()
    {
        if (game.NPCFactory.isInDungeon()) {
            List<INPC> npcList = game.NPCFactory.GetNPCList();
            Rectangle playerHitBox = game.Link.GetPlayerHitBox();
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

    public void BombBreakableWallCollision()
    {
        Dictionary<string, Rectangle> door_type = new Dictionary<string, Rectangle>(){
            {"up", new Rectangle( (int)(game.windowSize.X * 0.46875), (int)(game.windowSize.Y * 0), (int)(game.windowSize.X * 0.0625), (int)(game.windowSize.Y * 0.18))},
            {"down", new Rectangle( (int)(game.windowSize.X * 0.46875), (int)(game.windowSize.Y * 0.82), (int)(game.windowSize.X * 0.0625), (int)(game.windowSize.Y * 0.18))},
            {"left", new Rectangle( 0, (int)(game.windowSize.Y * 0.45), (int)(game.windowSize.X * 0.125), (int)(game.windowSize.Y * 0.1))},
            {"right", new Rectangle( (int)(game.windowSize.X * 0.875), (int)(game.windowSize.Y * 0.45), (int)(game.windowSize.X * 0.125), (int)(game.windowSize.Y * 0.1))}
        };
        foreach (IPlayerProjectile projectile in game.Link.GetProjectileHitBoxes().Keys)
        {
           if (projectile.GetType() == typeof(BombSprite)){
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

    public void Update() {
        ItemPlayerCollision();
		BombBreakableWallCollision();
		PlayerProjectileEnemyCollision();
        PlayerProjectileMapCollision();
        PlayerMapCollision();
        PlayerDoorCollision(game.windowSize, game.Link, game.map);
        PlayerNPCCollision();
		EnemyPlayerCollision();
        EnemyProjectilePlayerCollision();
        EnemyObjectCollision();
        PushableBlockCollision();
    }
}