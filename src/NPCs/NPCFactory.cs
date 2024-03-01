﻿using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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

    public NPCFactory(Texture2D texture, Vector2 position, Vector2 scale, SpriteFont font)
	{
		npcList = new List<INPC>();
		this.texture = texture;
		this.position = position;
		cycleIndex = 0;
		listLength = 0;
		AddNPCs();
		this.scale = scale;
		this.font = font;
	}

	public void AddNPCs()
	{
		npcList.Add(new Fairy(texture, position));
		npcList.Add(new Flame(texture, position));
		npcList.Add(new Merchant(texture, position));
		npcList.Add(new OldMan(texture, position));
		npcList.Add(new OldWoman(texture, position));
		npcList.Add(new Zelda(texture, position));
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
		npcList[cycleIndex].Draw(spriteBatch, scale);
        if (isCollision)
        {
            string collisionMessage = npcList[cycleIndex].GetCollisionMessage();
            spriteBatch.DrawString(font, collisionMessage, new Vector2(position.X, position.Y), Color.White);
        }
        isCollision = false;
    }
	public void Update()
	{
		npcList[cycleIndex].Update();

	}

	public void Reset()
	{
		cycleIndex = 0;
		listLength = 0;
	}
	public void PlayerNPCCollision(IPlayer player)
	{
		Rectangle playerHitBox = player.GetPlayerHitBox();
        if (npcList[cycleIndex].GetNPCHitBox().Intersects(playerHitBox))
		{
            isCollision = true;
        }

	}
}