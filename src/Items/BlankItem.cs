using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeldaGame.Items {
	public class BlankItem : IItemSprite {
		private Texture2D texture;
		private Vector2 pos;
		private String id;
        public BlankItem(Texture2D texture, Vector2 pos) {
			this.texture = texture;
			this.pos = pos;
			this.id = "Blank";
		}

		public void Draw(SpriteBatch spriteBatch, Vector2 location, Color color, Vector2 scale) {

		}

		public void Update() {

		}
        public Rectangle GetHitBox()
        {
            return new Rectangle();
        }

		public String GetID() {
			return id;
		}
        public void ItemAction()
        {

        }
    }
}
