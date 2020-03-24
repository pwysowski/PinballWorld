using Assets.Scripts;
using Assets.Scripts.Input;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlungerController : MonoBehaviour
{
    private IGameController _gameController;
    private IInputService _inputService;
    private Vector3 _initValue;

    private SpringJoint2D spring;

    public void Init(IGameController gameController, IInputService inputService)
    {
        _gameController = gameController;
        _inputService = inputService;
    }

    private void OnEnable()
    {
        _gameController.OnGameStateChange += ToggleInput;
    }
    private void OnDisable()
    {
        _gameController.OnGameStateChange -= ToggleInput;
    }

    private void ToggleInput(GameState obj)
    {
        if (obj == GameState.IN_GAME || obj == GameState.MENU)
        {
            DisablePlunger();
        }
        else
        {
            EnablePlunger();
        }
    }

    private void EnablePlunger()
    {
        _inputService.ShootPlunger += Shoot;
        _inputService.StartPlunger += InitPlunger;
    }

    private void DisablePlunger()
    {
        _inputService.ShootPlunger -= Shoot;
        _inputService.StartPlunger -= InitPlunger;
    }


    private void Shoot(Vector3 value)
    {
        if(value.y < _initValue.y)
        {
            var diff = _initValue - value;
            var force = diff.sqrMagnitude;
        }
    }

    private void InitPlunger(Vector3 value)
    {
        _initValue = value;
    }
}
