// Purpose: Load HUD map data from csv files and store it in a 2D array
/*
    References:
    - : empty
    numbers and letters : different type of map block
*/
using System;
using System.IO;

namespace ZeldaGame.HUD;

public class HUDMapLoader
{
    private string map_path;
    private string[,] map = new string[8, 8];
    public HUDMapLoader()
    {
        // set map path based on OS platform
        var pid = Environment.OSVersion.Platform;
        switch (pid)
        {
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
    }

    public string[,] load_map()
    {
        string file_path = map_path + "map_HUD.csv";
        if (File.Exists(file_path))
        {
            string[] lines = File.ReadAllLines(file_path);
            for (int i = 0; i < lines.Length; i++)
            {
                string[] line_data = lines[i].Split(',');
                for (int j = 0; j < line_data.Length; j++)
                    map[i, j] = line_data[j];
            }
        }
        return map;
    }
}