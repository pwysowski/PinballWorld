using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPointsService
{
    void ResetPoints();
    void AddPoints(float value);
    float GetPoints();
    float CalculateReward();
}
