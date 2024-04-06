using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ZeldaGame.Items;
using ZeldaGame.Map;
using ZeldaGame.Player;
namespace ZeldaGame.NPCs;

public class NPCFactory
{
	private List<INPC> npcList;
	private Texture2D texture;
	private Vector2 position;
	private Vector2 scale;
	private int listLength;
	private SpriteFont font;
    private MapHandler mapHandler;
    private List<Vector2> npcRooms;
	private bool inRoom;
    private ItemSpriteFactory itemSpriteFactory;
    private Vector2 itemPosition;
    public NPCFactory(Texture2D texture, Vector2 scale, SpriteFont font, MapHandler mapHandler, ItemSpriteFactory itemSpriteFactory)
	{
		npcList = new List<INPC>();
		npcRooms = new List<Vector2>();
        npcRooms.Add(new Vector2(0, 2));
		this.texture = texture;
		listLength = 0;
		this.scale = scale;
		this.font = font;
		this.mapHandler = mapHandler;
		inRoom = false;
        this.itemSpriteFactory = itemSpriteFactory;
        itemPosition = new Vector2(5, 4);
    }

	public void AddNPCs()
	{
        Random random = new Random();
        string[,] mapInfo = mapHandler.get_map_info();
        string npc;
        npcList.Clear();
        for (int i = 0; i < mapInfo.GetLength(0); i++) //Rows
        {
            for (int j = 0; j < mapInfo.GetLength(1); j++) //Columns
            {
                npc = mapInfo[i, j];
                position = GetNewScaledPosition(j, i);
                switch (npc)
                {
                    case "fl":
                        npcList.Add(new Flame(texture, position));
                        break;
                    case "rnd":
                        int randomNumber = random.Next(5);
                        switch (randomNumber)
                        {
                            case 0:
                                npcList.Add(new Merchant(texture, position));
                                if (itemSpriteFactory.IsMapChanged())
                                {
                                    itemSpriteFactory.AddItem("k", itemPosition);
                                }
                                break;
                            case 1:
                                npcList.Add(new OldMan(texture, position));
                                if (itemSpriteFactory.IsMapChanged())
                                {
                                    itemSpriteFactory.AddItem("ws", itemPosition);
                                }
                                break;
                            case 2:
                                npcList.Add(new OldWoman(texture, position));
                                if (itemSpriteFactory.IsMapChanged())
                                {
                                    itemSpriteFactory.AddItem("lp", itemPosition);
                                }
                                break;
                            case 3:
                                npcList.Add(new Fairy(texture, position));
                                break;
                            case 4:
                                npcList.Add(new Zelda(texture, position));
                                break;
                            default:
                                break;
                        }
                        break;
                    default:
                        break;
                }
            }
        }
        listLength = npcList.Count;
    }

	public void Draw(SpriteBatch spriteBatch)
	{
        for (int i = 0; i < listLength; i++)
        {
            npcList[i].Draw(spriteBatch, scale, font);
        }
    }
	public void Update()
	{
        for (int i = 0; i < listLength; i++)
        {
            npcList[i].Update();
        }
    }
	public bool IsInNPCRoom(Vector2 mapXY)
	{
        if (npcRooms.Contains(mapXY)) {
            inRoom = true;
            AddNPCs();
        }
        else if (!npcRooms.Contains(mapXY)) {
            Globals.audioLoader.StopSingleton("LOZ_Text");
            inRoom = false;
        }
        return inRoom;
	}
    private Vector2 GetNewScaledPosition(int x, int y)
    {
		int scaled_x = ((int)(scale.X * (16 * x + 32)));
		int scaled_y = ((int)(scale.Y * (16 * y + 32 + 56)));
		return new Vector2(scaled_x, scaled_y);
	}

    public List<INPC> GetNPCList()
    {
        return npcList;
    }
}