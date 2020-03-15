using Assets.Scripts;
using Assets.Scripts.Saves;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : IGameController
{
    private readonly ISaveController _saveController;

    public GameState CurrentState { get; set; }
    public int Money { get; set; }

    public GameController(ISaveController saveController)
    {
        _saveController = saveController;
    }

    public void ChangeGameState(GameState gameState)
    {
        
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
