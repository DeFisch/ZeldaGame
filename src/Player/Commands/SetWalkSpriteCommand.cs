namespace ZeldaGame.Player.Commands
{
    public class SetWalkSpriteCommand : ICommand
    {
        private Game1 MyGame;
        private int direction;

        // Constructor
        public SetWalkSpriteCommand(Game1 myGame, int direction)
        {
            MyGame = myGame;
            this.direction = direction;
        }

        public void Execute()
        {
            MyGame.Link.SetDirection(direction);
            MyGame.Link.Walk();
        }
    }
}