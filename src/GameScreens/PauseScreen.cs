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
            // Draw the pause screen
        }
        public void Update()
        {
            // Update the pause screen
        }
    }
}