namespace ZeldaGame.Commands;

public class MuteCommand : ICommand
{
    private readonly AudioLoader audioLoader;
    public MuteCommand(AudioLoader audioLoader)
    {
        this.audioLoader = audioLoader;
    }
    public void Execute()
    {
        audioLoader.Mute();
    }
}