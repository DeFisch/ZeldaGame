using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ZeldaGame
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public ISprite Link;
        public Texture2D sprite;

        private Vector2 location;
        private SpriteFont font;
        int framesPerSecond;

        private IController<Keys> keyboardController;
        private IController<Rectangle> mouseController;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // Initialize variables
            framesPerSecond = 0;
            location = new Vector2(350, 200);

            // Initialize controllers
            keyboardController = new KeyboardController();
            mouseController = new MouseController();
            
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // Load content
            sprite = Content.Load<Texture2D>("Link");
            font = Content.Load<SpriteFont>("Font");
            // Initializes Link with Sprite1
            Link = new Sprite1(sprite);

            // Registers commands with Keys as the identifier
            keyboardController.RegisterCommand(Keys.D0, new QuitCommand(this));
            keyboardController.RegisterCommand(Keys.D1, new SetSprite1Command(this));
            keyboardController.RegisterCommand(Keys.D2, new SetSprite2Command(this));
            keyboardController.RegisterCommand(Keys.D3, new SetSprite3Command(this));
            keyboardController.RegisterCommand(Keys.D4, new SetSprite4Command(this));

            // Registers commands with Rectangles as the identifier
            mouseController.RegisterCommand(new Rectangle(0, 0, 400, 250), new SetSprite1Command(this));
            mouseController.RegisterCommand(new Rectangle(401, 0, 800, 250), new SetSprite2Command(this));
            mouseController.RegisterCommand(new Rectangle(0, 251, 400, 500), new SetSprite3Command(this));
            mouseController.RegisterCommand(new Rectangle(401, 251, 800, 500), new SetSprite4Command(this));
        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.D0) || Mouse.GetState().RightButton == ButtonState.Pressed)
            {
                Exit();
            }

            // Updates the controllers
            keyboardController.Update();
            mouseController.Update();

            // Delays frames to show animation
            framesPerSecond++;
            if (framesPerSecond == 10)
            {
                Link.Update();
                framesPerSecond = 0;
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // Draws Link
            Link.Draw(_spriteBatch, location);

            // Draws credits
            _spriteBatch.Begin();
            _spriteBatch.DrawString(font, "Credits", new Vector2(250, 300), Color.Black);
            _spriteBatch.DrawString(font, "Project Made By: Dan Perry", new Vector2(250, 325), Color.Black);
            _spriteBatch.DrawString(font, "Sprites From: \nhttps://www.spriters-resource.com\n/nes/legendofzelda/sheet/8366/", new Vector2(250, 350), Color.Black);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
