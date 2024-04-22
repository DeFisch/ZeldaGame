using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ZeldaGame.NPCs {
	public interface INPC {
		void Update();

		void Draw(SpriteBatch spriteBatch, SpriteFont font);
        public Rectangle GetNPCHitBox();
    }
}
