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

    private float speed = 4000;

    [SerializeField]
    private HingeJoint2D hingeJoint;
    private JointMotor2D jointMotor;
    private IInputService _input;

    [Inject]
    public void Init(IInputService inputService)
    {
        _input = inputService;
    }
    private void OnEnable()
    {
        jointMotor = hingeJoint.motor;
        SetInitSpeed();
        SubscribeToEvent();
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
