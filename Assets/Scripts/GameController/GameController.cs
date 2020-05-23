using Assets.Scripts;
using Assets.Scripts.Saves;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : IGameController
{

    public GameState CurrentState { get; set; }
    public int Money { get; set; }
    public Action<GameState> OnGameStateChange { get; set; }

    public void ChangeGameState(GameState gameState)
    {
        OnGameStateChange?.Invoke(gameState);
    }

    public void Init()
    {
        ChangeGameState(GameState.MENU);
    }

    public void Exit()
    {
        throw new System.NotImplementedException();
    }
}
