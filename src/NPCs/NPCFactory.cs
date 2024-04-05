using System;
using System.Collections.Generic;
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
    private Vector2 npcRoom;
	private bool inNPCRoom;
    private ItemSpriteFactory itemSpriteFactory;
    private Vector2 itemPosition;
    public NPCFactory(Texture2D texture, Vector2 scale, SpriteFont font, MapHandler mapHandler, ItemSpriteFactory itemSpriteFactory)
	{
		npcList = new List<INPC>();
		this.texture = texture;
		listLength = 0;
		this.scale = scale;
		this.font = font;
		this.mapHandler = mapHandler;
        npcRoom = new Vector2(0, 2);
		inNPCRoom = false;
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
	public bool IsInNPCRoom()
	{
        Vector2 currentMapXY = mapHandler.getMapXY();

        if (currentMapXY.Equals(npcRoom) && !inNPCRoom)
		{
			inNPCRoom = true;
			AddNPCs();
		}
        else if (!currentMapXY.Equals(npcRoom) && inNPCRoom)
        {
            Globals.audioLoader.StopSingleton("LOZ_Text");
            inNPCRoom = false;
        }
        return inNPCRoom;
	}
    private Vector2 GetNewScaledPosition(int x, int y)
    {
        float width = scale.X/2;
        float height = scale.Y/2;
        return new Vector2(width + (x * 75), height + (y * 70) + 175);// +175: moves NPC downward 
    }

    public List<INPC> GetNPCList()
    {
        return npcList;
    }
}