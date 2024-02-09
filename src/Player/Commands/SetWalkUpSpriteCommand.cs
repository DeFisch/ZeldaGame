﻿namespace ZeldaGame.Player.Commands {
	public class SetWalkUpSpriteCommand : ICommand {
		private Game1 MyGame;

		// Constructor
		public SetWalkUpSpriteCommand(Game1 myGame) {
			MyGame = myGame;
		}

		public void Execute() {
			MyGame.Link.SetDirection(0);
			MyGame.Link.SetSprite(PlayerSpriteFactory.Instance.CreateWalkUpSprite());
			MyGame.Link.Walk();
		}
	}
}
