using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Diagnostics;
using static ZeldaGame.Player.PlayerStateMachine;

namespace ZeldaGame.Player;
public class WeaponHandler {
	public enum Swords {Wood, White, Magic, None};
	private readonly List<IPlayerProjectile> activeProjectiles;
	private readonly List<IPlayerProjectile> expiredProjectiles;
	int cooldown = 0;

	public Swords currSword;
	public WeaponHandler() {
		activeProjectiles = new List<IPlayerProjectile>();
		expiredProjectiles = new List<IPlayerProjectile>();
		currSword = Swords.Wood; //later, the default will be no sword
	}

	public void UseItem(int item, Vector2 location, Direction direction) {
		if (cooldown == 0) {
			IPlayerProjectile weapon = PlayerItemSpriteFactory.Instance.CreateItemSprite(direction, item, location);
			activeProjectiles.Add(weapon);
			cooldown = 20;
		}
	}

	public void UseSword(int sword, Vector2 location, Direction direction, int currHealth, int maxHealth) {
		IPlayerProjectile weapon = PlayerItemSpriteFactory.Instance.CreateSwordSprite(direction, sword, location);
		activeProjectiles.Add(weapon);

		if (cooldown == 0 && currHealth == maxHealth) {
			IPlayerProjectile weaponProj = PlayerItemSpriteFactory.Instance.CreateSwordProjectileSprite(direction, sword, location);
			activeProjectiles.Add(weaponProj);
		}
		cooldown = 30;
	}

	public void ProjectileExpiration(IPlayerProjectile projectile) {
		expiredProjectiles.Add(projectile);
	}

	public Dictionary<IPlayerProjectile, Rectangle> GetActiveHitBoxes()
	{
        Dictionary<IPlayerProjectile, Rectangle> activeHitBoxes = new Dictionary<IPlayerProjectile, Rectangle>();
		foreach (IPlayerProjectile projectile in activeProjectiles)
		{
            activeHitBoxes.Add(projectile, projectile.GetHitBox());
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

		while (cooldown > 0) {
			cooldown--;
		}
    }

    public void Draw(SpriteBatch spriteBatch, Vector2 scale) {
		foreach (IPlayerProjectile projectile in activeProjectiles)
		{
			projectile.Draw(spriteBatch, scale);
		}
    }
}

