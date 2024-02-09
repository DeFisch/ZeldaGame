using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Data.Common;
using ZeldaGame;

/*
 * Controller class for the Keyboard
 */
public class KeyboardController : IController
{
    private Dictionary<KeyValuePair<Keys, ICommand>, int> controllerMappings;
    private KeyboardHandler keyboardHandler;

    // Constructor: Initializes the commands based off the keys pressed
    public KeyboardController()
    {
        controllerMappings = new Dictionary<KeyValuePair<Keys, ICommand>, int>();
		keyboardHandler = new KeyboardHandler();
    }

    public void RegisterCommand(Keys identifier, ICommand command, int commandType) // 0 = isPressed, 1 = isHeld, 2 = isRelease
	{
		controllerMappings.Add(new KeyValuePair<Keys, ICommand>(identifier, command), commandType);
	}

	public void Update()
    {
        keyboardHandler.Update();

        foreach (KeyValuePair<KeyValuePair<Keys, ICommand>, int> keyCommandTypePair in controllerMappings) {
            switch (keyCommandTypePair.Value) {
                case 0:
                    if (keyboardHandler.IsPressed(keyCommandTypePair.Key.Key)) {
                        Pressed(keyCommandTypePair.Key);
                    }
                    break;
                case 1:
                    if (keyboardHandler.IsHeld(keyCommandTypePair.Key.Key)) {
                        Held(keyCommandTypePair.Key);
                    }
                    break;
                case 2:
                    if (keyboardHandler.IsReleased(keyCommandTypePair.Key.Key)) {
                        Released(keyCommandTypePair.Key);
                    }
                    break;
                default:
                    break;
            }
        }
    }

    public void Pressed(KeyValuePair<Keys, ICommand> keyPair) {
        keyPair.Value.Execute(); 
    }

	public void Held(KeyValuePair<Keys, ICommand> keyPair) {
		keyPair.Value.Execute();
	}

	public void Released(KeyValuePair<Keys, ICommand> keyPair) {
		keyPair.Value.Execute();
	}
}
