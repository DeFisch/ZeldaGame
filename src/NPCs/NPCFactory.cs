using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ZeldaGame.Map;
using ZeldaGame.Player;
namespace ZeldaGame.NPCs;

public class NPCFactory
{
	private List<INPC> npcList;
	private Texture2D texture;
	private Vector2 position;
	private Vector2 scale;
	private int cycleIndex;
	private int listLength;
	private SpriteFont font;
    private bool isCollision;
    private MapHandler mapHandler;
    private Vector2 dungeon;
	private bool inDungeon;
    public NPCFactory(Texture2D texture, Vector2 scale, SpriteFont font, MapHandler mapHandler)
	{
		npcList = new List<INPC>();
		this.texture = texture;
		cycleIndex = 0;
		listLength = 0;
		this.scale = scale;
		this.font = font;
		this.mapHandler = mapHandler;
        dungeon = new Vector2(0, 2);
		inDungeon = false;
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
                                break;
                            case 1:
                                npcList.Add(new OldMan(texture, position));
                                break;
                            case 2:
                                npcList.Add(new OldWoman(texture, position));
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
	public void cycleList(int cycleDirection)
	{
		listLength = npcList.Count;
		if (cycleDirection == 1)
		{
			cycleIndex = (cycleIndex + 1) % listLength;
		}
		else if (cycleDirection == 0)
		{
			cycleIndex = (cycleIndex - 1 + listLength) % listLength;
		}
	}
	public void Draw(SpriteBatch spriteBatch)
	{
        for (int i = 0; i < listLength; i++)
        {
            npcList[i].Draw(spriteBatch, scale);
        }

        /*if (isCollision)
        {
            string collisionMessage = npcList[cycleIndex].GetCollisionMessage();
            spriteBatch.DrawString(font, collisionMessage, new Vector2(position.X, position.Y), Color.White);
        }
        isCollision = false;*/
    }
	public void Update()
	{
        for (int i = 0; i < listLength; i++)
        {
            npcList[i].Update();
        }
    }
	public bool isInDungeon()
	{
        Vector2 currentMapXY = mapHandler.getMapXY();

        if (currentMapXY.Equals(dungeon) && !inDungeon)
		{
			inDungeon = true;
			AddNPCs();
		}
        else if (!currentMapXY.Equals(dungeon) && inDungeon)
        {
            inDungeon = false;
        }
        return inDungeon;
	}
    private Vector2 GetNewScaledPosition(int x, int y)
    {
        float width = scale.X/2;
        float height = scale.Y/2;
        return new Vector2(width + (x * 70), height + (y * 70));
    }
    public void PlayerNPCCollision(IPlayer player)
	{
			Rectangle playerHitBox = player.GetPlayerHitBox();
			for (int i = 0; i < listLength; i++)
			{
				if (npcList[i].GetNPCHitBox().Intersects(playerHitBox))
				{
					isCollision = true;
				}
			}
    }
    public void Reset()
    {
        cycleIndex = 0;
        listLength = 0;
    }
}