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
    [SerializeField]
    private float density;

    public Action<float> OnPointsGained { get; set; }

    private float timer;
    private bool ballInArea;

    private float currentPoints;

    private AudioSource audioSource;

    private void Awake() {
        audioSource = GetComponent<AudioSource>();
    }

    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Ball"))
        {
            ballInArea = true;
            floatEffector.density = density;

            if(!audioSource.isPlaying)
                audioSource.Play();
        }
    }

    public void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Ball"))
        {
            ballInArea = false;
            OnPointsGained?.Invoke(currentPoints);
            currentPoints = 0;
            audioSource.Stop();
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
