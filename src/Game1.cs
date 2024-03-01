using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ZeldaGame.Block;
using ZeldaGame.Controllers;
using ZeldaGame.Enemy;
using ZeldaGame.Enemy.Commands;
using ZeldaGame.Items;
using ZeldaGame.Map;
using ZeldaGame.Map.Commands;
using ZeldaGame.NPCs;
using ZeldaGame.Player;
using ZeldaGame.Player.Commands;

namespace ZeldaGame {
	public class Game1 : Game {
		private GraphicsDeviceManager _graphics;
		public SpriteBatch _spriteBatch;

		public IPlayer Link;
		public PlayerSpriteFactory playerFactory;

		public Texture2D npcs;
		public NPCFactory NPCFactory;

		public Texture2D Items;
		public ItemSpriteFactory itemFactory;

		public EnemyFactory enemyFactory;

		public BlockSpriteFactory blockSpriteFactory;
		public CollisionHandler collisionHandler;

		private KeyboardController keyboardController;
		private MouseController mouseController;
		private List<IController> controllers;
		public MapHandler map;
		public Vector2 windowSize;
		public Vector2 windowScale;
		public SpriteFont font;


		public Game1() {
			_graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
			IsMouseVisible = true;
		}

		protected override void Initialize() {

            // Initialize controllers
            keyboardController = new KeyboardController();
            mouseController = new MouseController();
			controllers = new List<IController> { keyboardController, mouseController };

			// set fixed window size
			windowSize = new Vector2(800, 600);
            _graphics.PreferredBackBufferWidth = (int)windowSize.X;
			_graphics.PreferredBackBufferHeight = (int)windowSize.Y;
			_graphics.ApplyChanges();

			// Initialize collition handler
			collisionHandler = new CollisionHandler(this);

			base.Initialize();
		}

