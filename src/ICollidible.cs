using System.Drawing;

namespace ZeldaGame;
/*
 * Interface for ICollibilde
 */
public interface ICollidible {
	public Rectangle GetHitBox();
	public void OnCollision(Rectangle intersect);
}
