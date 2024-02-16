using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using ZeldaGame;

/*
 * Controller class for the Keyboard
 */
public class KeyboardController : IController
{
	private KeyboardHandler keyboardHandler;
	private Dictionary<Keys, ICommand> holdKeys;
	private Dictionary<Keys, ICommand> pressKeys;

	public KeyboardController() {
		keyboardHandler = new KeyboardHandler();
		holdKeys = new Dictionary<Keys, ICommand>();
		pressKeys = new Dictionary<Keys, ICommand>();

	}

	public void RegisterHoldKey(Keys key, ICommand command)
	{
		holdKeys.Add(key, command);
	}

    public void RegisterPressKey(Keys key, ICommand command)
    {
        pressKeys.Add(key, command);
    }

	public void Update()
	{

		Keys[] pressedKeys = Keyboard.GetState().GetPressedKeys();

		keyboardHandler.Update();

		foreach (Keys key in pressedKeys)
		{
			if (holdKeys.ContainsKey(key) && keyboardHandler.IsHeld(key))
			{
				holdKeys[key].Execute();
			}

			if (pressKeys.ContainsKey(key) && keyboardHandler.IsPressed(key))
			{
				pressKeys[key].Execute();
			}
		}
	}

}