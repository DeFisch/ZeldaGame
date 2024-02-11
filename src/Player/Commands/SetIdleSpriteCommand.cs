namespace ZeldaGame.Player.Commands
{
    public class SetIdleSpriteCommand : ICommand
    {
        private Game1 MyGame;

        // Constructor
        public SetIdleSpriteCommand(Game1 myGame)
        {
            MyGame = myGame;
        }

        public void Execute()
        {
            MyGame.Link.Idle();
        }
    }
}