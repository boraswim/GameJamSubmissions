using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Mathf_Extra
{
    public static Vector3 GetDifferenceVector(Vector3 a, Vector3 b)
    {
        return a - b;
    }
    public static Vector3 GetDifferenceVectorNormalized(Vector3 a, Vector3 b)
    {
        return (a - b).normalized;
    }

    public static Vector3 GetVectorOfAngle(float angle)
    {
        return new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad), 0);
    }
    public static Vector3 GetAngleVectorBetweenPoints(Vector3 a, Vector3 b)
    {
        Vector3 distanceNormalized = GetDifferenceVectorNormalized(a, b);
        return Vector3.forward * Mathf.Atan2(distanceNormalized.y, distanceNormalized.x) * Mathf.Rad2Deg;
    }
    public static float GetAngleOfVector(Vector3 vector)
    {
        return Mathf.Rad2Deg * Mathf.Atan2(vector.y, vector.x);
    }
    public static float GetAngleBetweenPoints(Vector3 a, Vector3 b)
    {
        Vector3 distanceNormalized = GetDifferenceVectorNormalized(a, b);
        return Mathf.Atan2(distanceNormalized.x, distanceNormalized.z) * Mathf.Rad2Deg;
    }
}
