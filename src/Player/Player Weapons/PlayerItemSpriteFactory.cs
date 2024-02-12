using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using static ZeldaGame.Player.PlayerStateMachine;
namespace ZeldaGame.Player;

public class PlayerItemSpriteFactory
{
    private Texture2D playerTexture;

    private static PlayerItemSpriteFactory instance = new PlayerItemSpriteFactory();

    public static PlayerItemSpriteFactory Instance
    {
        get
        {
            return instance;
        }
    }

    public PlayerItemSpriteFactory()
    {
    }

    public void LoadAllTextures(ContentManager content)
    {
        playerTexture = content.Load<Texture2D>("Link");
    }

    public ISprite CreateItemSprite(Direction direction, int item)
    {
        switch (direction)
        {
            case Direction.Up:
                return new UseArrowVerticalSprite(playerTexture, 0);
            case Direction.Left:
                return new UseArrowHorizontalSprite(playerTexture, 1);
            case Direction.Down:
                return new UseArrowVerticalSprite(playerTexture, 1);
            case Direction.Right:
                return new UseArrowHorizontalSprite(playerTexture, 0);
        }
        return null;
    }
}