// Purpose: Load HUD map data from csv files and store it in a 2D array
/*
    References:
    - : empty
    numbers and letters : different type of map block
*/
using System;
using System.IO;
using Microsoft.Xna.Framework;

namespace ZeldaGame.HUD;

public class HUDMapLoader
{
    private string map_path;
    private string[,] map = new string[8, 8];
    private Rectangle sourceRectangle;
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
        sourceRectangle = new Rectangle(0,0,0,0);
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


    public Rectangle getSourceRectangle_O(string blockType)
    {
        sourceRectangle = new Rectangle(360, 140, 8, 8);
       switch (blockType)
       {
            case "0":
                sourceRectangle = new Rectangle(528, 108, 8, 8);
                break;
            case "1":
                sourceRectangle = new Rectangle(537, 108, 8, 8);
                break;
            case "2":
                sourceRectangle = new Rectangle(546, 108, 8, 8);
                break;
            case "3":
                sourceRectangle = new Rectangle(555, 108, 8, 8);
                break;
            case "4":
                sourceRectangle = new Rectangle(564, 108, 8, 8);
                break;
            case "5":
                sourceRectangle = new Rectangle(573, 108, 8, 8);
                break;
            case "6":
                sourceRectangle = new Rectangle(582, 108, 8, 8);
                break;
            case "7":
                sourceRectangle = new Rectangle(591, 108, 8, 8);
                break;
            case "9":
                sourceRectangle = new Rectangle(609, 108, 8, 8);
                break;
            case "a":
                sourceRectangle = new Rectangle(618, 108, 8, 8);
                break;
            case "b":
                sourceRectangle = new Rectangle(627, 108, 8, 8);
                break;
            case "e":
                sourceRectangle = new Rectangle(654, 108, 8, 8);
                break;
            default:
                sourceRectangle = new Rectangle(360, 140, 8, 8);
                break;
        }
        return sourceRectangle;
    }
    public Rectangle getSourceRectangle_B(string blockType)
    {
        sourceRectangle = new Rectangle(360, 140, 7, 3);
        switch (blockType)
        {
            case "-":
                sourceRectangle = new Rectangle(663, 111, 8, 4);
                break;
            default:
                sourceRectangle = new Rectangle(663, 108, 8, 4);
                break;
        }
        return sourceRectangle;
    }
}