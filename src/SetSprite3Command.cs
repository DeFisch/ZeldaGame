using ZeldaGame;

/*
 *  SetSprite1Command class for changing Link to Sprite1
 */
public class SetSprite3Command : ICommand
{
    private Game1 MyGame;

    // Constructor
    public SetSprite3Command(Game1 myGame)
    {
        MyGame = myGame;
    }

    public void Execute()
    {
        MyGame.Link = new Sprite3(MyGame.sprite);
    }
}
