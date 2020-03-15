using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public enum FlipperType
{
    LEFT,
    RIGHT,
}
public class FlipperController : MonoBehaviour
{
    [SerializeField]
    private FlipperType type;

    [SerializeField]
    private float speedUp = 1000;

    [SerializeField]
    private float speedDown = 600;

    [SerializeField]
    private HingeJoint2D hingeJoint;
    private JointMotor2D jointMotor;
    private void OnEnable()
    {
        jointMotor = hingeJoint.motor;
    }

    private void Paddle_Performed(InputAction.CallbackContext obj)
    {
        jointMotor.motorSpeed = speedDown;
        hingeJoint.motor = jointMotor;
    }
}
