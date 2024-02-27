﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Diagnostics;
using static ZeldaGame.Player.PlayerStateMachine;
namespace ZeldaGame.Player;

public class WeaponHandler {
	public enum Swords {Wood, White, Magic, None};
	private List<IPlayerProjectile> activeProjectiles;
	private List<IPlayerProjectile> expiredProjectiles;

	public Swords currSword;
	public WeaponHandler() {
		activeProjectiles = new List<IPlayerProjectile>();
		expiredProjectiles = new List<IPlayerProjectile>();
		currSword = Swords.Wood; //later, the default will be no sword
	}

	public void UseItem(int item, Vector2 location, Direction direction) {
		IPlayerProjectile weapon = PlayerItemSpriteFactory.Instance.CreateItemSprite(direction, item, location);
		activeProjectiles.Add(weapon);
	}

	public void UseSword(int sword, Vector2 location, Direction direction) {
		IPlayerProjectile weapon = PlayerItemSpriteFactory.Instance.CreateSwordSprite(direction, sword, location);
		activeProjectiles.Add(weapon);
	}

	public void ProjectileExpiration(IPlayerProjectile projectile) {
		expiredProjectiles.Add(projectile);
	}

	public List<Rectangle> GetActiveHitBoxes()
	{
		List<Rectangle> activeHitBoxes = new List<Rectangle>();
		foreach (IPlayerProjectile projectile in activeProjectiles)
		{
            activeHitBoxes.Add(projectile.GetHitBox());
		}
		return activeHitBoxes;
	}

	public void Update() {
		foreach (IPlayerProjectile projectile in activeProjectiles) {
            projectile.Update();
			if (!projectile.IsActive())
			{
				ProjectileExpiration(projectile);
			}
		}
		
		foreach (IPlayerProjectile projectile in expiredProjectiles)
		{
			activeProjectiles.Remove(projectile);
		}
		expiredProjectiles.Clear();
	}

	public void Draw(SpriteBatch spriteBatch, Vector2 scale) {
		foreach (IPlayerProjectile projectile in activeProjectiles)
		{
			projectile.Draw(spriteBatch, scale);
		}
	}
}

