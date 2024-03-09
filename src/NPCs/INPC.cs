using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ZeldaGame.NPCs {
	public interface INPC {
		void Update();

		void Draw(SpriteBatch spriteBatch, Vector2 scale);
        public Rectangle GetNPCHitBox();

		void DrawNPCQuote(SpriteBatch spriteBatch);

    }
}
