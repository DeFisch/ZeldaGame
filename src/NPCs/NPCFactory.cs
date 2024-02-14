using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ZeldaGame.Enemy;
namespace ZeldaGame.NPCs;

public class NPCFactory {
	private List<INPC> npcList;
	private Texture2D texture;
	private Vector2 position;
	private int cycleIndex;
	private int listLength;

	public NPCFactory(Texture2D texture, Vector2 window_size) {
		npcList = new List<INPC>();
		this.texture = texture;
		this.position = new Vector2(window_size.X / 3, window_size.Y / 3);
		cycleIndex = 0;
		listLength = 0;
		AddNPCs();
	}

	public void AddNPCs() {
		npcList.Add(new Fairy(texture, position));
		npcList.Add(new Flame(texture, position));
		npcList.Add(new Merchant(texture, position));
		npcList.Add(new OldMan(texture, position));
		npcList.Add(new OldWoman(texture, position));
		npcList.Add(new Zelda(texture, position));
	}
	public void cycleList(int cycleDirection) {
		listLength = npcList.Count;
		if (cycleDirection == 1) {
			cycleIndex = (cycleIndex + 1) % listLength;
		}
		else if (cycleDirection == 0) {
			cycleIndex = (cycleIndex - 1 + listLength) % listLength;
		}
	}
	public void Draw(SpriteBatch spriteBatch) {
		npcList[cycleIndex].Draw(spriteBatch);
	}
	public void Update() {
		npcList[cycleIndex].Update();

	}

	public void Reset()
	{
        cycleIndex = 0;
        listLength = 0;
    }
}