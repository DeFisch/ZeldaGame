using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ZeldaGame.Block;
using ZeldaGame.Controllers;
using ZeldaGame.Enemy;
using ZeldaGame.Enemy.Commands;
using ZeldaGame;
using ZeldaGame.HUD;
using ZeldaGame.Items;
using ZeldaGame.Map;
using ZeldaGame.Map.Commands;
using ZeldaGame.NPCs;
using ZeldaGame.Player;
using ZeldaGame.Player.Commands;

namespace ZeldaGame
{
    public class Game1 : Game {
		private GraphicsDeviceManager _graphics;
		private SpriteBatch _spriteBatch;

		public IPlayer Link;
		public PlayerSpriteFactory playerFactory;

		public Texture2D npcs;
		public NPCFactory NPCFactory;

		public Texture2D Items;
		public ItemSpriteFactory itemFactory;

		public EnemyFactory enemyFactory;
		public BlockSpriteFactory blockSpriteFactory;
		public CollisionHandler collisionHandler;

		public Texture2D HUD;
		public HeadUpDisplay headUpDisplay;
		public PlayerInfoHUD playerInfoHUD;

		private KeyboardController keyboardController;
		private MouseController mouseController;
		private List<IController> controllers;
		public MapHandler map;
		public Vector3 mapSize;
		public Vector2 mapScale;
		public Vector2 windowSize;
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

			// set fixed window size and map size
			windowSize = new Vector2(800, 725);
			mapSize = new Vector3(800, 550, 175);
            _graphics.PreferredBackBufferWidth = (int)windowSize.X;
			_graphics.PreferredBackBufferHeight = (int)windowSize.Y;
			_graphics.ApplyChanges();

			// Initialize collision handler
			collisionHandler = new CollisionHandler(this);
			// Initialize gameStateScreen handler
			Globals.gameStateScreenHandler = new GameStateScreenHandler(this);

			base.Initialize();
		}

		protected override void LoadContent() {
			_spriteBatch = new SpriteBatch(GraphicsDevice);

			// Load content
			npcs = Content.Load<Texture2D>("NPCs");
			Items = Content.Load<Texture2D>("Objects");
            font = Content.Load<SpriteFont>("Font");
			HUD = Content.Load<Texture2D>("HUD");

			// Load audio
			Globals.audioLoader = new AudioLoader(this);

			// Initialize map
			// Load default map
			Texture2D map_texture = Content.Load<Texture2D>("Level1_Map");
			map = new MapHandler(map_texture, this);
			mapScale = map.GetWindowScale(mapSize);

			// Initializes item classes
			PlayerSpriteFactory.Instance.LoadAllTextures(Content);
			PlayerItemSpriteFactory.Instance.LoadAllTextures(Content);
            Link = new Player1(new Vector2(mapSize.X / 2, (mapSize.X / 2) + mapSize.Z), mapScale);

			
			Texture2D[] enemy_texture = {Content.Load<Texture2D>("enemies"),Content.Load<Texture2D>("enemies_1")};
			blockSpriteFactory = new BlockSpriteFactory(map_texture, mapScale,mapSize,Link,map);
            NPCFactory = new NPCFactory(npcs, mapScale, font, map);
			itemFactory = new ItemSpriteFactory(Items, npcs, mapScale, Link, map);
			enemyFactory = new EnemyFactory(enemy_texture, mapScale, mapSize, itemFactory);
			headUpDisplay = new HeadUpDisplay(HUD,mapScale,mapSize);
			playerInfoHUD = new PlayerInfoHUD(HUD, mapScale, windowSize);
		

            // Define the quadrants based on the map size
            Rectangle leftDoorQuadrant = new Rectangle(0, (int)((mapSize.Y / 4) + mapSize.Z), (int)(mapSize.X / 4), (int)(mapSize.Y / 2));
            Rectangle rightDoorQuadrant = new Rectangle((int)(3 * mapSize.X / 4), (int)((mapSize.Y / 4) + mapSize.Z), (int)(mapSize.X / 4), (int)(mapSize.Y / 2));
            Rectangle topDoorQuadrant = new Rectangle((int)(mapSize.X / 4), (int)(mapSize.Z), (int)(mapSize.X / 2), (int)(mapSize.Y / 4));
            Rectangle bottomDoorQuadrant = new Rectangle((int)(mapSize.X / 4), (int)((3 * mapSize.Y / 4) + mapSize.Z), (int)(mapSize.X / 2), (int)(mapSize.Y / 4));

			//Add NPCs
			if (NPCFactory.isInDungeon())
			{
				NPCFactory.AddNPCs();
			}
            //Add Blocks
            blockSpriteFactory.AddBlock();
			//Add items
			//itemFactory.ObjectList();
			itemFactory.GetMapItems();

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

			//Registers commands with Keys for Reset
			keyboardController.RegisterPressKey(Keys.R, new ResetCommand(this));

            //Registers command with Key to start game
            keyboardController.RegisterPressKey(Keys.Space, new StartGameCommand(this));

            //Registers commands with Keys and MouseButton for Quit
            keyboardController.RegisterPressKey(Keys.Q, new QuitCommand(this));
            mouseController.RegisterPressButton(MouseButtons.Right, new QuitCommand(this));

            //Registers commands with MouseButtons for switching maps
            mouseController.RegisterQuadrant(leftDoorQuadrant, new MoveLeftCommand(map));
            mouseController.RegisterQuadrant(rightDoorQuadrant, new MoveRightCommand(map));
            mouseController.RegisterQuadrant(topDoorQuadrant, new MoveUpCommand(map));
            mouseController.RegisterQuadrant(bottomDoorQuadrant, new MoveDownCommand(map));

			//Registers commands with Keys for turning debug mode on and off
			keyboardController.RegisterPressKey(Keys.F, new DebugCommand(map));

			//Registers commands with Keys for muting and unmuting the audio
			keyboardController.RegisterPressKey(Keys.M, new MuteCommand(Globals.audioLoader));

            //Registers commands with Keys for displaying HUD
            keyboardController.RegisterPressKey(Keys.H, new DisplayHUDCommand(this));

			//Registers commands with Keys for pausing and unpausing the game
			keyboardController.RegisterPressKey(Keys.P, new PauseCommand(this));
        }

