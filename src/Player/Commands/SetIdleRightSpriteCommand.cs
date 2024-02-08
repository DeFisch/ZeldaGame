namespace ZeldaGame.Player.Commands
{
    public class SetIdleRightSpriteCommand : ICommand
    {
        private Game1 MyGame;

        // Constructor
        public SetIdleRightSpriteCommand(Game1 myGame)
        {
            MyGame = myGame;
        }

        public void Execute()
        {
            MyGame.Link.SetDirection(3);
			MyGame.Link.SetSprite(PlayerSpriteFactory.Instance.CreateIdleRightPlayer());
			MyGame.Link.Idle();
        }
    }
}
