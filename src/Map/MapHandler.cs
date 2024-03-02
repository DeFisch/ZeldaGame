
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ZeldaGame.Player;

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
        map = mapLoader.get_map_info(); // get default map info
        mapRectangles = new MapStaticRectangles(this);
        mapRectangles.SetLists(window_size);
    }
    public Vector2 GetWindowScale(Vector2 windowSize) {
		return new Vector2(windowSize.X / map_size.X, windowSize.Y / map_size.Y);
	}
    private Rectangle get_map_location(int x, int y) {
        return new Rectangle(1 + x * 257, 1 + y * 177, (int)map_size.X, (int)map_size.Y);
    }
    public bool move_up() {
        if(mapLoader.isRoomAvailable("up")){
            switch_map(y - 1, x);
            return true;
        }
        return false;
    }
    public bool move_down() {
        if(mapLoader.isRoomAvailable("down")){
            switch_map(y + 1, x);
            return true;
        }
        return false;
    }
    public bool move_left() {
        if(mapLoader.isRoomAvailable("left")){
            switch_map(y, x - 1);
            return true;
        }
        return false;
    }
    public bool move_right() {
        if(mapLoader.isRoomAvailable("right")){
            switch_map(y, x + 1);
            return true;
        }
        return false;
    }


    public bool isRoomAvailable(string direction){
        return mapLoader.isRoomAvailable(direction);
    }

    private bool switch_map(int y, int x) {
        if (mapLoader.load_map(x, y)) {
            this.x = x;
            this.y = y;
            map = mapLoader.get_map_info();
            mapRectangles.SetLists(window_size); // update map rectangles
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

    public bool Debug {
        get { return debug; }
        set { debug = value; }
    }

    public void Reset()
    {
        x = 2;
        y = 5;
    }

    public void PlayerDoorCollision(Vector2 window_size, IPlayer player){
        Rectangle playerHitBox = player.GetPlayerHitBox();
        Vector2 playerCenterpoint = new Vector2(playerHitBox.X + playerHitBox.Width/2, playerHitBox.Y + playerHitBox.Height/2);
        Rectangle up_door = new Rectangle((int)(0.46875*window_size.X), 0, (int)(0.0625*window_size.X), (int)(0.18*window_size.Y));
        Rectangle down_door = new Rectangle((int)(0.46875*window_size.X), (int)(0.82*window_size.Y), (int)(0.0625*window_size.X), (int)(0.18*window_size.Y));
        Rectangle left_door = new Rectangle(0, (int)(0.45*window_size.Y), (int)(0.125*window_size.X), (int)(0.1*window_size.Y));
        Rectangle right_door = new Rectangle((int)(0.875*window_size.X), (int)(0.45*window_size.Y), (int)(0.125*window_size.X), (int)(0.1*window_size.Y));
        if (up_door.Contains(playerCenterpoint)){
            move_up();
            player.SetPlayerPosition(new Vector2((int)(window_size.X/2), (int)(0.8*window_size.Y)));
        }else if (down_door.Contains(playerCenterpoint)){
            move_down();
            player.SetPlayerPosition(new Vector2((int)(window_size.X/2), (int)(0.2*window_size.Y)));
        }else if (left_door.Intersects(playerHitBox)){
            move_left();
            player.SetPlayerPosition(new Vector2((int)(0.8*window_size.X), (int)(window_size.Y/2)));
        }else if (right_door.Intersects(playerHitBox)){
            move_right();
            player.SetPlayerPosition(new Vector2((int)(0.2*window_size.X), (int)(window_size.Y/2)));
        }

        //Room_0_1 Stair collision
        Rectangle stair = new Rectangle((int)(window_size.X / 2), (int)(window_size.Y / 11 * 5), (int)(window_size.X / 16), (int)(window_size.Y / 11));
        if (getMapXY().Equals(new Vector2(1, 0)))
        {
            if (stair.Contains(playerCenterpoint))
            {
                x = 0;
                y = 0;
                switch_map(0, 0);
                player.SetPlayerPosition(new Vector2(175, 240));
            }
        }

        //Room_0_0 back to room_0_1
        Rectangle invisibleDoor = new Rectangle((int)(window_size.X / 16 * 3), 0, (int)(window_size.X / 16), (int)(window_size.Y / 3));
        if (getMapXY().Equals(new Vector2(0, 0)))
        {
            if (invisibleDoor.Contains(playerCenterpoint))
            {
                x = 1;
                y = 0;
                switch_map(0, 1);
                player.SetPlayerPosition(new Vector2(375,305));
            }
        }
    }
}