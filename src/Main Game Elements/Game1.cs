using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ZeldaGame.Block;
using ZeldaGame.Commands;
using ZeldaGame.Controllers;
using ZeldaGame.Enemy;
using ZeldaGame.HUD;
using ZeldaGame.HUD.Commands;
using ZeldaGame.Items;
using ZeldaGame.Map;
using ZeldaGame.Map.Commands;
using ZeldaGame.NPCs;
using ZeldaGame.Player;
using ZeldaGame.Player.Commands;
using static ZeldaGame.Globals;

namespace ZeldaGame
{
    public class Game1 : Game {
		private GraphicsDeviceManager _graphics;
		private SpriteBatch _spriteBatch;

		public IPlayer Link;
		public PlayerEnemy playerEnemy;
		public PlayerSpriteFactory playerFactory;

		public Texture2D npcs;
		public NPCFactory NPCFactory;

		public Texture2D Items;
		public ItemSpriteFactory itemFactory;

		public EnemyFactory enemyFactory;
		public PushableBlockHandler pushableBlockHandler;
		public CollisionHandler collisionHandler;

		public Texture2D HUD;
		private FlashlightOverlay flashlight;
		public HeadUpDisplay headUpDisplay;
		public PlayerInfoHUD playerInfoHUD;
		public bool flashlightMode = false;

		public KeyboardController keyboardController;
		private MouseController mouseController;
		private EnemyController enemyController;
		private List<IController> controllers;
		public MapHandler map;
		public Vector3 mapSize;
		public Vector2 mapScale;
		public Vector2 windowSize;
		public SpriteFont font;
		public int level = 0;

        public Game1() {
			_graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
			IsMouseVisible = true;
		}

		protected override void Initialize() {

            // Initialize controllers
            keyboardController = new KeyboardController();
            mouseController = new MouseController();
			enemyController = new EnemyController();
			controllers = new List<IController> { keyboardController, mouseController, enemyController };

			// set fixed window size and map size
			windowSize = new Vector2(800, 725);
			mapSize = new Vector3(800, 550, 175);
            _graphics.PreferredBackBufferWidth = (int)windowSize.X;
			_graphics.PreferredBackBufferHeight = (int)windowSize.Y;
			_graphics.ApplyChanges();

			// Initialize collision handler
			collisionHandler = new CollisionHandler(this);

			base.Initialize();
		}

		protected override void LoadContent() {
			_spriteBatch = new SpriteBatch(GraphicsDevice);

			// Load content
			npcs = Content.Load<Texture2D>("NPCs");
			Items = Content.Load<Texture2D>("Objects");
            font = Content.Load<SpriteFont>("Font");
			HUD = Content.Load<Texture2D>("HUD");

            // Initialize gameStateScreen handler
            Globals.gameStateScreenHandler = new GameStateScreenHandler(this);

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
            Link = new PlayerMain(new Vector2(mapSize.X / 2, (mapSize.X / 2) + mapSize.Z), mapScale);
            playerEnemy = new PlayerEnemy(Content.Load<Texture2D>("enemies"), new Vector2(375, 275), mapScale);

            // the stuff we mask out of darkness, essentially this texture defines our lights
            Texture2D alphaMask = Content.Load<Texture2D>("light");
			flashlight = new FlashlightOverlay(this, alphaMask);
			
			Texture2D[] enemy_texture = {Content.Load<Texture2D>("enemies"),Content.Load<Texture2D>("enemies_1")};
            pushableBlockHandler = new PushableBlockHandler(map_texture, mapScale,mapSize,Link,map);
			itemFactory = new ItemSpriteFactory(Items, npcs, mapScale, Link, map);
            NPCFactory = new NPCFactory(npcs, Items, mapScale, font, map, itemFactory);
            enemyFactory = new EnemyFactory(enemy_texture, mapScale, mapSize, itemFactory);
            headUpDisplay = new HeadUpDisplay(HUD, mapScale, map, collisionHandler, Link, font);
			playerInfoHUD = new PlayerInfoHUD(HUD, mapScale, map, font, collisionHandler, Link, headUpDisplay);
		

            // Define the quadrants based on the map size
            Rectangle leftDoorQuadrant = new Rectangle(0, (int)((mapSize.Y / 4) + mapSize.Z), (int)(mapSize.X / 4), (int)(mapSize.Y / 2));
            Rectangle rightDoorQuadrant = new Rectangle((int)(3 * mapSize.X / 4), (int)((mapSize.Y / 4) + mapSize.Z), (int)(mapSize.X / 4), (int)(mapSize.Y / 2));
            Rectangle topDoorQuadrant = new Rectangle((int)(mapSize.X / 4), (int)(mapSize.Z), (int)(mapSize.X / 2), (int)(mapSize.Y / 4));
            Rectangle bottomDoorQuadrant = new Rectangle((int)(mapSize.X / 4), (int)((3 * mapSize.Y / 4) + mapSize.Z), (int)(mapSize.X / 2), (int)(mapSize.Y / 4));

            //Add Blocks
            pushableBlockHandler.AddBlock();
			//Add items
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

			// // Use items
			// keyboardController.RegisterPressKey(Keys.D1, new UseItemCommand(this, PlayerProjectiles.WoodenArrow));
			// keyboardController.RegisterPressKey(Keys.D2, new UseItemCommand(this, PlayerProjectiles.BlueArrow));
			// keyboardController.RegisterPressKey(Keys.D3, new UseItemCommand(this, PlayerProjectiles.Boomerang));
			// keyboardController.RegisterPressKey(Keys.D4, new UseItemCommand(this, PlayerProjectiles.BlueBoomerang));
			// keyboardController.RegisterPressKey(Keys.D5, new UseItemCommand(this, PlayerProjectiles.Bomb));
			// keyboardController.RegisterPressKey(Keys.D6, new UseItemCommand(this, PlayerProjectiles.Fireball));

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
			keyboardController.RegisterPressKey(Keys.F, new FlashlightCommand(this));

			//Registers commands with Keys for muting and unmuting the audio
			keyboardController.RegisterPressKey(Keys.M, new MuteCommand(Globals.audioLoader));

            //Registers commands with Keys for displaying HUD
            keyboardController.RegisterPressKey(Keys.H, new DisplayHUDCommand(this));

			//Registers commands with Keys for pausing and unpausing the game
			keyboardController.RegisterPressKey(Keys.P, new PauseCommand(this));

			//Registers commands with Keys for HUD weapon selection
            keyboardController.RegisterPressKey(Keys.A, new HUDInventoryCommandLast(this));
            keyboardController.RegisterPressKey(Keys.D, new HUDInventoryCommandNext(this));

			//Registers commands with keys for HUD room selection
            keyboardController.RegisterPressKey(Keys.Left, new HUDCommandLastRoom(this));
            keyboardController.RegisterPressKey(Keys.Right, new HUDCommandNextRoom(this));

			//Registers commands with keys for HUD teleport
			keyboardController.RegisterPressKey(Keys.Enter, new HUDTeleportCommand(this));

			// Register enemy to controller
			enemyController.RegisterPlayer(playerEnemy);
			// Register Keys for enemy player
			enemyController.RegisterMovementKey(Keys.I, new Vector2(0, -1));
            enemyController.RegisterMovementKey(Keys.K, new Vector2(0, 1));
            enemyController.RegisterMovementKey(Keys.J, new Vector2(-1, 0));
            enemyController.RegisterMovementKey(Keys.L, new Vector2(1, 0));
        }

