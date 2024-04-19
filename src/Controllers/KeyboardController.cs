using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using ZeldaGame.Items;

namespace ZeldaGame.Controllers;
public class KeyboardController : IController
{
	private readonly KeyboardHandler keyboardHandler;
	private Dictionary<Keys, ICommand> holdKeys;
	private Dictionary<Keys, ICommand> pressKeys;
	private Keys[] keyLogger;
	int i;

	public KeyboardController()
	{
		keyboardHandler = new KeyboardHandler();
		holdKeys = new Dictionary<Keys, ICommand>();
		pressKeys = new Dictionary<Keys, ICommand>();
		keyLogger = new Keys[4];
		i = 0;

	}

	public void RegisterHoldKey(Keys key, ICommand command)
	{
		holdKeys.Add(key, command);
	}

    public void RegisterPressKey(Keys key, ICommand command)
    {
		if(!pressKeys.ContainsKey(key))
			pressKeys.Add(key, command);
		else
			pressKeys[key] = command;
    }

	public void UnregisterPressKey(Keys key)
	{
		if (pressKeys.ContainsKey(key))
		{
			pressKeys.Remove(key);
		}
	}

	public void RegisterPressKeySwitch(Keys key, ICommand command)
	{
		if (pressKeys.ContainsKey(key))
			pressKeys.Remove(key);
		pressKeys.Add(key, command);
	}

	public bool KeyLoggerTrack(Keys[] keyLogger)
	{
		Keys[] cheatCode = { Keys.D3, Keys.D9, Keys.D0, Keys.D2 };
		bool isCorrect = false;


		if (keyLogger[0] == Keys.D3 && keyLogger[1] == Keys.D9 && keyLogger[2] == Keys.D0 && keyLogger[3] == Keys.D2)
		{
			isCorrect = true;
			ItemActionHandler.inventoryCounts[1] += 1000;
			for (int j = 0; j < keyLogger.Length; j++)
			{
				keyLogger[j] = 0;
			}
		}
		return isCorrect;
	}

	public void Update()
	{

		Keys[] pressedKeys = Keyboard.GetState().GetPressedKeys();
		keyboardHandler.Update();

		foreach (Keys key in pressedKeys)
		{
			if (!keyLogger.Contains(key))
			{
				if (key.Equals(Keys.D3))
				{ i = 0;   
                }
				keyLogger[i] = key;
				i++;
				if (i > 3)
				{	i = 0;
					}
				KeyLoggerTrack(keyLogger);
			}
            if (holdKeys.ContainsKey(key) && keyboardHandler.IsHeld(key))
				{
					holdKeys[key].Execute();

				}

			if (pressKeys.ContainsKey(key) && keyboardHandler.IsPressed(key))
				{
					pressKeys[key].Execute();


				}
		}
	}
}

