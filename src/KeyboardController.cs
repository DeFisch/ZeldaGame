using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using ZeldaGame;

/*
 * Controller class for the Keyboard
 */
public class KeyboardController : IController<Keys>
{
    private Dictionary<Keys, ICommand> controllerMappings;

    // Constructor: Initializes the commands based off the keys pressed
    public KeyboardController()
    {
        controllerMappings = new Dictionary<Keys, ICommand>();
    }

    public void RegisterCommand(Keys identifier, ICommand command)
    {
        controllerMappings.Add(identifier, command);
    }

    public void Update()
    {
        Keys[] pressedKeys = Keyboard.GetState().GetPressedKeys();

        foreach (Keys key in pressedKeys)
        {
            controllerMappings[key].Execute();
        }
    }
}
