using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public static class MathHelper
{
    public static float RotationClamp(float rotation)
    {
        if (rotation < -360f) rotation += 360f;
        if (rotation > 360f) rotation -= 360f;

        return rotation;
    }
}
