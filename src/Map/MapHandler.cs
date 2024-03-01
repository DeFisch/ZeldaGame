using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ZeldaGame.Map;

public class MapHandler {
    /*
        Note here x denotes the row and y denotes the column
    */
    private bool debug = false;
    private MapLoader mapLoader;
    private string[,] map;
    private Texture2D map_texture;
    private Vector2 window_size;
    private Vector2 map_size;
    private int x = 2, y = 5; // default map
    private MapStaticRectangles mapRectangles;

    public MapHandler(Texture2D map_texture, Vector2 window_size) {
        mapLoader = new MapLoader();
        this.map_texture = map_texture;
        this.window_size = window_size;
        map_size = new Vector2(256, 176);
        mapLoader.load_map(x, y); // load default map
        map = mapLoader.get_map_info(); // get default map info
        mapRectangles = new MapStaticRectangles(this);
    }
    public Vector2 GetWindowScale(int x, int y) {
		return new Vector2(x / map_size.X, y / map_size.Y);
	}
    private Rectangle get_map_location(int x, int y) {
        return new Rectangle(1 + x * 257, 1 + y * 177, (int)map_size.X, (int)map_size.Y);
    }
    public bool move_up() {
        if(switch_map(y - 1, x))
            return true;
        return false;
    }
    public bool move_down() {
        if(switch_map(y + 1, x))
            return true;
        return false;
    }
    public bool move_left() {
        if(switch_map(y, x - 1))
            return true;
        return false;
    }
    public bool move_right() {
        if(switch_map(y, x + 1))
            return true;
        return false;
    }

    private bool switch_map(int y, int x) {
        if (mapLoader.load_map(x, y)) {
            this.x = x;
            this.y = y;
            map = mapLoader.get_map_info();
            mapRectangles = new MapStaticRectangles(this); // update map rectangles
            return true;
        }
        return false;
    }
    public string[,] get_map_info() {
        return map;
    }

    public void Draw(SpriteBatch spriteBatch){
        Rectangle mapSourceRectangle = get_map_location(x, y);
        Rectangle mapTargetRectangle = new Rectangle(0, 0, (int)window_size.X, (int)window_size.Y);
        spriteBatch.Draw(map_texture, mapTargetRectangle, mapSourceRectangle, Color.White);
        mapRectangles.SetLists(window_size);
        List<Rectangle> sList = mapRectangles.getSourceRectangleList();
        List<Rectangle> dlist = mapRectangles.getDestinationRectangleList();
        if (debug){
            for (int i = 0; i < dlist.Count; i++)
            {
                spriteBatch.Draw(map_texture, dlist[i], sList[i], Color.Green);
            }
        }
    }
    public List<Rectangle> getAllObjectRectangles(){
        return mapRectangles.getDestinationRectangleList();
    }

    public Vector2 getMapXY(){
        return new Vector2(x, y);
    }

    public bool is_map_available(int x, int y) {
        return mapLoader.is_map_available(x, y);
    }

}