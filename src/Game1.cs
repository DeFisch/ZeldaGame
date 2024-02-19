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

		public Texture2D npcs;
		public NPCFactory NPCFactory;

		public Texture2D Objects;
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
            Link = new Player1(new Vector2(window_width, window_height));

            NPCFactory = new NPCFactory(npcs, new Vector2(window_width, window_height));
			objectFactory = new ItemSpriteFactory(Objects, npcs);

			Texture2D[] enemies = {Content.Load<Texture2D>("enemies"),Content.Load<Texture2D>("enemies_1")};
			blockSpriteFactory = new BlockSpriteFactory(Content.Load<Texture2D>("Level1_Map"));
			enemyFactory = new EnemyFactory(enemies, window_size: new Vector2(window_width, window_height));
			Random random = new Random();
			for (int i = 0; i < 5; i++) // spawn 5 enemies of each type
			{
				enemyFactory.AddEnemy("Stalfos");
			}

			//Add NPCs
			NPCFactory.AddNPCs();
			//Add Blocks
			blockSpriteFactory.AddBlock();
			//Add items
			objectFactory.ObjectList();

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
		}

        public void setSprite(ISprite sprite) {
			this.Link.SetSprite(sprite);
		}

		protected override void Update(GameTime gameTime) {
			if (Keyboard.GetState().IsKeyDown(Keys.Q) || Mouse.GetState().RightButton == ButtonState.Pressed) {
				Exit();
			}

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

			base.Update(gameTime);
		}

		protected override void Draw(GameTime gameTime) {
			GraphicsDevice.Clear(Color.CornflowerBlue);

			_spriteBatch.Begin();

            //Draws Blocks
            blockSpriteFactory.Draw(_spriteBatch);
            // Draws enemies
            enemyFactory.Draw(_spriteBatch);
			//Draws NPCs
			NPCFactory.Draw(_spriteBatch);
			//Draws objects
			objectFactory.Draw(_spriteBatch);
			// Draws player
			Link.Draw(_spriteBatch);;

			_spriteBatch.End();

			base.Draw(gameTime);
		}
	}
}
