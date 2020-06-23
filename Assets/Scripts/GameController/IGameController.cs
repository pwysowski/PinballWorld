using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGameController
{
    void Init();
    void Exit();
    void ChangeGameState(GameState gameState);
    void ShowAchievementsUI();
    void ShowLeaderboardsUI();
    void SaveGamepoints(int points);
    void AddMoney(int money);
    void CompletedFirstGame();
    GameState CurrentState { get; set; }
    int Money { get; set; }
    bool FirstGamePlayed { get; set; }
    Action<GameState> OnGameStateChange { get; set; }
}
