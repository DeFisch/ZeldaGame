using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ZeldaGame.Enemy;

namespace ZeldaGame
{
    public class Game1 : Game
    {
        public enum State { Attack, Idle, Left, Right, Forward, Backward}
        public enum Direction { Left, Right, Forward, Backward}
        public enum Health { Damaged, Dead, Healthy}

        private int window_width = 800;
        private int window_height = 600;
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public ISprite Link;
        public Texture2D sprite;

        private EnemyFactory enemyFactory;
        private int framesPerSecond;

        private KeyboardController keyboardController;
        private MouseController mouseController;
        private List<IController> controllers;

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

            // Initialize controllers
            keyboardController = new KeyboardController();
            mouseController = new MouseController();
            controllers = new List<IController> { keyboardController, mouseController };

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
            Texture2D enemies = Content.Load<Texture2D>("enemies");

            // Initializes object classes
            Link = new IdleState(Direction.Forward, Health.Healthy);

            enemyFactory = new EnemyFactory(enemies, window_size: new Vector2(window_width, window_height));
            Random random = new Random();
            for (int i = 0; i < 5; i++) // spawn 5 enemies of each type
            {
                enemyFactory.AddEnemy("Stalfos");
                enemyFactory.AddEnemy("Gibdo");
                if (i % 2 == 0)
                    enemyFactory.AddEnemy("KeeseGoriya", color_variation: "blue");
                else
                    enemyFactory.AddEnemy("KeeseGoriya", color_variation: "red");
            }

            // Registers commands with Keys as the identifier
            keyboardController.RegisterCommand(Keys.D0, new QuitCommand(this));
            keyboardController.RegisterCommand(Keys.D1, new SetSprite1Command(this));
            keyboardController.RegisterCommand(Keys.D2, new SetSprite2Command(this));
            keyboardController.RegisterCommand(Keys.D3, new SetSprite3Command(this));
            keyboardController.RegisterCommand(Keys.D4, new SetSprite4Command(this));
            //keyboardController.RegisterCommand(Keys.Z, new AttackingState());

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

            foreach (IController controller in controllers)
            {
                controller.Update();
            }

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

            _spriteBatch.Begin();
            
            // Draws enemies
            enemyFactory.Draw(_spriteBatch);
            
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
