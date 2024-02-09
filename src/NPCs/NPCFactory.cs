using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ZeldaGame.Enemy;
namespace ZeldaGame.NPCs;

public class NPCFactory
{
    private List<INPC> npcList;
    private Texture2D texture;
    private Vector2 position;
    private INPC npc;
    private int cycleIndex;
    private int listLength;

    public NPCFactory(Texture2D texture, Vector2 window_size)
    {
        npcList = new List<INPC>();
        this.texture = texture;
        this.position = new Vector2(window_size.X / 3, window_size.Y / 3);
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
            case "OldWoman":
                npc = new OldWoman(texture, position);
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
        npc = npcList[cycleIndex];
        npc.Draw(spriteBatch);
    }
    public void Update()
    {
        npc = npcList[cycleIndex];
        npc.Update();
    }
}