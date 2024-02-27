using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using static MouseController;
public enum MouseButtons { Left, Right };

/*
 * Controller class for the Mouse
 */
public class MouseController : IController {
    private Dictionary<MouseButtons, ICommand> buttonCommands;
    private Dictionary<Rectangle, ICommand> quadrants;
    private ButtonState previousLeftButtonState = ButtonState.Released;

    // Constructor: Initializes the Dictionary with commands based off mouse buttons and quadrants
    public MouseController() {
		buttonCommands = new Dictionary<MouseButtons, ICommand>();
        quadrants = new Dictionary<Rectangle, ICommand>();
    }

	public void RegisterRightMouseButtonCommand(MouseButtons button, ICommand command) {
		buttonCommands.Add(button, command);
	}
    public void RegisterQuadrant(Rectangle quadrant, ICommand command)
    {
        quadrants.Add(quadrant, command);
    }

    public void Update()
    {
        MouseState mouseState = Mouse.GetState();
        foreach (var quadrant in quadrants.Keys)
        {
            // Checks if the mouse is in the quadrant and the left button is pressed
            if (quadrant.Contains(mouseState.X, mouseState.Y) && 
            mouseState.LeftButton == ButtonState.Pressed && 
            previousLeftButtonState == ButtonState.Released)
            {
                quadrants[quadrant].Execute();
            }
        }

        if (mouseState.RightButton == ButtonState.Pressed && buttonCommands.ContainsKey(MouseButtons.Right))
        {
            buttonCommands[MouseButtons.Right].Execute();
        }

        previousLeftButtonState = mouseState.LeftButton;
    }
}
