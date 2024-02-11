using static ZeldaGame.Player.PlayerStateMachine;

namespace ZeldaGame.Player.Commands
{
    public class SetWalkRightSpriteCommand : ICommand
    {
        private Game1 MyGame;

        // Constructor
        public SetWalkRightSpriteCommand(Game1 myGame)
        {
            MyGame = myGame;
        }

        public void Execute()
        {
            MyGame.Link.SetDirection(3);
            MyGame.Link.SetSprite(PlayerSpriteFactory.Instance.CreateWalkSprite(Direction.Right));
            MyGame.Link.Walk();
        }
    }
}
