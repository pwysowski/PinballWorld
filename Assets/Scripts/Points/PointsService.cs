using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsService : IPointsService
{
    private float Points;
    public void AddPoints(float value)
    {
        Debug.Log(Points);
        Points += value;
    }

    public float CalculateReward()
    {
        return Points * 0.3f;
    }

    public void ResetPoints()
    {
        Points = 0;
    }
}
