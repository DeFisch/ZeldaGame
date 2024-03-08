// Purpose: Load map data from csv files and store it in a 2D array
/*
    References:
    - : normal tile
    b : block
    w : water
    st : stairs
*/
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace ZeldaGame.Map;

public class MapLoader {
    private string map_path;
    private string[,] map = new string[7, 12];
    private List<string> enemies = new List<string>();
    public Dictionary<string, int> door_type = new Dictionary<string, int>(){
        {"up", 0},
        {"down", 0},
        {"left", 0},
        {"right", 0}
    }; // 0: no door, 1: door, 2: breakable wall
    public MapLoader() {
        // set map path based on OS platform
        var pid = Environment.OSVersion.Platform;
        switch (pid) {
            case PlatformID.Win32NT:
			case PlatformID.Win32S:
			case PlatformID.Win32Windows:
			case PlatformID.WinCE:
                map_path = "../../../Content/map_data/";
                break;
            case PlatformID.MacOSX:
			case PlatformID.Unix:
                map_path = "Content/map_data/";
                break;
            default:
                map_path = "../../../Content/map_data/";
                break;
        }
        if (!load_map(2, 5)) { // default map
            throw new FileNotFoundException("Map file not found");
        }
    }

    public void SetDoorType(Dictionary<string, int> door_type) {
        this.door_type = door_type;
    }
    public void reset(){
        door_type = new Dictionary<string, int>(){
            {"up", 0},
            {"down", 0},
            {"left", 0},
            {"right", 0}
        };
        enemies.Clear();
    }

    public bool load_map(int x, int y) {
        door_type = new Dictionary<string, int>(){
            {"up", 0},
            {"down", 0},
            {"left", 0},
            {"right", 0}
        };
        string file_path = map_path + "map_" + y + "_" + x + ".csv";
        if (File.Exists(file_path)) {
            string[] lines = File.ReadAllLines(file_path);
            reset(); // reset door_type and enemies
            for (int i = 0; i < lines.Length; i++) {
                string[] line_data = lines[i].Split(',');
                if (i < 7){ // read tile data
                    for (int j = 0; j < line_data.Length; j++) {
                        map[i, j] = line_data[j];
                    }
                }else{ // read in additional information
                    switch (line_data[0]) {
                        case "door":
                            for (int j = 1; j < line_data.Length; j++) {
                                if(door_type.ContainsKey(line_data[j]))door_type[line_data[j]] = 1;
                            }
                            break;
                        case "breakable":
                            for (int j = 1; j < line_data.Length; j++) {
                                if(door_type.ContainsKey(line_data[j]))door_type[line_data[j]] = 2;
                            }
                            break;
                        case "enemy":
                            for (int j = 1; j < line_data.Length; j++) {
                                enemies.Add(line_data[j]);
                            }
                            break;
                    }
                }
            }
            return true;
        }
        return false;
    }

    public List<string> get_enemies() {
        return enemies;
    }

    public string[,] get_map_info() {
        return map;
    }

    public bool isRoomAvailable(string direction) {
        if(door_type[direction] == 1) {
            return true;
        }
        return false;
    }
}