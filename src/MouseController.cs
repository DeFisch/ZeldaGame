using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using static MouseController;
public enum MouseButtons { Left, Right };

/*
 * Controller class for the Mouse
 */
public class MouseController : IController {
    private Dictionary<MouseButtons, ICommand> buttonCommands;

	// Constructor: Initializes the Dictionary with commands based off mouse buttons
	public MouseController() {
		buttonCommands = new Dictionary<MouseButtons, ICommand>();
	}

	public void RegisterCommand(MouseButtons button, ICommand command) {
		buttonCommands.Add(button, command);
	}

    public void Update()
    {
        MouseState mouseState = Mouse.GetState();

        if (mouseState.LeftButton == ButtonState.Pressed && buttonCommands.ContainsKey(MouseButtons.Left))
        {
            buttonCommands[MouseButtons.Left].Execute();
        }

        if (mouseState.RightButton == ButtonState.Pressed && buttonCommands.ContainsKey(MouseButtons.Right))
        {
            buttonCommands[MouseButtons.Right].Execute();
        }
    }
}
