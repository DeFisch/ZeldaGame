using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using static ZeldaGame.Globals;

namespace ZeldaGame.Player;
public class PlayerItemSpriteFactory {
	private Texture2D playerTexture;

	private static readonly PlayerItemSpriteFactory instance = new();

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

	public IPlayerProjectile CreateItemSprite(Direction direction, PlayerProjectiles item, Vector2 position) {
		switch (item) {
			case PlayerProjectiles.WoodenArrow:
				return CreateArrowSprite(direction, position);
			case PlayerProjectiles.BlueArrow:
				return CreateBlueArrowSprite(direction, position);
			case PlayerProjectiles.Boomerang:
				return CreateBoomerangSprite(direction, position);
			case PlayerProjectiles.BlueBoomerang:
				return CreateBlueBoomerangSprite(direction, position);
			case PlayerProjectiles.Bomb:
				return CreateBombSprite(direction, position);
			case PlayerProjectiles.Fireball:
				return CreateFireballSprite(direction, position);
			default:
				return null;
		}
	}

	public IPlayerProjectile CreateSwordSprite(Direction direction, Swords sword, Vector2 position) {
		Globals.audioLoader.Play("LOZ_Sword_Slash");
		switch (sword) {
			case Swords.Wood:
				return CreateWoodSwordSprite(direction, position);
			case Swords.White:
				return CreateWhiteSwordSprite(direction, position);
			case Swords.Magic:
				return CreateMagicSwordSprite(direction, position);
			default:
				return null;
		}
	}

	public IPlayerProjectile CreateSwordProjectileSprite(Direction direction, Swords sword, Vector2 position) {
		Globals.audioLoader.Play("LOZ_Sword_Shoot");
		switch (sword) {
			case Swords.Wood:
				return CreateWoodSwordProjectileSprite(direction, position);
			case Swords.White:
				return CreateWhiteSwordProjectileSprite(direction, position);
			case Swords.Magic:
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
				return new WhiteSwordVerticalSprite(playerTexture, Direction.Up, position);
			case Direction.Left:
				return new WhiteSwordHorizontalSprite(playerTexture, Direction.Left, position);
			case Direction.Down:
				return new WhiteSwordVerticalSprite(playerTexture, Direction.Down, position);
			case Direction.Right:
				return new WhiteSwordHorizontalSprite(playerTexture, Direction.Right, position);
		}
		return null;
	}

	public IPlayerProjectile CreateMagicSwordSprite(Direction direction, Vector2 position) {
		switch (direction) {
			case Direction.Up:
				return new MagicSwordVerticalSprite(playerTexture, Direction.Up, position);
			case Direction.Left:
				return new MagicSwordHorizontalSprite(playerTexture, Direction.Left, position);
			case Direction.Down:
				return new MagicSwordVerticalSprite(playerTexture, Direction.Down, position);
			case Direction.Right:
				return new MagicSwordHorizontalSprite(playerTexture, Direction.Right, position);
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