// Purpose: Load map data from csv files and store it in a 2D array
/*
    References:
    - : normal tile
    b : block
    w : water
    st : stairs
*/
using System;
using System.Diagnostics;
using System.IO;

namespace ZeldaGame.Map;

public class MapLoader {
    private string map_path;
    private string[,] map = new string[7, 12];
    public MapLoader() {
        // set map path based on OS platform
        var pid = Environment.OSVersion.Platform;
        switch (pid) {
            case PlatformID.Win32NT:
				map_path = "../../../Content/map_data/";
				break;
			case PlatformID.Win32S:
				map_path = "../../../Content/map_data/";
				break;
			case PlatformID.Win32Windows:
				map_path = "../../../Content/map_data/";
				break;
			case PlatformID.WinCE:
                map_path = "../../../Content/map_data/";
                break;
            case PlatformID.MacOSX:
				map_path = "Content/map_data/";
				break;
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

    public bool load_map(int x, int y) {
        string file_path = map_path + "map_" + y + "_" + x + ".csv";
        if (File.Exists(file_path)) {
            string[] lines = File.ReadAllLines(file_path);
            for (int i = 0; i < lines.Length; i++) {
                string[] line_data = lines[i].Split(',');
                for (int j = 0; j < line_data.Length; j++) {
                    map[i, j] = line_data[j];
                }
            }
            return true;
        }
        return false;
    }

    public bool is_map_available(int x, int y) {
        string file_path = map_path + "map_" + y + "_" + x + ".csv";
        return File.Exists(file_path);
    }

    public string[,] get_map_info() {
        return map;
    }
}