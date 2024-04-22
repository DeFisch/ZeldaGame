using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using ZeldaGame.Controllers;

public class EnemyController : IController
{
    private readonly Dictionary<Keys, Vector2> movementKeys;
    private PlayerEnemy playerEnemy;
    private bool isAI;

    public EnemyController()
    {
        movementKeys = new Dictionary<Keys, Vector2>();
        isAI = false;
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

    public void ToggleAI(bool isAI)
    {
        this.isAI = isAI;
    }

    public void Update()
    {
        if (!isAI)
        {
            Keys[] pressedKeys = Keyboard.GetState().GetPressedKeys();

            foreach (Keys key in pressedKeys)
            {
                if (movementKeys.ContainsKey(key))
                    playerEnemy?.Move(movementKeys[key]);
            }
        }
    }
}