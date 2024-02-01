using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ZeldaGame.Enemy;

namespace ZeldaGame
{
    public class Game1 : Game
    {
        private int window_width = 800;
        private int window_height = 600;
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public ISprite Link;
        public Texture2D sprite;

        private Vector2 location;
        private SpriteFont font;
        private EnemyFactory enemyFactory;
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

            // set fixed window size
            _graphics.PreferredBackBufferWidth = this.window_width;
            _graphics.PreferredBackBufferHeight = this.window_height;
            _graphics.ApplyChanges();
            
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // Load content
            sprite = Content.Load<Texture2D>("Link");
            font = Content.Load<SpriteFont>("Font");
            Texture2D enemies = Content.Load<Texture2D>("enemies");

            // Initializes object classes
            Link = new Sprite1(sprite);
            enemyFactory = new EnemyFactory(enemies);
            Random random = new Random();
            for (int i = 0; i < 100; i++)
            {
                enemyFactory.AddEnemy("Stalfos", new Vector2(random.Next(0, window_width-16), random.Next(0, window_height-16)));
            }

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
            enemyFactory.Update();
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // Draws Link
            Link.Draw(_spriteBatch, location);

            _spriteBatch.Begin();

            // Draws credits
            _spriteBatch.DrawString(font, "Credits", new Vector2(250, 300), Color.Black);
            _spriteBatch.DrawString(font, "Project Made By: Dan Perry", new Vector2(250, 325), Color.Black);
            _spriteBatch.DrawString(font, "Sprites From: \nhttps://www.spriters-resource.com\n/nes/legendofzelda/sheet/8366/", new Vector2(250, 350), Color.Black);
            
            
            // Draws enemies
            enemyFactory.Draw(_spriteBatch);
            
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
