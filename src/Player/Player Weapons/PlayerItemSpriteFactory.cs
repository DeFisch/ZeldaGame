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

    public IProjectile CreateItemSprite(Direction direction)
    {
        switch (direction)
        {
            case Direction.Up:
                return new UseArrowVerticalSprite(playerTexture, Direction.Up);
            case Direction.Left:
                return new UseArrowHorizontalSprite(playerTexture, Direction.Left);
            case Direction.Down:
                return new UseArrowVerticalSprite(playerTexture, Direction.Down);
            case Direction.Right:
                return new UseArrowHorizontalSprite(playerTexture, Direction.Right);
        }
        return null;
    }
}