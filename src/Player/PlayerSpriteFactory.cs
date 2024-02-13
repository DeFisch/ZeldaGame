using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using static ZeldaGame.Player.PlayerStateMachine;
namespace ZeldaGame.Player;

public class PlayerSpriteFactory {
	private Texture2D playerTexture;
	//private Vector2 window_size;

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

	public ISprite CreateWalkSprite(Direction direction) {
		switch (direction) {
			case Direction.Up:
				return new WalkUpSprite(playerTexture);
			case Direction.Left:
				return new WalkLeftSprite(playerTexture);
			case Direction.Down:
				return new WalkDownSprite(playerTexture);
			case Direction.Right:
				return new WalkRightSprite(playerTexture);
		}
		return null;
	}

	public ISprite CreateIdleSprite(Direction direction) {
		switch (direction) {
			case Direction.Up:
				return new IdleUpSprite(playerTexture);
			case Direction.Left:
				return new IdleLeftSprite(playerTexture);
			case Direction.Down:
				return new IdleDownSprite(playerTexture);
			case Direction.Right:
				return new IdleRightSprite(playerTexture);
		}
		return null;
	}

	public ISprite CreateAttackSprite(Direction direction) {
		switch (direction) {
			case Direction.Up:
				return new AttackUpSprite(playerTexture);
			case Direction.Left:
				return new AttackLeftSprite(playerTexture);
			case Direction.Down:
				return new AttackDownSprite(playerTexture);
			case Direction.Right:
				return new AttackRightSprite(playerTexture);
		}
		return null;
	}
	public ISprite CreateUseItemSprite(Direction direction) {
		switch (direction) {
			case Direction.Up:
				return new UseItemUpSprite(playerTexture);
			case Direction.Left:
				return new UseItemLeftSprite(playerTexture);
			case Direction.Down:
				return new UseItemDownSprite(playerTexture);
			case Direction.Right:
				return new UseItemRightSprite(playerTexture);
		}
		return null;
	}
	/*
        Add additional methods for attack, use item, etc.
     */

}