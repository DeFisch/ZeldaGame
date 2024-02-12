using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using static ZeldaGame.Player.PlayerStateMachine;
namespace ZeldaGame.Player;

public class PlayerItemSpriteFactory
{
    private Texture2D playerTexture;
    private List<IProjectile> projectiles;

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

    public IProjectile CreateItemSprite(Direction direction, int item)
    {
        projectiles = new List<IProjectile>
        { 
            CreateArrowSprite(direction), CreateBlueArrowSprite(direction), CreateBoomerangSprite(direction),
            CreateBlueBoomerangSprite(direction), CreateBombSprite(direction), CreateFireballSprite(direction)
        };

        return projectiles[item];
    }

    public IProjectile CreateArrowSprite(Direction direction)
    {
        switch (direction)
        {
            case Direction.Up:
                return new ArrowVerticalSprite(playerTexture, Direction.Up);
            case Direction.Left:
                return new ArrowHorizontalSprite(playerTexture, Direction.Left);
            case Direction.Down:
                return new ArrowVerticalSprite(playerTexture, Direction.Down);
            case Direction.Right:
                return new ArrowHorizontalSprite(playerTexture, Direction.Right);
        }
        return null;
    }

    public IProjectile CreateBlueArrowSprite(Direction direction)
    {
        switch (direction)
        {
            case Direction.Up:
                return new BlueArrowVerticalSprite(playerTexture, Direction.Up);
            case Direction.Left:
                return new BlueArrowHorizontalSprite(playerTexture, Direction.Left);
            case Direction.Down:
                return new BlueArrowVerticalSprite(playerTexture, Direction.Down);
            case Direction.Right:
                return new BlueArrowHorizontalSprite(playerTexture, Direction.Right);
        }
        return null;
    }

    public IProjectile CreateBoomerangSprite(Direction direction)
    {
        return new BoomerangSprite(playerTexture, direction);
    }

    public IProjectile CreateBlueBoomerangSprite(Direction direction)
    {
        return new BlueBoomerangSprite(playerTexture, direction);
    }

    public IProjectile CreateBombSprite(Direction direction)
    {
        return new BombSprite(playerTexture, direction);
    }

    public IProjectile CreateFireballSprite(Direction direction)
    {
        return new FireballSprite(playerTexture, direction);
    }
}