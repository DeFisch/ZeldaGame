using Microsoft.Xna.Framework.Input;

namespace ZeldaGame.Controllers {
	public class KeyboardHandler {
		KeyboardState oldState;
		KeyboardState newState;

		public KeyboardHandler() {
			oldState = new KeyboardState();
			newState = new KeyboardState();

		}

		public void Update() {
			oldState = newState;
			newState = Keyboard.GetState();
		}

		public bool IsPressed(Keys key) {
			return (oldState.IsKeyDown(key) == false) && (newState.IsKeyDown(key) == true);
		}

		public bool IsHeld(Keys key) {
			return (oldState.IsKeyDown(key) == true) && (newState.IsKeyDown(key) == true);
		}
	}
}
