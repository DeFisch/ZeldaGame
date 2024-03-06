using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeldaGame.Items {
	public class BlankItem : IItemSprite {
		private int counter = 0;
		private Texture2D texture;
		private Vector2 pos;
        private Rectangle sourceRectangle_1;
        private Rectangle sourceRectangle_2;
        private Rectangle destinationRectangle_1;
        private Rectangle destinationRectangle_2;
        public BlankItem(Texture2D texture, Vector2 pos) {
			this.texture = texture;
			this.pos = pos;

		}

		public void Draw(SpriteBatch spriteBatch, Vector2 location, Color color, Vector2 scale) {

		}

		public void Update() {

		}
        public Rectangle GetHitBox()
        {
            return destinationRectangle_1;
        }

        public void ItemAction()
        {

        }
    }
}
