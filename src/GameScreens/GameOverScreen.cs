using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ZeldaGame
{

    public class GameOverScreen : IGameScreen
    {
        private Game1 myGame;
        private Vector2 window_size;
        private Texture2D blackTexture;
        private Texture2D redTexture;
        private bool showRedScreen = true;
        private int fadeDuration = 100;
        private int currentFrame = 0;

        public GameOverScreen(Texture2D gameOverTexture, Game1 game)
        {
            this.myGame = game;
            window_size = game.windowSize;
            this.blackTexture = gameOverTexture;
            this.blackTexture.SetData(new Color[] { Color.Black });
            redTexture = new Texture2D(myGame.GraphicsDevice, 1, 1);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (showRedScreen)
            {
                float transparency = (float)currentFrame / fadeDuration;
                Color redColor = new Color(255, 0, 0, (int)(10 * transparency));

                redTexture.SetData(new Color[] { redColor });
                spriteBatch.Draw(redTexture, new Rectangle(0, (int)myGame.mapSize.Z, (int)(myGame.mapSize.X), (int)myGame.mapSize.Y), Color.White);
            }
            else
            {
                spriteBatch.Draw(blackTexture, new Rectangle(0, 0, (int)window_size.X, (int)window_size.Y), Color.Black);

                SpriteFont font = myGame.font;
                float fontScale = 2;
                Vector2 fontSize = font.MeasureString("    GAME OVER\nPress R to restart") * fontScale;
                Vector2 fontPosition = (window_size - fontSize) / 2;
                spriteBatch.DrawString(font, "    GAME OVER\nPress R to restart", fontPosition, Color.White, 0, Vector2.Zero, fontScale, SpriteEffects.None, 0);
            }
        }

        public void Update()
        {
            currentFrame++;
            if (currentFrame >= fadeDuration)
            {
                showRedScreen = false;
                currentFrame = 0;
            }
        }
        public void Reset()
        {
            showRedScreen = true;
        }
    }
}