using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ZeldaGame
{

    public class PauseScreen : IGameScreen{
        private Game1 myGame;
        private Vector2 window_size;
        public PauseScreen(Texture2D pauseTexture, Game1 game)
        {
            myGame = game;
            window_size = game.windowSize;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            SpriteFont font = myGame.font;
            float font_scale = 2;
            Vector2 font_size = font.MeasureString("PAUSED") * font_scale;
            spriteBatch.DrawString(font, "PAUSED", (window_size - font_size)/2, Color.White*0.1f, 0, Vector2.Zero, font_scale, SpriteEffects.None, 0);
        }
        public void Update()
        {
            // Update the pause screen
        }
    }
}