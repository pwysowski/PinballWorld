using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatAreaController : MonoBehaviour
{
    [SerializeField]
    private float floatingTime;
    [SerializeField]
    private BuoyancyEffector2D floatEffector;

    [SerializeField]
    private float pointsPerSecond;

    public Action<float> OnPointsGained { get; set; }

    private float timer;
    private bool ballInArea;

    private float currentPoints;

    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Ball"))
        {
            ballInArea = true;
            floatEffector.density = 200;
        }
    }

    public void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Ball"))
        {
            ballInArea = false;
            OnPointsGained?.Invoke(currentPoints);
            currentPoints = 0;
        }
    }

    void Update()
    {
        if (ballInArea)
        {
            timer += Time.deltaTime;
            currentPoints += pointsPerSecond;

            if (timer >= floatingTime)
            {
                ballInArea = false;
                floatEffector.density = 0;
                timer = 0f;
            }
        }
    }


}
