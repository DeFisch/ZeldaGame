using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

/*
 * Controller class for the Mouse
 */
public class MouseController : IController
{
    private Dictionary<Rectangle, ICommand> quadrants;

    // Constructor: Initializes the Dictionary with commands based off the 4 quadrants
    public MouseController()
    {
        quadrants = new Dictionary<Rectangle, ICommand>();
    }

    public void RegisterCommand(Rectangle identifier, ICommand command)
    {
        quadrants.Add(identifier, command);
    }

    public void Update()
    {
        MouseState mouseState = Mouse.GetState();
        
        foreach (Rectangle rectangle in quadrants.Keys)
        {
            // Checks if the mouse is in the quadrant and the left button is pressed
            if (rectangle.Contains(mouseState.X, mouseState.Y) && mouseState.LeftButton == ButtonState.Pressed)
            {
                quadrants[rectangle].Execute();
            }
        }
    }
}
