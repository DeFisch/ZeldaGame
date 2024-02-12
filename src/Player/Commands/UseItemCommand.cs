namespace ZeldaGame.Player.Commands
{
    public class UseItemCommand : ICommand
    {
        private Game1 MyGame;

        public UseItemCommand(Game1 myGame)
        {
            MyGame = myGame;
        }

        public void Execute()
        {
            MyGame.Link.UseItem();
        }
    }
}
