using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
namespace ZeldaGame.Player;

public class PlayerSpriteFactory {
	private Texture2D playerTexture;
	private Vector2 window_size;

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

	public ISprite CreateIdleUpPlayer(Vector2 location) {
		return new IdleUpSprite(playerTexture, location); // will make ISprite methods later
	}
	public ISprite CreateIdleLeftPlayer(Vector2 location) {
		return new IdleLeftSprite(playerTexture, location);
	}
	public ISprite CreateIdleDownPlayer(Vector2 location) {
		return new IdleDownSprite(playerTexture, location);
	}
	public ISprite CreateIdleRightPlayer(Vector2 location) {
		return new IdleRightSprite(playerTexture, location);
	}
	/*
		Add additional methods for attack, use item, etc.
	 */

	public void Draw(SpriteBatch spriteBatch) {
		//player.Draw(spriteBatch);
	}
	public void Update() {
		//player.Update();
	}
}