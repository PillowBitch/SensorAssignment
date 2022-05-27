using System;
using UnityEngine;

[Serializable]
public class KeyFrame
{
    public float accelX;
    public float accelZ;

    public KeyFrame() { }

    public KeyFrame(float x, float z)
    {
        accelX = x;
        accelZ = z;
    }
}