using Assets.Scripts.Input;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class CameraMovement : MonoBehaviour
{
    private IInputService _input;
    private Camera _camera;

    [Inject]
    public void Init(IInputService inputService)
    {
        _input = inputService;
    }

    private void Awake()
    {
        _camera = GetComponent<Camera>();
    }

    private void OnEnable()
    {
        _input.Zoom += Zoom;
        _input.Pan += Pan;
    }

    private void OnDisable()
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
