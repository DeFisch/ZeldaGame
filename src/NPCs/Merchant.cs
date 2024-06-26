﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace ZeldaGame.NPCs;

public class Merchant : INPC {
	private Texture2D texture;
	private Vector2 position;
	private Rectangle sourceRectangle;
	private Rectangle destinationRectangle;
	private string npcQuote;
    private int charactersDisplayed = 0;
    private bool quoteDisplayed = false;


    public Merchant(Texture2D texture, Vector2 position) {
		this.texture = texture;
		this.position = position;
		npcQuote = "BUY SOMETHIN' WILL YA!";
    }

	public void Draw(SpriteBatch spriteBatch, Vector2 scale, SpriteFont font) {
		sourceRectangle = new Rectangle(126, 11, 16, 16);
		destinationRectangle = new Rectangle((int)position.X, (int)position.Y, (int)(sourceRectangle.Width * scale.X), (int)(sourceRectangle.Height * scale.Y));
		spriteBatch.Draw(texture, destinationRectangle, sourceRectangle, Color.White);

        if (!quoteDisplayed && charactersDisplayed < npcQuote.Length)
        {
            Globals.audioLoader.PlaySingleton("LOZ_Text", true);
            string displayedText = npcQuote.Substring(0, charactersDisplayed + 1);
            spriteBatch.DrawString(font, displayedText, new Vector2(125, 300), Color.White);
            charactersDisplayed++;
            if (charactersDisplayed == npcQuote.Length)
            {
                quoteDisplayed = true;
            }
        }
        else if (quoteDisplayed)
        {
            Globals.audioLoader.StopSingleton("LOZ_Text");
            spriteBatch.DrawString(font, npcQuote, new Vector2(125, 300), Color.White);
        }
    }

    public void Update() {

	}
    public Rectangle GetNPCHitBox()
    {
        return destinationRectangle;
    }
}