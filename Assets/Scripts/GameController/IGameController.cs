﻿using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGameController
{
    void Init();
    void Exit();
    void ChangeGameState(GameState gameState);
    GameState CurrentState { get; set; }
    int Money { get; set; }
}