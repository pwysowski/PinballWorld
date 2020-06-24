using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsService : IPointsService
{
    private float Points;
    public void AddPoints(float value)
    {
        Points += value;
    }

    public float GetPoints(){
        return Points;
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
