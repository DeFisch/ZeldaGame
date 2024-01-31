using ZeldaGame;

/*
 *  SetSprite1Command class for changing Link to Sprite1
 */
public class SetSprite1Command : ICommand
{
    private Game1 MyGame;

    // Constructor
    public SetSprite1Command(Game1 myGame)
    {
        MyGame = myGame;
    }

    public void Execute()
    {
        MyGame.Link = new Sprite1(MyGame.sprite);
    }
}
