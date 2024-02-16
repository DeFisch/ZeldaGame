using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using ZeldaGame.Player;
using static ZeldaGame.Player.PlayerStateMachine;

public class WeaponHandler {
	private List<IPlayerProjectile> activeProjectiles;
	private List<IPlayerProjectile> expiredProjectiles;

	public WeaponHandler() {
		activeProjectiles = new List<IPlayerProjectile>();
		expiredProjectiles = new List<IPlayerProjectile>();
	}

	public void UseItem(int item, Vector2 location, Direction direction) {
		IPlayerProjectile weapon = PlayerItemSpriteFactory.Instance.CreateItemSprite(direction, item, location);
		activeProjectiles.Add(weapon);
	}

	public void ProjectileExpiration(IPlayerProjectile projectile) {
		expiredProjectiles.Add(projectile);
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

	public void Draw(SpriteBatch spriteBatch) {
		foreach (IPlayerProjectile projectile in activeProjectiles)
		{
			projectile.Draw(spriteBatch);
		}
	}
}

