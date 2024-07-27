using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MathfHelper
{

    /// <summary>
    /// Return List Direction with Amount base on Direction and Max Angle
    ///     Amount = 1 => return Direction
    ///     Amount > 1 && Angle < 180 => Spread Angle
    ///     Angle > 180 => Circle Angle
    /// </summary>
    /// <param name="direction"></param>
    /// <param name="amount"></param>
    /// <returns></returns>
    public static Vector3[] CalculateDirection(Vector3 direction, int amount = 1, float maxAngle = 360)
    {
        Debug.Log($"1: {direction}");
        if (amount <= 1)
        {
            return new Vector3[] { direction };
        }

        Vector3[] directions = new Vector3[amount];

        float max = maxAngle / 2;
        float min = -max;

        // Calculate the angle between each bullet
        float angleStep = maxAngle <= 180 ? (maxAngle / (amount - 1)) : (360f / amount);

        // Convert base direction to angles
        //float baseAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        // Create directions
        for (int i = 0; i < amount; i++)
        {
            float angle = Mathf.Clamp(min + i * angleStep,min,max);
            Vector3 dir = Quaternion.Euler(0f, angle, 0f) * direction;
            directions[i] = dir;
        }

        return directions;
    }
}
