
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ZeldaGame.Player;
using ZeldaGame.Enemy;
using ZeldaGame.Items;
using static ZeldaGame.Globals;

namespace ZeldaGame.Map;

public class MapHandler {
    /*
        Note here x denotes the row and y denotes the column
    */
    private bool debug = false;
    private readonly MapLoader mapLoader;
    private string[,] map;
    private readonly Texture2D map_texture;
    private Vector3 map_size;
    private Vector2 room_size;
    public int x = 2, y = 5; // default map //Olivia modified this 
    private readonly MapStaticRectangles mapRectangles;
    private readonly Game1 game;
    private readonly  (bool, Dictionary<string, int>)[,] door_record = new (bool, Dictionary<string, int>)[7,12];
    private readonly (bool, List<IEnemy>)[,] enemy_record = new (bool, List<IEnemy>)[7,12];

    public MapHandler(Texture2D map_texture, Game1 game) {
        mapLoader = new MapLoader();
        this.game = game;
        this.map_texture = map_texture;
        map_size = game.mapSize;
        room_size = new Vector2(256, 176);
        map = mapLoader.get_map_info(); // get default map info
        mapRectangles = new MapStaticRectangles(this);
        mapRectangles.SetLists(map_size);
        // initialize enemy record
		for (int i = 0; i < 7; i++) {
			for (int j = 0; j < 12; j++) {
				enemy_record[i, j] = (false, null);
                door_record[i, j] = (false, null);
			}
		}
    }
    public void GetWindowScale(Vector3 mapSize) {
		scale = new Vector2(mapSize.X / room_size.X, mapSize.Y / room_size.Y);
	}
    private Rectangle get_map_location(int x, int y) {
        return new Rectangle(1 + x * 257, 1 + y * 177, (int)room_size.X, (int)room_size.Y);
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

    public bool hasBreakableWall(){
        foreach (string key in mapLoader.door_type.Keys){
            if (mapLoader.door_type[key] == 3){
                return true;
            }
        }
        return false;
    }

    public bool switch_map(int y, int x) {
        Dictionary<string, int> door_type_old = mapLoader.door_type;
        if (mapLoader.load_map(x, y)) {
            // save the enemies in the current room
            enemy_record[this.y, this.x] = (true, game.enemyFactory.GetAllEnemies());
            door_record[this.y, this.x] = (true, door_type_old);
            game.enemyFactory.ClearEnemies();
            if (!enemy_record[y, x].Item1) { // if player haven't been to this room
                foreach (string enemy in mapLoader.get_enemies(game.level)) {
                    game.enemyFactory.AddEnemy(enemy, map);
                }
            } else {
                foreach (IEnemy enemy in enemy_record[y, x].Item2) {
                    game.enemyFactory.AddEnemy(enemy);
                }
            }
            if (door_record[y, x].Item1) {
                mapLoader.door_type = door_record[y, x].Item2;
            }
            this.x = x;
            this.y = y;
            map = mapLoader.get_map_info();
            mapRectangles.SetLists(map_size); // update map rectangles
            ItemActionHandler.inventoryCounts[3] = 0;
            return true;
        }
        return false;
    }
    public string[,] get_map_info() {
        return map;
    }

    public void Draw(SpriteBatch spriteBatch){
        Rectangle mapSourceRectangle = get_map_location(x, y);
        Rectangle mapTargetRectangle = new (0, (int)map_size.Z, (int)map_size.X, (int)map_size.Y);
        spriteBatch.Draw(map_texture, mapTargetRectangle, mapSourceRectangle, Color.White);
        List<Rectangle> sList = mapRectangles.getSourceRectangleList();
        List<Rectangle> dlist = mapRectangles.getDestinationRectangleList();
        foreach(string key in mapLoader.door_type.Keys){
            if(mapLoader.door_type[key] == 2){
                DrawWallOnHole(spriteBatch, key, map_size);
            }
            else if (mapLoader.door_type[key] == 3)
            {
                DrawKeyedDoorOnHole(spriteBatch, key, map_size);
            }
        }
        if (debug){
            for (int i = 0; i < dlist.Count; i++)
                spriteBatch.Draw(map_texture, dlist[i], sList[i], Color.Red);
            dlist = this.getAllObjectRectangles(includeWater: false);
            for (int i = 0; i < dlist.Count; i++)
                spriteBatch.Draw(map_texture, dlist[i], sList[i], Color.Green);
        }
    }

    public bool BreakWall(string pos){
        int orig_y = y;
        int orig_x = x;
        switch(pos){
            case "up":
                if(mapLoader.door_type["up"] == 2){
                    mapLoader.door_type["up"] = 1;
                    this.switch_map(orig_y - 1, orig_x);
                    mapLoader.door_type["down"] = 1;
                    this.switch_map(orig_y, orig_x);
                    mapRectangles.SetLists(map_size);
                    return true;
                }
                break;
            case "down":
                if(mapLoader.door_type["down"] == 2){
                    mapLoader.door_type["down"] = 1;
                    this.switch_map(orig_y + 1, orig_x);
                    mapLoader.door_type["up"] = 1;
                    this.switch_map(orig_y, orig_x);
                    mapRectangles.SetLists(map_size);
                    return true;
                }
                break;
            case "left":
                if(mapLoader.door_type["left"] == 2){
                    mapLoader.door_type["left"] = 1;
                    this.switch_map(orig_y, orig_x - 1);
                    mapLoader.door_type["right"] = 1;
                    this.switch_map(orig_y, orig_x);
                    mapRectangles.SetLists(map_size);
                    return true;
                }
                break;
            case "right":
                if(mapLoader.door_type["right"] == 2){
                    mapLoader.door_type["right"] = 1;
                    this.switch_map(orig_y, orig_x + 1);
                    mapLoader.door_type["left"] = 1;
                    this.switch_map(orig_y, orig_x);
                    mapRectangles.SetLists(map_size);
                    return true;
                }
                break;
        }
        return false;
    }
    public bool UnlockDoor(string pos)
    {
        int orig_y = y;
        int orig_x = x;
        switch(pos){
            case "up":
                if(mapLoader.door_type["up"] == 3){
                    mapLoader.door_type["up"] = 1;
                    this.switch_map(orig_y - 1, orig_x);
                    mapLoader.door_type["down"] = 1;
                    this.switch_map(orig_y, orig_x);
                    mapRectangles.SetLists(map_size);
                    return true;
                }
                break;
            case "down":
                if(mapLoader.door_type["down"] == 3){
                    mapLoader.door_type["down"] = 1;
                    this.switch_map(orig_y + 1, orig_x);
                    mapLoader.door_type["up"] = 1;
                    this.switch_map(orig_y, orig_x);
                    mapRectangles.SetLists(map_size);
                    return true;
                }
                break;
            case "left":
                if(mapLoader.door_type["left"] == 3){
                    mapLoader.door_type["left"] = 1;
                    this.switch_map(orig_y, orig_x - 1);
                    mapLoader.door_type["right"] = 1;
                    this.switch_map(orig_y, orig_x);
                    mapRectangles.SetLists(map_size);
                    return true;
                }
                break;
            case "right":
                if(mapLoader.door_type["right"] == 3){
                    mapLoader.door_type["right"] = 1;
                    this.switch_map(orig_y, orig_x + 1);
                    mapLoader.door_type["left"] = 1;
                    this.switch_map(orig_y, orig_x);
                    mapRectangles.SetLists(map_size);
                    return true;
                }
                break;
        }
        return false;
    }

    public void DrawWallOnHole(SpriteBatch spriteBatch, string pos, Vector3 map_size){
        Rectangle srcRect = new (1527, 0, 16, 32);
        Rectangle destRect = new (0, 0, 0, 0);
        switch(pos){
            case "up":
                destRect = new Rectangle((int)(map_size.X*0.46875), (int)(map_size.Z), (int)(map_size.X*0.0625), (int)(map_size.Y*0.18));
                break;
            case "down":
                destRect = new Rectangle((int)(map_size.X*0.46875), (int)((map_size.Y*0.82) + map_size.Z), (int)(map_size.X*0.0625), (int)(map_size.Y*0.18));
                break;
            case "left":
                destRect = new Rectangle(0, (int)((map_size.Y*0.45) + map_size.Z), (int)(map_size.X*0.125), (int)(map_size.Y*0.1));
                break;
            case "right":
                destRect = new Rectangle((int)(map_size.X*0.875), (int)((map_size.Y*0.45) + map_size.Z), (int)(map_size.X*0.125), (int)(map_size.Y*0.1));
                break;
        }
        if (pos == "down")
            spriteBatch.Draw(map_texture, destRect, srcRect, Color.White, 0, new Vector2(0, 0), SpriteEffects.FlipVertically, 0);
        else
            spriteBatch.Draw(map_texture, destRect, srcRect, Color.White);
    }

    public void DrawKeyedDoorOnHole(SpriteBatch spriteBatch, string pos, Vector3 map_size)
    {
        Rectangle leftKeyedDoorSrcRect = new (324, 241, 32, 32);
        Rectangle rightKeyedDoorSrcRect = new (324, 274, 32, 32);
        Rectangle upKeyedDoorSrcRect = new (324, 208, 32, 32);
        Rectangle downKeyedDoorSrcRect = new (324, 307, 32, 32);
        Rectangle srcRect = Rectangle.Empty;
        Rectangle destRect = new (0, 0, 0, 0);
       switch(pos){
            case "up":
                srcRect = upKeyedDoorSrcRect;
                destRect = new Rectangle((int)(map_size.X*0.4375), (int)(map_size.Z), (int)(map_size.X*0.1289), (int)(map_size.Y*0.181818));
                break;
            case "down":
                srcRect = downKeyedDoorSrcRect;
                destRect = new Rectangle((int)(map_size.X*0.4375), (int)((map_size.Y*0.81818) + map_size.Z), (int)(map_size.X*0.1289), (int)(map_size.Y*0.181818));
                break;
            case "left":
                srcRect = leftKeyedDoorSrcRect;
                destRect = new Rectangle(0, (int)((map_size.Y*0.40909) + map_size.Z), (int)(map_size.X*0.125), (int)(map_size.Y*0.181818));
                break;
            case "right":
                srcRect = rightKeyedDoorSrcRect;
                destRect = new Rectangle((int)(map_size.X*0.875), (int)((map_size.Y*0.40909) + map_size.Z), (int)(map_size.X*0.125), (int)(map_size.Y*0.181818));
                break;
        }
        spriteBatch.Draw(map_texture, destRect, srcRect, Color.White);
    }
    public List<Rectangle> getAllObjectRectangles(bool includeWater = true){
        if (!includeWater)
            mapRectangles.SetLists(map_size, false);
        else
            mapRectangles.SetLists(map_size, true);
        List<Rectangle> dList = mapRectangles.getDestinationRectangleList();
        return dList;
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
        map = mapLoader.get_map_info();
        mapRectangles.SetLists(map_size);
        mapLoader.load_map(x,y);
        for (int i = 0; i < 7; i++)
            for (int j = 0; j < 12; j++)
            {
                enemy_record[i, j] = (false, null);
                door_record[i,j] = (false, null);
            }    
        switch_map(y, x);
    }
}