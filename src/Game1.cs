using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Sprint0.Block;
using ZeldaGame.Enemy;
using ZeldaGame.Items;
using ZeldaGame.NPCs;
using ZeldaGame.Player;
using ZeldaGame.Player.Commands;

namespace ZeldaGame
{
    public class Game1 : Game
    {
        private int window_width = 800;
        private int window_height = 600;
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public Player1 Link;
        public Texture2D sprite;
        public PlayerSpriteFactory playerFactory;

        public Texture2D npcs;
        public NPCFactory NPCFactory;

        public Texture2D Objects;
        private BlueRuby blueRuby;
        public ObjectSpriteFactory objectFactory;

        private EnemyFactory enemyFactory;

        public BlockSpriteFactory blockSpriteFactory;

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
            //Texture2D enemies = Content.Load<Texture2D>("enemies");
            npcs = Content.Load<Texture2D>("NPCs");
            NPCFactory = new NPCFactory(npcs, new Vector2(window_width/2, window_height/2));
            
            Objects = Content.Load<Texture2D>("Objects");
            

            // Initializes object classes
            PlayerSpriteFactory.Instance.LoadAllTextures(Content);
			Link = new Player1(new Vector2(window_width, window_height));
            objectFactory = new ObjectSpriteFactory(Objects);
            //NPCFactory = new NPCFactory(npcs, new Vector2(window_width/2, window_height/2));

            Texture2D enemies = Content.Load<Texture2D>("enemies");
            blockSpriteFactory = new BlockSpriteFactory(Content.Load<Texture2D>("Level1_Map"));
            blueRuby = new BlueRuby(Objects, new Vector2(300, 150));
            // Initializes object classes


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

            //Add NPCs
            NPCFactory.AddNPC("Fairy");
            NPCFactory.AddNPC("Flame");
            NPCFactory.AddNPC("Merchant");
            NPCFactory.AddNPC("OldMan");
            NPCFactory.AddNPC("Zelda");
            NPCFactory.AddNPC("OldWoman");
            //Add Blocks
            blockSpriteFactory.AddBlocks("Stair");
            blockSpriteFactory.AddBlocks("Walls");
            blockSpriteFactory.AddBlocks("Ground");
            blockSpriteFactory.AddBlocks("Obstacle");
            //Add items
            objectFactory.ObjectList();

            // Registers commands with Keys as the identifier
            keyboardController.RegisterCommand(Keys.W, new SetWalkUpSpriteCommand(this), 0);
			keyboardController.RegisterCommand(Keys.W, new SetIdleUpSpriteCommand(this), 2);
			keyboardController.RegisterCommand(Keys.Up, new SetWalkUpSpriteCommand(this), 0);
			keyboardController.RegisterCommand(Keys.Up, new SetIdleUpSpriteCommand(this), 2);

			keyboardController.RegisterCommand(Keys.A, new SetWalkLeftSpriteCommand(this), 0);
			keyboardController.RegisterCommand(Keys.A, new SetIdleLeftSpriteCommand(this), 2);
			keyboardController.RegisterCommand(Keys.Left, new SetWalkLeftSpriteCommand(this), 0);
			keyboardController.RegisterCommand(Keys.Left, new SetIdleLeftSpriteCommand(this), 2);

			keyboardController.RegisterCommand(Keys.S, new SetWalkDownSpriteCommand(this), 0);
			keyboardController.RegisterCommand(Keys.S, new SetIdleDownSpriteCommand(this), 2);
			keyboardController.RegisterCommand(Keys.Down, new SetWalkDownSpriteCommand(this), 0);
			keyboardController.RegisterCommand(Keys.Down, new SetIdleDownSpriteCommand(this), 2);

			keyboardController.RegisterCommand(Keys.D, new SetWalkRightSpriteCommand(this), 0);
			keyboardController.RegisterCommand(Keys.D, new SetIdleRightSpriteCommand(this), 2);
			keyboardController.RegisterCommand(Keys.Right, new SetWalkRightSpriteCommand(this), 0);
			keyboardController.RegisterCommand(Keys.Right, new SetIdleRightSpriteCommand(this), 2);

			//Registers commands with Keys for blocks
			keyboardController.RegisterCommand(Keys.T, new NextBlockCommand(this), 0);
            keyboardController.RegisterCommand(Keys.Y, new PreviousBlockCommand(this), 0);

            //Register commands with keys for items
            keyboardController.RegisterCommand(Keys.I, new NextItemCommand(this), 0);
            keyboardController.RegisterCommand(Keys.U, new LastItemCommand(this), 0);

            //Registers commands with Keys for npcs
            //keyboardController.RegisterCommand(Keys.O, new NextNPCCommand(this));
            //keyboardController.RegisterCommand(Keys.P, new PreviousNPCCommand(this));

            // Registers commands with Rectangles as the identifier
            /*
            mouseController.RegisterCommand(new Rectangle(0, 0, 400, 250), new SetSprite1Command(this));
            mouseController.RegisterCommand(new Rectangle(401, 0, 800, 250), new SetSprite2Command(this));
            mouseController.RegisterCommand(new Rectangle(0, 251, 400, 500), new SetSprite3Command(this));
            mouseController.RegisterCommand(new Rectangle(401, 251, 800, 500), new SetSprite4Command(this));
            */
        }

		public void setSprite(ISprite sprite) {
			this.Link.SetSprite(sprite);
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

            Link.Update();

            base.Update(gameTime);
            enemyFactory.Update();
            //NPCFactory.Update();
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
            
            // Draws enemies
            enemyFactory.Draw(_spriteBatch);

            //NPCFactory.Draw(_spriteBatch);

            //Draws Blocks
            blockSpriteFactory.Draw(_spriteBatch);

            //Draws objects
            objectFactory.Draw(_spriteBatch);
            //blueRuby.Draw(_spriteBatch, new Vector2(300, 150));

            // Draws player
            Link.Draw(_spriteBatch);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
