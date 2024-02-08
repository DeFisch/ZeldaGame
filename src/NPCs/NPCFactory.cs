using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace ZeldaGame.NPCs;

public class NPCFactory
{
    private List<INPC> npcList;
    private Texture2D texture;
    private Vector2 position;
    private INPC npc;
    private int cycleIndex;
    private int listLength;

    public NPCFactory(Texture2D texture, Vector2 position)
    {
        npcList = new List<INPC>();
        this.texture = texture;
        this.position = position;
        npc = null;
        cycleIndex = 0;
        listLength = 0;
    }

    public void AddNPC(string npcName)
    {
        switch (npcName)
        {
            case "OldMan":
                npc = new OldMan(texture, position);
                break;
            case "Flame":
                npc = new Flame(texture, position);
                break;
            case "Zelda":
                npc = new Zelda(texture, position);
                break;
            case "Merchant":
                npc = new Merchant(texture, position);
                break;
            case "Fairy":
                npc = new Fairy(texture, position);
                break;
            default:
                break;
        }
        npcList.Add(npc);
    }
    public void cycleList(int cycleDirection)
    {
        listLength = npcList.Count;
        if (cycleDirection == 1 && cycleIndex < listLength)
        {
            cycleIndex++;
            if (cycleIndex >= listLength)
                cycleIndex -= listLength;
        }
        else if (cycleDirection == 0 && cycleIndex >= 0)
        {
            cycleIndex--;
            if (cycleIndex < 0)
                cycleIndex += listLength;
        }
    }
    public void Draw(SpriteBatch spriteBatch)
    {
        npc = npcList[cycleIndex];
        npc.Draw(spriteBatch);
    }
    public void Update()
    {
        foreach (INPC npc in npcList)
        {
            npc.Update();
        }
    }
}