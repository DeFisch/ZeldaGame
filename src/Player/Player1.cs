using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeldaGame.Player {
	public class Player1 : IPlayer {
		PlayerStateMachine stateMachine;
		ISprite sprite = PlayerSpriteFactory.Instance.CreateIdleDownPlayer();

		public Player1(Texture2D texture, Vector2 window_size) {
			stateMachine = new PlayerStateMachine();
		}

		public void SetDirection(int direction) {// 0 = up, 1 = left, 2 = down, 3 = right
			stateMachine.SetDirection(direction);
		}

		public void Draw(SpriteBatch spriteBatch) {
			stateMachine.Draw(spriteBatch);
		}

		public void Update() {
			
		}
	}
}