        protected override void Update(GameTime gameTime) {
			// Updates controllers
			foreach (IController controller in controllers) {
				controller.Update();
			}
			if (!Globals.gameStateScreenHandler.IsPlaying())
			{
				Globals.gameStateScreenHandler.Update();
				return;
			}
            // Updates enemies
            enemyFactory.Update();
			//Updates blocks
			blockSpriteFactory.Update();
			// Updates npc's
			if (NPCFactory.isInDungeon()) { 
				NPCFactory.Update();
			}
			// Updates Link
            Link.Update();

			// Handles collisions
			collisionHandler.Update();

			base.Update(gameTime);
		}

		protected override void Draw(GameTime gameTime) {
			_spriteBatch.Begin();
            //Draws player info HUD
            playerInfoHUD.Draw(_spriteBatch);
            if (!Globals.gameStateScreenHandler.IsPlaying())
			{
                Globals.gameStateScreenHandler.Draw(_spriteBatch);
                //Draws HUD
                if (headUpDisplay.isVisible())
					headUpDisplay.Draw(_spriteBatch);
				_spriteBatch.End();
				return;
			}
			GraphicsDevice.Clear(Color.CornflowerBlue);
            // Draws map
            map.Draw(_spriteBatch);
			// Draws Blocks
			blockSpriteFactory.Draw(_spriteBatch);
            // Draws enemies
            enemyFactory.Draw(_spriteBatch);
			//Draws NPCs
			if (NPCFactory.isInDungeon())
			{
				NPCFactory.Draw(_spriteBatch);
			}
			//Draws objects
			if (!itemFactory.IsMapChanged())
			{
				itemFactory.Draw(_spriteBatch);
			}
			// Draws player
			Link.Draw(_spriteBatch, Color.White);

            _spriteBatch.End();

			base.Draw(gameTime);
		}

		public void PauseGame() {
			if (Globals.gameStateScreenHandler.IsPlaying())
			{
				Globals.audioLoader.Mute();
				Globals.gameStateScreenHandler.CurrentGameState = GameState.Pause;
			}
		}

		public void ResumeGame() {
			if (Globals.gameStateScreenHandler.CurrentGameState == GameState.Pause)
			{
				Globals.audioLoader.Mute();
				Globals.gameStateScreenHandler.CurrentGameState = GameState.Playing;
			}
		}

		public bool IsPaused() {
			return !Globals.gameStateScreenHandler.IsPlaying();
		}
	}
}
