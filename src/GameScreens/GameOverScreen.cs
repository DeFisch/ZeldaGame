using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ZeldaGame;

public class GameOverScreen : IGameScreen
{
    private Game1 game;
    private Texture2D texture;
    private Rectangle sourceRectangle;
    private Rectangle destinationRectangle;
    public GameOverScreen(Texture2D texture, Game1 game)
    {
        this.game = game;
        this.texture = texture;
    }
    public void Draw(SpriteBatch spriteBatch)
    {
        // Gets black pixel
        sourceRectangle = new Rectangle(4, 14, 1, 1);
        destinationRectangle = new Rectangle(0, 175, (int)game.mapSize.X, (int)game.mapSize.Y);
        // Draws background
        spriteBatch.Draw(texture, destinationRectangle, sourceRectangle, Color.Black);

        SpriteFont font = game.font;
        float font_scale = 2;
        Vector2 font_size = font.MeasureString("GAME OVER") * font_scale;
        Vector2 centerText = new Vector2(game.mapSize.X, game.mapSize.Y + destinationRectangle.Y * 2);
        // Draws text
        spriteBatch.DrawString(game.font, "GAME OVER", (centerText - font_size) / 2, Color.White, 0, Vector2.Zero, font_scale, SpriteEffects.None, 0);
    }

    public void Update()
    {
    }
}