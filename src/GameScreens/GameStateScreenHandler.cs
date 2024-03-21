﻿using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace ZeldaGame;
public enum GameState
{
    TitleScreen, Playing, Pause, GameOver
};

public class GameStateScreenHandler
{
    private Dictionary<GameState, IGameScreen> screens;
    private GameState currentGameState;
    private Game1 myGame;
    public GameStateScreenHandler(Game1 game)
    {
        screens = new Dictionary<GameState, IGameScreen>();
        currentGameState = GameState.TitleScreen;
        this.myGame = game;
        AddScreen(GameState.TitleScreen, new TitleScreen(game.Content.Load<Texture2D>("TitleScreen"), game));
        AddScreen(GameState.Pause, new PauseGame(game.Content.Load<Texture2D>("TitleScreen"), game)); // Placeholder
    }
    public void AddScreen(GameState state, IGameScreen screen)
    {
        screens[state] = screen;
    }

    public void Update()
    {
        screens[currentGameState].Update();
    }
    public void Draw(SpriteBatch spriteBatch)
    {
        screens[currentGameState].Draw(spriteBatch);
    }

    public GameState CurrentGameState
    {
        get { return currentGameState; }
        set { currentGameState = value; }
    }

    public bool IsPlaying()
    {
        return currentGameState == GameState.Playing;
    }
}