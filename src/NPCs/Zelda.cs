using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;


namespace ZeldaGame.NPCs;

public class Zelda : INPC {
	private Texture2D texture;
	private Vector2 position;
	private int currentFrame = 0;
	private int totalFrames = 2;
	private static int[,] character_sprites = new int[,] { { 1, 42, 16, 16 }, { 18, 42, 16, 16 } };
	private int switchFrameDelay = 15;
	private int frameCounter = 0;
	private Rectangle sourceRectangle;
	private Rectangle destinationRectangle;
    private string npcQuote;
    private int charactersDisplayed = 0;
	private bool quoteDisplayed = false;

    public Zelda(Texture2D texture, Vector2 position) {
		this.texture = texture;
		this.position = position;
        npcQuote = "I AM ZELDA, PRINCESS OF HYRULE.";
    }

	public void Draw(SpriteBatch spriteBatch, Vector2 scale, SpriteFont font) {
		int sprite_id = currentFrame % 2;
		sourceRectangle = new Rectangle(character_sprites[sprite_id, 0], character_sprites[sprite_id, 1], character_sprites[sprite_id, 2], character_sprites[sprite_id, 3]);
		destinationRectangle = new Rectangle((int)position.X, (int)position.Y, (int)(sourceRectangle.Width * scale.X), (int)(sourceRectangle.Height * scale.Y));
		spriteBatch.Draw(texture, destinationRectangle, sourceRectangle, Color.White);

		if (!quoteDisplayed && charactersDisplayed < npcQuote.Length)
		{
			string displayedText = npcQuote.Substring(0, charactersDisplayed + 1);
			spriteBatch.DrawString(font, displayedText, new Vector2(125, 125), Color.White);
			charactersDisplayed++;
			if (charactersDisplayed == npcQuote.Length)
			{
				quoteDisplayed = true;
			}
		}
		else if(quoteDisplayed) {
            spriteBatch.DrawString(font, npcQuote, new Vector2(125, 125), Color.White);
        }
    }

	public void Update()
	{
		frameCounter++;
		if (frameCounter >= switchFrameDelay)
		{
			currentFrame++;
			if (currentFrame >= totalFrames)
			{
				currentFrame = 0;
			}
			frameCounter = 0;
		}
	}
    public Rectangle GetNPCHitBox()
    {
		return destinationRectangle;
    }
}