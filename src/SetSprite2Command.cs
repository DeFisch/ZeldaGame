using ZeldaGame;

/*
 *  SetSprite1Command class for changing Link to Sprite1
 */
public class SetSprite2Command : ICommand
{
    private Game1 MyGame;

    // Constructor
    public SetSprite2Command(Game1 myGame)
    {
        MyGame = myGame;
    }

    public void Execute()
    {
        MyGame.Link = new Sprite2(MyGame.sprite);
    }
}
