using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
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

	public ISprite CreateIdleUpSprite() {
		return new IdleUpSprite(playerTexture); // will make ISprite methods later
	}
	public ISprite CreateIdleLeftSprite() {
		return new IdleLeftSprite(playerTexture);
	}
	public ISprite CreateIdleDownSprite() {
		return new IdleDownSprite(playerTexture);
	}
	public ISprite CreateIdleRightSprite() {
		return new IdleRightSprite(playerTexture);
	}
	public ISprite CreateWalkUpSprite() {
		return new WalkUpSprite(playerTexture);
	}
	public ISprite CreateWalkLeftSprite() {
		return new WalkLeftSprite(playerTexture);
	}
	public ISprite CreateWalkDownSprite() {
		return new WalkDownSprite(playerTexture);
	}
	public ISprite CreateWalkRightSprite() {
		return new WalkRightSprite(playerTexture);
	}
	/*
		Add additional methods for attack, use item, etc.
	 */

	//public void Draw(SpriteBatch spriteBatch) {
	//player.Draw(spriteBatch);
	//}
	//public void Update() {
	//player.Update();
	//}
}