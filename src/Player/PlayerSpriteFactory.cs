using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using static ZeldaGame.Player.PlayerStateMachine;

namespace ZeldaGame.Player;
public class PlayerSpriteFactory {
	private Texture2D playerTexture;

	private static PlayerSpriteFactory instance = new PlayerSpriteFactory();

	public static PlayerSpriteFactory Instance {
		get {
			return instance;
		}
	}
	private PlayerSpriteFactory() {
	}

	public void LoadAllTextures(ContentManager content) {
		playerTexture = content.Load<Texture2D>("Link");
	}

	public IPlayerSprite CreateWalkSprite(Direction direction) {
		switch (direction) {
			case Direction.Up:
				return new WalkUpSprite(playerTexture);
			case Direction.Left:
				return new WalkLeftSprite(playerTexture);
			case Direction.Down:
				return new WalkDownSprite(playerTexture);
			case Direction.Right:
				return new WalkRightSprite(playerTexture);
			default:
				return null;
		}
	}

	public IPlayerSprite CreateUseItemSprite(Direction direction) {
		switch (direction) {
			case Direction.Up:
				return new UseItemUpSprite(playerTexture);
			case Direction.Left:
				return new UseItemLeftSprite(playerTexture);
			case Direction.Down:
				return new UseItemDownSprite(playerTexture);
			case Direction.Right:
				return new UseItemRightSprite(playerTexture);
			default:
				return null;
		}
	}
}