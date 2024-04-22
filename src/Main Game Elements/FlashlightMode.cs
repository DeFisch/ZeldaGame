using System;
using System.Collections.Generic;
using Enemy;
using Enemy.Projectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ZeldaGame.Player;

namespace ZeldaGame;

public class FlashlightOverlay{
    private Game1 game;
    private Texture2D flashlightMask;
    private int timer = 0;
    public FlashlightOverlay(Game1 game, Texture2D flashlightMask){
        this.game = game;
        this.flashlightMask = flashlightMask;
    }
    public void Draw(SpriteBatch spriteBatch){
        timer++;
        Rectangle link_box = game.Link.GetHitBox();
        int light_radius = 75 + (int)(Math.Sin(timer / 25.0) * 15);
        link_box.Inflate(light_radius, light_radius);
        spriteBatch.Draw(flashlightMask, link_box, Color.White);
        Dictionary<IPlayerProjectile, Rectangle> activeHitBoxes = game.Link.GetProjectileHitBoxes();
        foreach (KeyValuePair<IPlayerProjectile, Rectangle> entry in activeHitBoxes)
        {
            if (typeof(FireballSprite).IsInstanceOfType(entry.Key)){
                Rectangle hitBox = entry.Value;
                light_radius = 125 + (int)(Math.Sin(timer / 10.0) * 25);
                hitBox.Inflate(light_radius, light_radius);
                spriteBatch.Draw(flashlightMask, hitBox, Color.White);
            } 
            if (entry.Key.GetType() == typeof(BombSprite)){
                Rectangle hitBox = entry.Value;
                if(hitBox.Width == 0){
                    continue;
                }
                light_radius = 300;
                hitBox.Inflate(light_radius, light_radius);
                spriteBatch.Draw(flashlightMask, hitBox, Color.White);
                spriteBatch.Draw(flashlightMask, hitBox, Color.White);
                spriteBatch.Draw(flashlightMask, hitBox, Color.White);
            }
        }

        foreach (IEnemyProjectile projectile in game.enemyFactory.GetAllProjectiles())
        {
            if (projectile.GetType() == typeof(FireBall)){
                Rectangle hitBox = projectile.GetRectangle();
                light_radius = 125 + (int)(Math.Sin(timer / 10.0) * 25);
                hitBox.Inflate(light_radius, light_radius);
                spriteBatch.Draw(flashlightMask, hitBox, Color.White);
            }
        }

        foreach (var item in game.itemFactory.GetAllItems())
        {
            Rectangle hitBox = item.GetHitBox();
            light_radius = 50 + (int)(Math.Sin(timer / 9.0) * 15);
            hitBox.Inflate(light_radius, light_radius);
            spriteBatch.Draw(flashlightMask, hitBox, Color.White*0.5f);
        }
    }
}