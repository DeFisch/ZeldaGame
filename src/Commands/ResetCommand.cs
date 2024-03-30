namespace ZeldaGame
{
    public class ResetCommand: ICommand
    {
        private Game1 MyGame;

        public ResetCommand(Game1 game)
        {
            MyGame = game;
        }

        public void Execute()
        {
            MyGame.map.Reset();
            MyGame.Link.Reset();
            MyGame.itemFactory.Reset();
            MyGame.enemyFactory.Reset();
            MyGame.headUpDisplay.Reset();
            MyGame.blockSpriteFactory.Reset();
            MyGame.collisionHandler.itemActionHandler.Reset();
            Globals.audioLoader.Reset();
            Globals.gameStateScreenHandler.Reset();
        }
    }
}