        protected override void Update(GameTime gameTime) {
			// Updates controllers
			foreach (IController controller in controllers) {
				controller.Update();
			}
			if (!gameStateScreenHandler.IsPlaying())
			{
				gameStateScreenHandler.Update();
				return;
			}
            // Updates enemies
            enemyFactory.Update();
			//Updates blocks
			pushableBlockHandler.Update();
			// Updates npc's
			if (NPCFactory.IsInNPCRoom(map.getMapXY())) { 
				NPCFactory.Update();
			}
			// Updates Link
            Link.Update();
			if (level == 3) playerEnemy.Update();

			// Handles collisions
			collisionHandler.Update();

			//Update HUD
			headUpDisplay.Update();

			// Checks if end game
            if (gameStateScreenHandler.GameOver())
                gameStateScreenHandler.EndGame();
				
			//Use items based on current choice
			keyboardController.RegisterPressKeySwitch(Keys.B, new UseItemCommand(this, headUpDisplay.HUDInventory.CurrentEquip()));
            base.Update(gameTime);
		}

		protected override void Draw(GameTime gameTime) {
				// this rendertarget will hold a black rectangle that gets masked with alphaMask
				RenderTarget2D darkness = null;

				if (gameStateScreenHandler.IsPlaying() && flashlightMode) {
				darkness = new RenderTarget2D(GraphicsDevice, (int)windowSize.X, (int)windowSize.Y);
				// prepare the darkness
				GraphicsDevice.SetRenderTarget(darkness);
				GraphicsDevice.Clear(new Color(0 ,0 ,0 ,250)); // the 120 here is the darkness "intensity"

				var blend = new BlendState();
				blend.AlphaSourceBlend = Blend.Zero;
				blend.AlphaDestinationBlend = Blend.InverseSourceColor;
				blend.ColorSourceBlend = Blend.Zero;
				blend.ColorDestinationBlend = Blend.InverseSourceColor;
				_spriteBatch.Begin(blendState: blend);
				flashlight.Draw(_spriteBatch);
				_spriteBatch.End();
				GraphicsDevice.SetRenderTarget(null);
			}


			_spriteBatch.Begin();
            if (!gameStateScreenHandler.IsPlaying())
			{
                gameStateScreenHandler.Draw(_spriteBatch);
                //Draws HUD
                if (headUpDisplay.isVisible())
					headUpDisplay.Draw(_spriteBatch);
				_spriteBatch.End();
				return;
			}

            // Draws map
            map.Draw(_spriteBatch);
			// Draws Blocks
			pushableBlockHandler.Draw(_spriteBatch);
            // Draws enemies
            enemyFactory.Draw(_spriteBatch);
			//Draws NPCs
			if (NPCFactory.IsInNPCRoom(map.getMapXY()))
			{
				NPCFactory.Draw(_spriteBatch);
			}
			//Draws objects
			if (!itemFactory.IsMapChanged())
			{
				itemFactory.Draw(_spriteBatch);
			}
			// Draws players
			Link.Draw(_spriteBatch, Color.White);
			if (level == 3) playerEnemy.Draw(_spriteBatch, Color.White);

			if (Globals.gameStateScreenHandler.GameOver())
                GraphicsDevice.Clear(Color.Black);
			if (gameStateScreenHandler.IsPlaying())

			// draw the masked darkness!
			if (flashlightMode)
			_spriteBatch.Draw(darkness, Vector2.Zero, Color.White);


            //Draws player info HUD
            playerInfoHUD.Draw(_spriteBatch, false);
			_spriteBatch.End();

			base.Draw(gameTime);
		}
	}
}
