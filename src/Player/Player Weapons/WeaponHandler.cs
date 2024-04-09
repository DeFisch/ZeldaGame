using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using ZeldaGame.Items;
using static ZeldaGame.Player.PlayerActionHandler;
using static ZeldaGame.Globals;

namespace ZeldaGame.Player;
public class WeaponHandler {
	private readonly List<IPlayerProjectile> activeProjectiles;
	private readonly List<IPlayerProjectile> expiredProjectiles;

    int cooldown = 0;

	public Swords currSword;
	public WeaponHandler() {
		activeProjectiles = new List<IPlayerProjectile>();
		expiredProjectiles = new List<IPlayerProjectile>();
		currSword = Swords.Wood; //later, the default will be no sword
	}

	public void UseItem(PlayerProjectiles item, Vector2 location, Direction direction) {
		if (item == PlayerProjectiles.WoodenArrow && ItemActionHandler.inventoryCounts[1] >= 1)
			ItemActionHandler.inventoryCounts[1]--;
        if (item == PlayerProjectiles.BlueArrow && ItemActionHandler.inventoryCounts[1] >= 3)
            ItemActionHandler.inventoryCounts[1]-=3;

        if (cooldown == 0) {
			IPlayerProjectile weapon = PlayerItemSpriteFactory.Instance.CreateItemSprite(direction, item, location);
			activeProjectiles.Add(weapon);
			cooldown = 20;
        }
	}

	public void UseSword(Swords sword, Vector2 location, Direction direction, float currHealth, float maxHealth) {
		IPlayerProjectile weapon = PlayerItemSpriteFactory.Instance.CreateSwordSprite(direction, sword, location);
		activeProjectiles.Add(weapon);

		if (cooldown == 0 && currHealth == maxHealth) {
			IPlayerProjectile weaponProj = PlayerItemSpriteFactory.Instance.CreateSwordProjectileSprite(direction, sword, location);
			activeProjectiles.Add(weaponProj);
		}
		cooldown = 30;
	}

	public void ProjectileExpiration(IPlayerProjectile projectile) {
		Globals.audioLoader.Stop("LOZ_Arrow_Boomerang");
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

