using Microsoft.Xna.Framework.Input;

namespace ZeldaGame.Controllers
{
    public class MouseHandler
    {
        MouseState oldState;
        MouseState newState;

        public MouseHandler()
        {
            oldState = new MouseState();
            newState = new MouseState();
        }

        public void Update()
        {
            oldState = newState;
            newState = Mouse.GetState();
        }
        public bool IsPressed(MouseButtons button) {
			switch (button) {
				case MouseButtons.Left:
					return oldState.LeftButton == ButtonState.Released && newState.LeftButton == ButtonState.Pressed;
				case MouseButtons.Right:
					return oldState.RightButton == ButtonState.Released && newState.RightButton == ButtonState.Pressed;
				default:
					return false;
			}
		}

		public bool IsHeld(MouseButtons button) {
			switch (button) {
				case MouseButtons.Left:
					return oldState.LeftButton == ButtonState.Pressed && newState.LeftButton == ButtonState.Pressed;
				case MouseButtons.Right:
					return oldState.RightButton == ButtonState.Pressed && newState.RightButton == ButtonState.Pressed;
				default:
					return false;
			}
		}
	}
}
