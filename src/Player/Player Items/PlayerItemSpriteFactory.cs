using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using static ZeldaGame.Player.PlayerStateMachine;
namespace ZeldaGame.Player;

public class PlayerItemSpriteFactory
{
    private Texture2D playerTexture;
    private List<ISprite> upItemSprites;
    private List<ISprite> leftItemSprites;
    private List<ISprite> downItemSprites;
    private List<ISprite> rightItemSprites;

    public PlayerItemSpriteFactory(Texture2D texture)
    {
        playerTexture = texture;
        // add list of sprites
        upItemSprites = new List<ISprite> { new UseArrowVerticalSprite(playerTexture, 0) };
        leftItemSprites = new List<ISprite> { new UseArrowHorizontalSprite(playerTexture, 1) };
        downItemSprites = new List<ISprite> { new UseArrowVerticalSprite(playerTexture, 1) };
        rightItemSprites = new List<ISprite> { new UseArrowHorizontalSprite(playerTexture, 0) };
    }

    public ISprite CreateItemSprite(Direction direction, int item)
    {
        switch (direction)
        {
            case Direction.Up:
                return upItemSprites[item];
            case Direction.Left:
                return leftItemSprites[item];
            case Direction.Down:
                return downItemSprites[item];
            case Direction.Right:
                return rightItemSprites[item];
        }
        return null;
    }
}