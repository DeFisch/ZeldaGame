using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using static ZeldaGame.Player.PlayerStateMachine;

namespace ZeldaGame.Player;
public class PlayerItemSpriteFactory {
	private Texture2D playerTexture;
	private List<IPlayerProjectile> projectiles;
	private List<IPlayerProjectile> swords;

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
		projectiles = new List<IPlayerProjectile>
		{
			CreateArrowSprite(direction, position), CreateBlueArrowSprite(direction, position),
			CreateBoomerangSprite(direction, position), CreateBlueBoomerangSprite(direction, position),
			CreateBombSprite(direction, position), CreateFireballSprite(direction, position)
		};

		return projectiles[item];
	}

	public IPlayerProjectile CreateSwordSprite(Direction direction, int sword, Vector2 position) {
		swords = new List<IPlayerProjectile>
		{
			CreateWoodSwordSprite(direction, position) , CreateWhiteSwordSprite(direction, position), 
			CreateMagicSwordSprite(direction, position)
		};

		return swords[sword];
	}

	public IPlayerProjectile CreateArrowSprite(Direction direction, Vector2 position) {
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

	public IPlayerProjectile CreateBoomerangSprite(Direction direction, Vector2 position) {
		return new BoomerangSprite(playerTexture, direction, position);
	}

	public IPlayerProjectile CreateBlueBoomerangSprite(Direction direction, Vector2 position) {
		return new BlueBoomerangSprite(playerTexture, direction, position);
	}

	public IPlayerProjectile CreateBombSprite(Direction direction, Vector2 position) {
		return new BombSprite(playerTexture, direction, position);
	}

	public IPlayerProjectile CreateFireballSprite(Direction direction, Vector2 position) {
		return new FireballSprite(playerTexture, direction, position);
	}

}