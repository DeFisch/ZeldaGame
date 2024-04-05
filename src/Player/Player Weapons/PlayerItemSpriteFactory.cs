using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using static ZeldaGame.Player.PlayerStateMachine;

namespace ZeldaGame.Player;
public class PlayerItemSpriteFactory {
	private Texture2D playerTexture;

	private static PlayerItemSpriteFactory instance = new PlayerItemSpriteFactory();

	public static PlayerItemSpriteFactory Instance {
		get {
			return instance;
		}
	}

	public PlayerItemSpriteFactory() {
	}

	public void LoadAllTextures(ContentManager content) {
		playerTexture = content.Load<Texture2D>("Link");
	}

	public IPlayerProjectile CreateItemSprite(Direction direction, int item, Vector2 position) {
		switch (item) {
			case 0:
				return CreateArrowSprite(direction, position);
			case 1:
				return CreateBlueArrowSprite(direction, position);
			case 2:
				return CreateBoomerangSprite(direction, position);
			case 3:
				return CreateBlueBoomerangSprite(direction, position);
			case 4:
				return CreateBombSprite(direction, position);
			case 5:
				return CreateFireballSprite(direction, position);
			default:
				return null;
		}
	}

	public IPlayerProjectile CreateSwordSprite(Direction direction, int sword, Vector2 position) {
		Globals.audioLoader.Play("LOZ_Sword_Slash");
		switch (sword) {
			case 0:
				return CreateWoodSwordSprite(direction, position);
			case 1:
				return CreateWhiteSwordSprite(direction, position);
			case 2:
				return CreateMagicSwordSprite(direction, position);
			default:
				return null;
		}
	}

	public IPlayerProjectile CreateSwordProjectileSprite(Direction direction, int sword, Vector2 position) {
		Globals.audioLoader.Play("LOZ_Sword_Shoot");
		switch (sword) {
			case 0:
				return CreateWoodSwordProjectileSprite(direction, position);
			case 1:
				return CreateWhiteSwordProjectileSprite(direction, position);
			case 2:
				return CreateMagicSwordProjectileSprite(direction, position);
			default:
				return null;
		}
	}

	public IPlayerProjectile CreateArrowSprite(Direction direction, Vector2 position) {
		Globals.audioLoader.Play("LOZ_Arrow_Boomerang", true);
		switch (direction) {
			case Direction.Up:
				return new ArrowVerticalSprite(playerTexture, Direction.Up, position);
			case Direction.Left:
				return new ArrowHorizontalSprite(playerTexture, Direction.Left, position);
			case Direction.Down:
				return new ArrowVerticalSprite(playerTexture, Direction.Down, position);
			case Direction.Right:
				return new ArrowHorizontalSprite(playerTexture, Direction.Right, position);
		}
		return null;
	}

	public IPlayerProjectile CreateBlueArrowSprite(Direction direction, Vector2 position) {
		Globals.audioLoader.Play("LOZ_Arrow_Boomerang", true);
		switch (direction) {
			case Direction.Up:
				return new BlueArrowVerticalSprite(playerTexture, Direction.Up, position);
			case Direction.Left:
				return new BlueArrowHorizontalSprite(playerTexture, Direction.Left, position);
			case Direction.Down:
				return new BlueArrowVerticalSprite(playerTexture, Direction.Down, position);
			case Direction.Right:
				return new BlueArrowHorizontalSprite(playerTexture, Direction.Right, position);
		}
		return null;
	}

	public IPlayerProjectile CreateWoodSwordSprite(Direction direction, Vector2 position) {
		switch (direction) {
			case Direction.Up:
				return new WoodSwordVerticalSprite(playerTexture, Direction.Up, position);
			case Direction.Left:
				return new WoodSwordHorizontalSprite(playerTexture, Direction.Left, position);
			case Direction.Down:
				return new WoodSwordVerticalSprite(playerTexture, Direction.Down, position);
			case Direction.Right:
				return new WoodSwordHorizontalSprite(playerTexture, Direction.Right, position);
		}
		return null;
	}

