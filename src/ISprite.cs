using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

/*
 * Interface for ISprite
 */
public interface ISprite {
	/*
     * Updates the frames and location of the sprite
     */
	void Update();

	/*
     *  Draws the location of the sprite in the location passed, given a color to recolor the sprite
     */
	void Draw(SpriteBatch spriteBatch, Vector2 location, Color color);

}