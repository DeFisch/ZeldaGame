namespace ZeldaGame.Commands;

public class ResetCommand: ICommand
{
    private Game1 MyGame;

    public ResetCommand(Game1 game)
    {
        MyGame = game;
    }

    public void Execute()
    {
        MyGame.Link.Reset();
        MyGame.map.Reset();
        MyGame.Link.Reset();
        MyGame.itemFactory.Reset();
        MyGame.enemyFactory.Reset();
        MyGame.headUpDisplay.Reset();
        MyGame.headUpDisplay.HUDInventory.Reset();
        MyGame.collisionHandler.itemActionHandler.Reset();
        Globals.audioLoader.Reset();
        Globals.gameStateScreenHandler.Reset();
    }
}
