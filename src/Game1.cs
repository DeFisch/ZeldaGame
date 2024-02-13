using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Sprint0.Block;
using ZeldaGame.Enemy;
using ZeldaGame.Items;
using ZeldaGame.NPCs;
using ZeldaGame.Player;
using ZeldaGame.Player.Commands;
using System.Data;
using ZeldaGame.Enemy.Commands;

namespace ZeldaGame {
	public class Game1 : Game {
		private int window_width = 800;
		private int window_height = 600;
		private GraphicsDeviceManager _graphics;
		private SpriteBatch _spriteBatch;

		public Player1 Link;
		public PlayerSpriteFactory playerFactory;
		public WeaponHandler weaponHandler;

		public Texture2D npcs;
		public NPCFactory NPCFactory;

		public Texture2D Objects;
		private YellowRuby blueRuby;
		public ItemSpriteFactory objectFactory;

		public EnemyFactory enemyFactory;

		public BlockSpriteFactory blockSpriteFactory;

		private KeyboardController keyboardController;
		private MouseController mouseController;
		private List<IController> controllers;

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
			_graphics.PreferredBackBufferWidth = this.window_width;
			_graphics.PreferredBackBufferHeight = this.window_height;
			_graphics.ApplyChanges();

			base.Initialize();
		}

		protected override void LoadContent() {
			_spriteBatch = new SpriteBatch(GraphicsDevice);

			// Load content
			npcs = Content.Load<Texture2D>("NPCs");
			Objects = Content.Load<Texture2D>("Objects");


			// Initializes object classes
			PlayerSpriteFactory.Instance.LoadAllTextures(Content);
			PlayerItemSpriteFactory.Instance.LoadAllTextures(Content);
			weaponHandler = new WeaponHandler();
			Link = new Player1(new Vector2(window_width, window_height), weaponHandler);
			NPCFactory = new NPCFactory(npcs, new Vector2(window_width, window_height));
			objectFactory = new ItemSpriteFactory(Objects);

			Texture2D enemies = Content.Load<Texture2D>("enemies");
			blockSpriteFactory = new BlockSpriteFactory(Content.Load<Texture2D>("Level1_Map"));
			blueRuby = new YellowRuby(Objects, new Vector2(300, 150));
			// Initializes object classes


			enemyFactory = new EnemyFactory(enemies, window_size: new Vector2(window_width, window_height));
			Random random = new Random();
			for (int i = 0; i < 5; i++) // spawn 5 enemies of each type
			{
				enemyFactory.AddEnemy("Stalfos");
			}

			//Add NPCs
			NPCFactory.AddNPCs();
			//Add Blocks
			blockSpriteFactory.AddBlocks("Stair");
			blockSpriteFactory.AddBlocks("Walls");
			blockSpriteFactory.AddBlocks("Ground");
			blockSpriteFactory.AddBlocks("Obstacle");
			//Add items
			objectFactory.ObjectList();

			// Registers commands with Keys as the identifier
			keyboardController.RegisterCommand(Keys.W, new SetWalkSpriteCommand(this, 0), 0);
			keyboardController.RegisterCommand(Keys.W, new SetIdleSpriteCommand(this), 2);
			keyboardController.RegisterCommand(Keys.Up, new SetWalkSpriteCommand(this, 0), 0);
			keyboardController.RegisterCommand(Keys.Up, new SetIdleSpriteCommand(this), 2);

			keyboardController.RegisterCommand(Keys.A, new SetWalkSpriteCommand(this, 1), 0);
			keyboardController.RegisterCommand(Keys.A, new SetIdleSpriteCommand(this), 2);
			keyboardController.RegisterCommand(Keys.Left, new SetWalkSpriteCommand(this, 1), 0);
			keyboardController.RegisterCommand(Keys.Left, new SetIdleSpriteCommand(this), 2);

			keyboardController.RegisterCommand(Keys.S, new SetWalkSpriteCommand(this, 2), 0);
			keyboardController.RegisterCommand(Keys.S, new SetIdleSpriteCommand(this), 2);
			keyboardController.RegisterCommand(Keys.Down, new SetWalkSpriteCommand(this, 2), 0);
			keyboardController.RegisterCommand(Keys.Down, new SetIdleSpriteCommand(this), 2);

			keyboardController.RegisterCommand(Keys.D, new SetWalkSpriteCommand(this, 3), 0);
			keyboardController.RegisterCommand(Keys.D, new SetIdleSpriteCommand(this), 2);
			keyboardController.RegisterCommand(Keys.Right, new SetWalkSpriteCommand(this, 3), 0);
			keyboardController.RegisterCommand(Keys.Right, new SetIdleSpriteCommand(this), 2);

			// Attack
			keyboardController.RegisterCommand(Keys.Z, new AttackCommand(this), 0);
			keyboardController.RegisterCommand(Keys.N, new AttackCommand(this), 0);

			// Use items
			keyboardController.RegisterCommand(Keys.D1, new UseItemCommand(this, 0), 0);
			keyboardController.RegisterCommand(Keys.D2, new UseItemCommand(this, 1), 0);
			keyboardController.RegisterCommand(Keys.D3, new UseItemCommand(this, 2), 0);
			keyboardController.RegisterCommand(Keys.D4, new UseItemCommand(this, 3), 0);
			keyboardController.RegisterCommand(Keys.D5, new UseItemCommand(this, 4), 0);
			keyboardController.RegisterCommand(Keys.D6, new UseItemCommand(this, 5), 0);

			//Registers commands with Keys for blocks
			keyboardController.RegisterCommand(Keys.T, new NextBlockCommand(this), 0);
			keyboardController.RegisterCommand(Keys.Y, new PreviousBlockCommand(this), 0);

			//Register commands with keys for items
			keyboardController.RegisterCommand(Keys.I, new NextItemCommand(this), 0);
			keyboardController.RegisterCommand(Keys.U, new LastItemCommand(this), 0);

			
			keyboardController.RegisterCommand(Keys.O, new NextNPCCommand(this), 0);
			keyboardController.RegisterCommand(Keys.P, new PreviousNPCCommand(this), 0);

			//Registers commands with Keys for enemies
			keyboardController.RegisterCommand(Keys.O, new nextEnemyCommand(this), 0);
			keyboardController.RegisterCommand(Keys.P, new previousEnemyCommand(this), 0);

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

		protected override void Update(GameTime gameTime) {
			if (Keyboard.GetState().IsKeyDown(Keys.D0) || Mouse.GetState().RightButton == ButtonState.Pressed) {
				Exit();
			}

			foreach (IController controller in controllers) {
				controller.Update();
			}

			Link.Update();

			weaponHandler.Update();

			base.Update(gameTime);
			enemyFactory.Update();
			NPCFactory.Update();
		}

		protected override void Draw(GameTime gameTime) {
			GraphicsDevice.Clear(Color.CornflowerBlue);

			_spriteBatch.Begin();

			// Draws enemies
			enemyFactory.Draw(_spriteBatch);

			//Draws NPCs
			NPCFactory.Draw(_spriteBatch);

			//Draws Blocks
			blockSpriteFactory.Draw(_spriteBatch);

			//Draws objects
			objectFactory.Draw(_spriteBatch);

			// Draws player
			Link.Draw(_spriteBatch);

			// Draws player items
			weaponHandler.Draw(_spriteBatch);

			_spriteBatch.End();

			base.Draw(gameTime);
		}
	}
}
