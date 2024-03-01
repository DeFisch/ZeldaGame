using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace ZeldaGame.Controllers;
public enum MouseButtons { Left, Right };
/*
 * Controller class for the Mouse
 */
public class MouseController : IController {
	private readonly MouseHandler mouseHandler;
	private Dictionary<MouseButtons, ICommand> holdButtons;
	private Dictionary<MouseButtons, ICommand> pressButtons;
    private Dictionary<Rectangle, ICommand> quadrants;

    // Constructor: Initializes the Dictionary with commands based off mouse buttons and quadrants
    public MouseController() {
		mouseHandler = new MouseHandler();
        holdButtons = new Dictionary<MouseButtons, ICommand>();
		pressButtons = new Dictionary<MouseButtons, ICommand>();
		quadrants = new Dictionary<Rectangle, ICommand>();
    }

	public void RegisterHoldButton(MouseButtons button, ICommand command) {
		holdButtons.Add(button, command);
	}
	public void RegisterPressButton(MouseButtons button, ICommand command) {
		pressButtons.Add(button, command);
	}
	public void RegisterQuadrant(Rectangle quadrant, ICommand command)
    {
        quadrants.Add(quadrant, command);
    }

    public void Update()
    {
		mouseHandler.Update();
		foreach (var quadrant in quadrants.Keys)
        {
            // Checks if the mouse is in the quadrant and the left button is pressed
            if (quadrant.Contains(Mouse.GetState().Position) && mouseHandler.IsPressed(MouseButtons.Left))
            {
                quadrants[quadrant].Execute();
            }
        }

        if (pressButtons.ContainsKey(MouseButtons.Right) && mouseHandler.IsPressed(MouseButtons.Right))
        {
            pressButtons[MouseButtons.Right].Execute();
        }
    }
}
