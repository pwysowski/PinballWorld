using Assets.Scripts;
using Assets.Scripts.Input;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class CameraMovement : MonoBehaviour
{
    private IInputService _input;
    private IGameController _gameController;
    private Camera _camera;

    [Inject]
    public void Init(IInputService inputService, IGameController gameController)
    {
        _input = inputService;
        _gameController = gameController;
    }

    private void Awake()
    {
        _camera = GetComponent<Camera>();
    }

    private void OnEnable()
    {
        _gameController.OnGameStateChange += GameStateChanged;
    }

    private void OnDisable()
    {
        _gameController.OnGameStateChange -= GameStateChanged;
    }

    private void GameStateChanged(GameState state)
    {
        switch (state)
        {
            case GameState.IN_GAME:
                DisableMovementControls();
                break;
            case GameState.PRE_GAME:
                EnableMovementControls();
                break;
            case GameState.MENU:
                DisableMovementControls();
                break;
        }
    }

    private void EnableMovementControls()
    {
        _input.Zoom += Zoom;
        _input.Pan += Pan;
    }

    private void DisableMovementControls()
    {
        _input.Zoom -= Zoom;
        _input.Pan -= Pan;
    }

    private void Pan(Vector3 obj)
    {
        _camera.transform.position += obj;
    }

    private void Zoom(float inc)
    {
        _camera.orthographicSize = Mathf.Clamp((_camera.orthographicSize - inc), 1, 8);
    }
}
