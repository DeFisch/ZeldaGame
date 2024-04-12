using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ZeldaGame.Commands;
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
            myGame.keyboardController.RegisterPressKey(Keys.Up, new DifficultyUpCommand(myGame));
            myGame.keyboardController.RegisterPressKey(Keys.Down, new DifficultyDownCommand(myGame));
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            int frame_id = currentFrame % 2;
            sourceRectangle = new Rectangle(screen_frames[frame_id, 0], screen_frames[frame_id, 1], screen_frames[frame_id, 2], screen_frames[frame_id, 3]);
            destinationRectangle = new Rectangle(0, 0, (int)window_size.X, (int)window_size.Y);
            spriteBatch.Draw(titleTexture, destinationRectangle, sourceRectangle, Color.White);
            SpriteFont font = myGame.font;
            float font_scale = 1.5f;
            switch(myGame.level)
            {
                case 0:
                    Vector2 font_size = font.MeasureString("Level: Easy") * font_scale;
                    spriteBatch.DrawString(font, "Level: Easy", new Vector2((window_size.X-font_size.X)/2,450), Color.Green, 0, Vector2.Zero, font_scale, SpriteEffects.None, 0);
                    break;
                case 1:
                    Vector2 font_size1 = font.MeasureString("Level: Hard") * font_scale;
                    spriteBatch.DrawString(font, "Level: Hard", new Vector2((window_size.X - font_size1.X) / 2, 450), Color.Red, 0, Vector2.Zero, font_scale, SpriteEffects.None, 0);
                    break;
                case 2:
                    Vector2 font_size2 = font.MeasureString("Level: Insane") * font_scale;
                    spriteBatch.DrawString(font, "Level: Insane", new Vector2((window_size.X - font_size2.X) / 2, 450), Color.Brown, 0, Vector2.Zero, font_scale, SpriteEffects.None, 0);
                    break;
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
    }
}
