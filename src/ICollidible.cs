using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ZeldaGame;
/*
 * Interface for ICollibilde
 */
public interface ICollidible {
	public Rectangle GetHitBox();
	public void OnCollision(Rectangle intersect);
}
