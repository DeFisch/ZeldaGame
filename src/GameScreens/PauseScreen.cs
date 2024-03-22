using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ZeldaGame
{

    public class PauseGame : IGameScreen{
        private Texture2D pauseTexture;
        private Game1 myGame;
        private Rectangle sourceRectangle;
        private Rectangle destinationRectangle;
        private Vector2 window_size;
        public PauseGame(Texture2D pauseTexture, Game1 game)
        {
            this.pauseTexture = pauseTexture;
            myGame = game;
            window_size = game.windowSize;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            SpriteFont font = myGame.font;
            float font_scale = 2;
            Vector2 font_size = font.MeasureString("PAUSED") * font_scale;
            spriteBatch.DrawString(myGame.font, "PAUSED", (myGame.windowSize - font_size)/2, Color.White*0.1f, 0, Vector2.Zero, font_scale, SpriteEffects.None, 0);
        }
        public void Update()
        {
            // Update the pause screen
        }
    }
}