		protected override void LoadContent() {
			_spriteBatch = new SpriteBatch(GraphicsDevice);

			// Load content
			npcs = Content.Load<Texture2D>("NPCs");
			Items = Content.Load<Texture2D>("Objects");
            font = Content.Load<SpriteFont>("Font");

			// Load default map
			Texture2D map_texture = Content.Load<Texture2D>("Level1_Map");
			map = new MapHandler(map_texture, windowSize);
			windowScale = map.GetWindowScale(windowSize);

			// Initializes item classes
			PlayerSpriteFactory.Instance.LoadAllTextures(Content);
			PlayerItemSpriteFactory.Instance.LoadAllTextures(Content);
            Link = new Player1(new Vector2(windowSize.X / 2, windowSize.X / 2), windowScale);

            NPCFactory = new NPCFactory(npcs, new Vector2(windowSize.X / 3, windowSize.Y / 3), windowScale, font);
			itemFactory = new ItemSpriteFactory(Items, npcs, windowScale);

			Texture2D[] enemy_texture = {Content.Load<Texture2D>("enemies"),Content.Load<Texture2D>("enemies_1")};
			blockSpriteFactory = new BlockSpriteFactory(Content.Load<Texture2D>("Level1_Map"), windowScale);
			enemyFactory = new EnemyFactory(enemy_texture, windowScale);
			Random random = new Random();
			enemyFactory.AddEnemy("Stalfos", new Vector2(random.Next((int)windowSize.X), random.Next((int)windowSize.Y)));

            // Define the quadrants based on the window size
            Rectangle leftDoorQuadrant = new Rectangle(0, (int)(windowSize.Y / 4), (int)(windowSize.X / 4), (int)(windowSize.Y / 2));
            Rectangle rightDoorQuadrant = new Rectangle((int)(3 * windowSize.X / 4), (int)(windowSize.Y / 4), (int)(windowSize.X / 4), (int)(windowSize.Y / 2));
            Rectangle topDoorQuadrant = new Rectangle((int)(windowSize.X / 4), 0, (int)(windowSize.X / 2), (int)(windowSize.Y / 4));
            Rectangle bottomDoorQuadrant = new Rectangle((int)(windowSize.X / 4), (int)(3 * windowSize.Y / 4), (int)(windowSize.X / 2), (int)(windowSize.Y / 4));

            //Add NPCs
            NPCFactory.AddNPCs();
			//Add Blocks
			blockSpriteFactory.AddBlock();
			//Add items
			itemFactory.ObjectList();

			// Registers commands with Keys as the identifier
			keyboardController.RegisterHoldKey(Keys.W, new SetWalkSpriteCommand(this, 0));
            keyboardController.RegisterHoldKey(Keys.A, new SetWalkSpriteCommand(this, 1));
            keyboardController.RegisterHoldKey(Keys.S, new SetWalkSpriteCommand(this, 2));
            keyboardController.RegisterHoldKey(Keys.D, new SetWalkSpriteCommand(this, 3));

            keyboardController.RegisterHoldKey(Keys.Up, new SetWalkSpriteCommand(this, 0));
			keyboardController.RegisterHoldKey(Keys.Left, new SetWalkSpriteCommand(this, 1));
            keyboardController.RegisterHoldKey(Keys.Down, new SetWalkSpriteCommand(this, 2));
            keyboardController.RegisterHoldKey(Keys.Right, new SetWalkSpriteCommand(this, 3));

			// Attack
			keyboardController.RegisterPressKey(Keys.Z, new AttackCommand(this));
			keyboardController.RegisterPressKey(Keys.N, new AttackCommand(this));

			// Use items
			keyboardController.RegisterPressKey(Keys.D1, new UseItemCommand(this, 0));
			keyboardController.RegisterPressKey(Keys.D2, new UseItemCommand(this, 1));
			keyboardController.RegisterPressKey(Keys.D3, new UseItemCommand(this, 2));
			keyboardController.RegisterPressKey(Keys.D4, new UseItemCommand(this, 3));
			keyboardController.RegisterPressKey(Keys.D5, new UseItemCommand(this, 4));
			keyboardController.RegisterPressKey(Keys.D6, new UseItemCommand(this, 5));

			//Registers commands with Keys for blocks
			keyboardController.RegisterPressKey(Keys.T, new NextBlockCommand(this));
			keyboardController.RegisterPressKey(Keys.Y, new PreviousBlockCommand(this));

			//Register commands with keys for items
			keyboardController.RegisterPressKey(Keys.I, new NextItemCommand(this));
			keyboardController.RegisterPressKey(Keys.U, new LastItemCommand(this));

            //Register commands with keys for npcs
            keyboardController.RegisterPressKey(Keys.O, new NextNPCCommand(this));
			keyboardController.RegisterPressKey(Keys.P, new PreviousNPCCommand(this));

			//Registers commands with Keys for enemies
			keyboardController.RegisterPressKey(Keys.K, new nextEnemyCommand(this));
			keyboardController.RegisterPressKey(Keys.L, new previousEnemyCommand(this));

			//Registers commands with Keys for Reset
			keyboardController.RegisterPressKey(Keys.R, new ResetCommand(this));

            //Registers commands with Keys and MouseButton for Quit
            keyboardController.RegisterPressKey(Keys.Q, new QuitCommand(this));
            mouseController.RegisterPressButton(MouseButtons.Right, new QuitCommand(this));

            //Registers commands with Keys for taking damage
            keyboardController.RegisterPressKey(Keys.E, new TakeDamageCommand(this));

			//Registers commands with Keys for switching maps
			keyboardController.RegisterPressKey(Keys.X, new MoveUpCommand(map));
			keyboardController.RegisterPressKey(Keys.C, new MoveDownCommand(map));
			keyboardController.RegisterPressKey(Keys.V, new MoveLeftCommand(map));
			keyboardController.RegisterPressKey(Keys.B, new MoveRightCommand(map));

            //Registers commands with MouseButtons for switching maps
            mouseController.RegisterQuadrant(leftDoorQuadrant, new MoveLeftCommand(map));
            mouseController.RegisterQuadrant(rightDoorQuadrant, new MoveRightCommand(map));
            mouseController.RegisterQuadrant(topDoorQuadrant, new MoveUpCommand(map));
            mouseController.RegisterQuadrant(bottomDoorQuadrant, new MoveDownCommand(map));

			//Registers commands with Keys for turning debug mode on and off
			keyboardController.RegisterPressKey(Keys.F, new DebugCommand(map));

        }

		protected override void Update(GameTime gameTime) {
			// Updates controllers
			foreach (IController controller in controllers) {
				controller.Update();
			}
			// Updates enemies
            enemyFactory.Update();
			// Updates npc's
            NPCFactory.Update();
			// Updates Link
            Link.Update();

			// Handles collisions
			collisionHandler.Update();

			base.Update(gameTime);
		}

		protected override void Draw(GameTime gameTime) {
			GraphicsDevice.Clear(Color.CornflowerBlue);

			_spriteBatch.Begin();
			// Draws map
			map.Draw(_spriteBatch);
            // Draws enemies
            enemyFactory.Draw(_spriteBatch);
			//Draws NPCs
			NPCFactory.Draw(_spriteBatch);
			//Draws objects
			itemFactory.Draw(_spriteBatch);
			// Draws player
			Link.Draw(_spriteBatch, Color.White);


			_spriteBatch.End();

			base.Draw(gameTime);
		}
	}
}