	public IPlayerProjectile CreateWhiteSwordSprite(Direction direction, Vector2 position) {
		switch (direction) {
			case Direction.Up:
				return new WoodSwordVerticalSprite(playerTexture, Direction.Up, position);
			case Direction.Left:
				return new WoodSwordHorizontalSprite(playerTexture, Direction.Left, position);
			case Direction.Down:
				return new WoodSwordVerticalSprite(playerTexture, Direction.Down, position);
			case Direction.Right:
				return new WoodSwordHorizontalSprite(playerTexture, Direction.Right, position);
		}
		return null;
	}

	public IPlayerProjectile CreateMagicSwordSprite(Direction direction, Vector2 position) {
		switch (direction) {
			case Direction.Up:
				return new WoodSwordVerticalSprite(playerTexture, Direction.Up, position);
			case Direction.Left:
				return new WoodSwordHorizontalSprite(playerTexture, Direction.Left, position);
			case Direction.Down:
				return new WoodSwordVerticalSprite(playerTexture, Direction.Down, position);
			case Direction.Right:
				return new WoodSwordHorizontalSprite(playerTexture, Direction.Right, position);
		}
		return null;
	}

	public IPlayerProjectile CreateWoodSwordProjectileSprite(Direction direction, Vector2 position) {
		switch (direction) {
			case Direction.Up:
				return new WoodSwordVerticalProjectileSprite(playerTexture, Direction.Up, position);
			case Direction.Left:
				return new WoodSwordHorizontalProjectileSprite(playerTexture, Direction.Left, position);
			case Direction.Down:
				return new WoodSwordVerticalProjectileSprite(playerTexture, Direction.Down, position);
			case Direction.Right:
				return new WoodSwordHorizontalProjectileSprite(playerTexture, Direction.Right, position);
		}
		return null;
	}

	public IPlayerProjectile CreateWhiteSwordProjectileSprite(Direction direction, Vector2 position) {
		switch (direction) {
			case Direction.Up:
				return new WhiteSwordVerticalProjectileSprite(playerTexture, Direction.Up, position);
			case Direction.Left:
				return new WhiteSwordHorizontalProjectileSprite(playerTexture, Direction.Left, position);
			case Direction.Down:
				return new WhiteSwordVerticalProjectileSprite(playerTexture, Direction.Down, position);
			case Direction.Right:
				return new WhiteSwordHorizontalProjectileSprite(playerTexture, Direction.Right, position);
		}
		return null;
	}

	public IPlayerProjectile CreateMagicSwordProjectileSprite(Direction direction, Vector2 position) {
		switch (direction) {
			case Direction.Up:
				return new MagicSwordVerticalProjectileSprite(playerTexture, Direction.Up, position);
			case Direction.Left:
				return new MagicSwordHorizontalProjectileSprite(playerTexture, Direction.Left, position);
			case Direction.Down:
				return new MagicSwordVerticalProjectileSprite(playerTexture, Direction.Down, position);
			case Direction.Right:
				return new MagicSwordHorizontalProjectileSprite(playerTexture, Direction.Right, position);
		}
		return null;
	}

	public IPlayerProjectile CreateBoomerangSprite(Direction direction, Vector2 position) {
		Globals.audioLoader.Play("LOZ_Arrow_Boomerang", true);
		return new BoomerangSprite(playerTexture, direction, position);
	}

	public IPlayerProjectile CreateBlueBoomerangSprite(Direction direction, Vector2 position) {
		Globals.audioLoader.Play("LOZ_Arrow_Boomerang", true);
		return new BlueBoomerangSprite(playerTexture, direction, position);
	}

	public IPlayerProjectile CreateBombSprite(Direction direction, Vector2 position) {
		Globals.audioLoader.Play("LOZ_Bomb_Drop");
		return new BombSprite(playerTexture, direction, position);
	}

	public IPlayerProjectile CreateFireballSprite(Direction direction, Vector2 position) {
		return new FireballSprite(playerTexture, direction, position);
	}

}