using Microsoft.Xna.Framework.Input;

namespace ZeldaGame {
	public class MouseHandler {
		MouseState oldState;
		MouseState newState;

		public MouseHandler() {
			oldState = new MouseState();
			newState = new MouseState();
			MouseButtons.Left.HasFlag(ButtonState.Pressed);
		}

		public void Update() {
			oldState = newState;
			newState = Mouse.GetState();
		}

		public bool IsPressed(Keys key) {
			return (oldState.LeftButton.HasFlag(ButtonState.Pressed) == false) && (newState.LeftButton.HasFlag(ButtonState.Pressed) == true);
		}

		public bool IsHeld(Keys key) {
			return (oldState.IsKeyDown(key) == true) && (newState.IsKeyDown(key) == true);
		}
	}
}
