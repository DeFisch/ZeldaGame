using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using ZeldaGame.Player;
using static ZeldaGame.Player.PlayerStateMachine;

public class WeaponHandler {
	private Dictionary<IProjectile, Vector2> activeProjectiles;
	private Vector2 projectileMovement;
	private int projectileSpeed = 3;
	public WeaponHandler() {
		activeProjectiles = new Dictionary<IProjectile, Vector2>();
	}

	public void UseItem(int item, Vector2 location, Direction direction) {
		IProjectile weapon = PlayerItemSpriteFactory.Instance.CreateItemSprite(direction, item);
		activeProjectiles.Add(weapon, location);
	}

	public void ProjectileExpiration(IProjectile projectile) {
		activeProjectiles.Remove(projectile);
	}

	public void Update() {
		foreach (IProjectile weapon in activeProjectiles.Keys) {
			switch (weapon.GetDirection()) {
				case Direction.Up:
					projectileMovement = new Vector2(0, -projectileSpeed);
					break;
				case Direction.Down:
					projectileMovement = new Vector2(0, projectileSpeed);
					break;
				case Direction.Left:
					projectileMovement = new Vector2(-projectileSpeed, 0);
					break;
				case Direction.Right:
					projectileMovement = new Vector2(projectileSpeed, 0);
					break;
			}
			activeProjectiles[weapon] = activeProjectiles[weapon] + projectileMovement;
			weapon.Update();
		}
	}

	public void Draw(SpriteBatch spriteBatch) {
		foreach (IProjectile weapon in activeProjectiles.Keys) {
			weapon.Draw(spriteBatch, activeProjectiles[weapon]);
		}
	}
}

