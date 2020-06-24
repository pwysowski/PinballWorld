using Assets.Scripts;
using Assets.Scripts.Input;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public enum FlipperType
{
    LEFT,
    RIGHT,
}
public class FlipperController : MonoBehaviour
{
    [SerializeField]
    private FlipperType type;

    private float speed = 500;

    [SerializeField]
    private HingeJoint2D hingeJoint;
    private JointMotor2D jointMotor;
    private IInputService _input;
    private IGameController _gameController;

    [Inject]
    public void Init(IInputService inputService, IGameController gameController)
    {
        _input = inputService;
        _gameController = gameController;
    }
    private void OnEnable()
    {
        jointMotor = hingeJoint.motor;
        SetInitSpeed();

        _gameController.OnGameStateChange += ChangeStateHandle;

    }

    private void OnDisable()
    {
        _gameController.OnGameStateChange -= ChangeStateHandle;
    }

    private void ChangeStateHandle(GameState obj)
    {
        if(obj == GameState.PRE_GAME || obj == GameState.MENU)
        {
            UnsubscribeFromEvent();
        }
        else
        {
            SubscribeToEvent();
        }
    }

    private void SetInitSpeed()
    {
        var finalSpeed = type == FlipperType.LEFT ? speed : -speed;
        jointMotor.motorSpeed = finalSpeed;
        hingeJoint.motor = jointMotor;
    }

    private void SubscribeToEvent()
    {
        if(type == FlipperType.LEFT)
        {
            _input.OnPaddleLeftUp += PaddleDown;
            _input.OnPaddleLeftDown += PaddleUp;
        }
        else
        {
            _input.OnPaddleRightUp += PaddleDown;
            _input.OnPaddleRightDown += PaddleUp;
        }
    }

    private void UnsubscribeFromEvent()
    {
        PaddleDown();
        if (type == FlipperType.LEFT)
        {
            _input.OnPaddleLeftUp -= PaddleDown;
            _input.OnPaddleLeftDown -= PaddleUp;
        }
        else
        {
            _input.OnPaddleRightUp -= PaddleDown;
            _input.OnPaddleRightDown -= PaddleUp;
        }
    }

    private void PaddleUp()
    {
        var finalSpeed = type == FlipperType.LEFT ? -speed : speed;
        jointMotor.motorSpeed = finalSpeed;
        hingeJoint.motor = jointMotor;
    }

    private void PaddleDown()
    {
        var finalSpeed = type == FlipperType.LEFT ? speed : -speed;
        jointMotor.motorSpeed = finalSpeed;
        hingeJoint.motor = jointMotor;
    }

}
