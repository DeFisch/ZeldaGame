using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ZeldaGame.Items;
using ZeldaGame.Map;
namespace ZeldaGame.NPCs;

public class NPCFactory
{
	private List<INPC> npcList;
	private Texture2D npcTexture;
    private Texture2D itemsTexture;
    private Vector2 position;
	private Vector2 scale;
	private int npclistLength;
	private SpriteFont font;
    private MapHandler mapHandler;
    private List<Vector2> npcRooms;
	private bool inRoom;
    private Vector2 itemPosition;
    private Vector2 itemPosition2;
    private ItemSpriteFactory itemSpriteFactory;
    public NPCFactory(Texture2D npcTexture, Texture2D itemsTexture, Vector2 scale, SpriteFont font, MapHandler mapHandler, ItemSpriteFactory itemSpriteFactory)
	{
		npcList = new List<INPC>();
		npcRooms = new List<Vector2>();
        npcRooms.Add(new Vector2(0, 2));
		this.npcTexture = npcTexture;
        this.itemsTexture = itemsTexture;
        npclistLength = 0;
		this.scale = scale;
		this.font = font;
		this.mapHandler = mapHandler;
		inRoom = false;
        itemPosition = new Vector2(5, 5);
        itemPosition2 = new Vector2(7, 5);
        this.itemSpriteFactory = itemSpriteFactory;
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
                        npcList.Add(new Flame(npcTexture, position));
                        break;
                    case "rnd":
                        int randomNumber = random.Next(5);
                        switch (randomNumber)
                        {
                            case 0:
                                npcList.Add(new Merchant(npcTexture, position));
                                if (itemSpriteFactory.IsMapChanged())
                                {
                                    itemSpriteFactory.AddItem("Key", itemPosition);
                                }
                                break;
                            case 1:
                                npcList.Add(new OldMan(npcTexture, position));
								if (itemSpriteFactory.IsMapChanged()) {
									itemSpriteFactory.AddItem("WhiteSword", itemPosition);
								}
								break;
                            case 2:
                                npcList.Add(new OldWoman(npcTexture, position));
                                if (itemSpriteFactory.IsMapChanged())
                                {
                                    itemSpriteFactory.AddItem("LifePotion", itemPosition);
                                }
                                break;
                            case 3:
                                npcList.Add(new Fairy(npcTexture, position));
                                if (itemSpriteFactory.IsMapChanged())
                                {
                                    itemSpriteFactory.AddItem("Heart", itemPosition);
                                    itemSpriteFactory.AddItem("HeartContainer", itemPosition2);
                                }
                                break;
                            case 4:
                                npcList.Add(new Zelda(npcTexture, position));
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
        npclistLength = npcList.Count;
    }

	public void Draw(SpriteBatch spriteBatch)
	{
        for (int i = 0; i < npclistLength; i++)
        {
            npcList[i].Draw(spriteBatch, scale, font);

            if (npcList[i] is Merchant)
            {
                DisplayCost(spriteBatch, itemPosition, 2);
            }
            else if (npcList[i] is OldWoman)
            {
                DisplayCost(spriteBatch, itemPosition, 5);
            }
        }
    }
	public void Update()
	{
        for (int i = 0; i < npclistLength; i++)
        {
            npcList[i].Update();
        }
    }
	public bool IsInNPCRoom(Vector2 mapXY)
	{
        if (npcRooms.Contains(mapXY) && !inRoom) {
            inRoom = true;
            AddNPCs();
        }
        else if (!npcRooms.Contains(mapXY) && inRoom) {
            Globals.audioLoader.StopSingleton("LOZ_Text");
            itemSpriteFactory.GetAllItems().Clear();
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
    public void DisplayCost(SpriteBatch spriteBatch, Vector2 position, int cost)
    {
        Vector2 costPosition = new Vector2(GetNewScaledPosition((int)position.X, (int)position.Y).X, GetNewScaledPosition((int)position.X, (int)position.Y).Y + 55);

        spriteBatch.DrawString(font, cost.ToString(), costPosition, Color.White);

        
        Rectangle sourceRectangle = new Rectangle(72, 16, 8, 16);
        Rectangle destinationRectangle = new Rectangle((int)costPosition.X - 200, (int)costPosition.Y - 20, (int)(sourceRectangle.Width * scale.X), (int)(sourceRectangle.Height * scale.Y));
        spriteBatch.Draw(itemsTexture, destinationRectangle, sourceRectangle, Color.White);
        spriteBatch.DrawString(font, "X", new Vector2 (destinationRectangle.X + 30,costPosition.Y), Color.White);
    }
}