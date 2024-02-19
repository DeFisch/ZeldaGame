using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ZeldaGame.Player.PlayerStateMachine;

namespace ZeldaGame.Player {
	public interface IPlayer {

		public void Attack();
		public void Idle();
		public void Walk();
		public void UseItem(int item);
		public void SetDirection(Direction direction); 
		public void TakeDamage();
		public void Draw(SpriteBatch spriteBatch, Color color);
		public void Update();
		public void Reset();
	}
}