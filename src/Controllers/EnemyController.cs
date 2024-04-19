using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using ZeldaGame.Controllers;

public class EnemyController : IController
{
    private readonly Dictionary<Keys, Vector2> movementKeys;
    private PlayerEnemy playerEnemy;
    public EnemyController()
    {
        movementKeys = new Dictionary<Keys, Vector2>();
    }

    public void RegisterMovementKey(Keys key, Vector2 movement)
    {
        movementKeys.Add(key, movement);
    }

    public void RegisterPlayer(PlayerEnemy playerEnemy)
    {
        this.playerEnemy = playerEnemy;
    }

    public void UnregisterPlayer()
    {
        playerEnemy = null;
    }

    public void Update()
    {
        Keys[] pressedKeys = Keyboard.GetState().GetPressedKeys();

        foreach (Keys key in pressedKeys)
        {
            if (movementKeys.ContainsKey(key))
                playerEnemy?.Move(movementKeys[key]);
        }
    }
}