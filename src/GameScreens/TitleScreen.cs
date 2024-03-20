using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ZeldaGame
{

    public class TitleScreen : IGameScreen
    {
        public Texture2D titleTexture;
        private Game1 myGame;
        private int currentFrame = 0;
        private int totalFrames = 2;
        private static int[,] screen_frames = new int[,] { { 1, 11, 256, 222 }, { 258, 11, 256, 222 } };
        private int switchFrameDelay = 15;
        private int frameCounter = 0;
        private Rectangle sourceRectangle;
        private Rectangle destinationRectangle;
        private Vector2 window_size;
        public TitleScreen(Texture2D titleTexture, Game1 game)
        {
            this.titleTexture = titleTexture;
            myGame = game;
            window_size = game.windowSize;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            int frame_id = currentFrame % 2;
            sourceRectangle = new Rectangle(screen_frames[frame_id, 0], screen_frames[frame_id, 1], screen_frames[frame_id, 2], screen_frames[frame_id, 3]);
            destinationRectangle = new Rectangle(0, 0, (int)window_size.X, (int)window_size.Y);
            spriteBatch.Draw(titleTexture, destinationRectangle, sourceRectangle, Color.White);
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
    }
}
