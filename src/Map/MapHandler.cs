using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ZeldaGame.Map;

public class MapHandler {
    /*
        Note here x denotes the row and y denotes the column
    */
    private MapLoader mapLoader;
    private string[,] map;
    private Texture2D map_texture;
    private Vector2 window_size;
    private int x = 2, y = 5;
    public MapHandler(Texture2D map_texture, Vector2 window_size) {
        mapLoader = new MapLoader();
        this.map_texture = map_texture;
        this.window_size = window_size;
        mapLoader.load_map(x, y); // load default map
        map = mapLoader.get_map(); // get default map info
    }
    public Rectangle get_map_location(int x, int y) {
        return new Rectangle(1 + x * 257, 1 + y * 177, 256, 176);
    }
    public bool switch_map(int x, int y) {
        if (mapLoader.load_map(x, y)) {
            map = mapLoader.get_map();
            return true;
        }
        return false;
    }
    public string[,] get_map() {
        return map;
    }

    public void Draw(SpriteBatch spriteBatch){
        Rectangle sourceRectangle = get_map_location(x, y);
        Rectangle targetRectangle = new Rectangle(0, 0, (int)window_size.X, (int)window_size.Y);
        spriteBatch.Draw(map_texture, targetRectangle, sourceRectangle, Color.White);
    }

}