﻿using Assets.Scripts;
using Assets.Scripts.Input;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PlungerController : MonoBehaviour
{
    private IGameController _gameController;
    private IInputService _inputService;
    private Vector3 _initValue;

    [SerializeField]
    private SpringJoint2D spring;
    [SerializeField]
    private Rigidbody2D plungerRigidbody;

    public UnityEngine.UI.Text text;

    [Inject]
    public void Init(IGameController gameController, IInputService inputService, IGPSController gps)
    {
        _gameController = gameController;
        _inputService = inputService;
        
        gps.InitializeGPS();
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
        else if(obj == GameState.PRE_GAME)
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
        float force = 0;
        if (value.y < _initValue.y)
        {
            var diff = _initValue - value;
            force = diff.sqrMagnitude;
        }
        var finalForce = Mathf.Clamp(force, 0, 300);
        plungerRigidbody.AddForce(Vector3.up * finalForce);
    }

    private void InitPlunger(Vector3 value)
    {
        _initValue = value;
    }
}
