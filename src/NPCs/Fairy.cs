using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static ZeldaGame.Globals;

namespace ZeldaGame.NPCs;

public class Fairy : INPC {
	private Texture2D texture;
	private Vector2 position;
    private int currentFrame = 0;
    private int totalFrames = 2;
    private static int[,] character_sprites = new int[,] { { 186, 11, 8, 16 }, { 195, 11, 8, 16 } };
    private int switchFrameDelay = 15;
    private int frameCounter = 0;
    private Rectangle sourceRectangle;
	private Rectangle destinationRectangle;
	private string npcQuote;
    private int charactersDisplayed = 0;
    private bool quoteDisplayed = false;

    public Fairy(Texture2D texture, Vector2 position) {
		this.texture = texture;
		this.position = position;
		npcQuote = "WHEN YOU ARE WEARY OF BATTLE,\nPLEASE COME BACK TO VISIT ME!";

    }

	public void Draw(SpriteBatch spriteBatch, SpriteFont font) {
        int sprite_id = currentFrame % 2;
        sourceRectangle = new Rectangle(character_sprites[sprite_id, 0], character_sprites[sprite_id, 1], character_sprites[sprite_id, 2], character_sprites[sprite_id, 3]);
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