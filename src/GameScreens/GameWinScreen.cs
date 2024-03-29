using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ZeldaGame;

namespace ZeldaGame
{

    public class GameWinScreen : IGameScreen
    {
        private Game1 myGame;
        private Vector2 window_size;
        private Texture2D blackTexture;
        private string victoryMessage;
        private int charactersDisplayed = 0;
        private bool messageDisplayed = false;
        private SpriteFont font;

        public GameWinScreen(Texture2D gameWinTexture, Game1 game)
        {
            this.myGame = game;
            window_size = game.windowSize;
            font = game.font;
            victoryMessage = "THANKS LINK, YOU'RE \nTHE HERO OF HYRULE. \n\n\n\nFINALLY, \nPEACE RETURNS TO HYRULE. \nTHIS ENDS THE STORY.";
            this.blackTexture = gameWinTexture;
            this.blackTexture.SetData(new Color[] { Color.Black });
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(blackTexture, new Rectangle(0, 0, (int)window_size.X, (int)window_size.Y), Color.Black);
            if (!messageDisplayed && charactersDisplayed < victoryMessage.Length)
            {
                Globals.audioLoader.PlaySingleton("LOZ_Text", true);
                string displayedText = victoryMessage.Substring(0, charactersDisplayed + 1);
                spriteBatch.DrawString(font, displayedText, new Vector2(125, 300), Color.White);
                charactersDisplayed++;
                if (charactersDisplayed == victoryMessage.Length)
                {
                    messageDisplayed = true;
                }
            }

            else if (messageDisplayed)
            {
                Globals.audioLoader.StopSingleton("LOZ_Text");
                spriteBatch.DrawString(font, victoryMessage, new Vector2(125, 300), Color.White);
            }
        }

        public void Update()
        {

        }
    }
}