using Microsoft.Xna.Framework;

namespace ZeldaGame;

public class CollisionHandler{
    private Game1 game;
    public CollisionHandler(Game1 game) {
        this.game = game;
    }

    public void Update() {
        game.map.PlayerDoorCollision(new Vector2(game.window_width, game.window_height), game.Link);
    }
}