using ZeldaGame;

/*
 *  SetSprite1Command class for changing Link to Sprite1
 */
public class SetSprite4Command : ICommand
{
    private Game1 MyGame;

    // Constructor
    public SetSprite4Command(Game1 myGame)
    {
        MyGame = myGame;
    }

    public void Execute()
    {
        MyGame.Link = new Sprite4(MyGame.sprite);
    }
}
