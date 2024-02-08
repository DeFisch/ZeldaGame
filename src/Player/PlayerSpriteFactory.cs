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

	public ISprite CreateIdleUpPlayer() {
		return new IdleUpSprite(playerTexture); // will make ISprite methods later
	}
	public ISprite CreateIdleLeftPlayer() {
		return new IdleLeftSprite(playerTexture);
	}
	public ISprite CreateIdleDownPlayer() {
		return new IdleDownSprite(playerTexture);
	}
	public ISprite CreateIdleRightPlayer() {
		return new IdleRightSprite(playerTexture);
	}
	public ISprite CreateWalkUpPlayer() {
		return new IdleUpSprite(playerTexture); // will make ISprite methods later
	}
	public ISprite CreateWalkLeftPlayer() {
		return new IdleLeftSprite(playerTexture);
	}
	public ISprite CreateWalkDownPlayer() {
		return new IdleDownSprite(playerTexture);
	}
	public ISprite CreateWalkRightPlayer() {
		return new IdleRightSprite(playerTexture);
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