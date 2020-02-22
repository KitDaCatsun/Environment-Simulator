using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerlinNoise3D {
    public float Evaluate(Vector3 point) {
        float XY = Mathf.PerlinNoise(point.x, point.y);
        float YZ = Mathf.PerlinNoise(point.y, point.z);
        float XZ = Mathf.PerlinNoise(point.x, point.z);

        float YX = Mathf.PerlinNoise(point.y, point.x);
        float ZY = Mathf.PerlinNoise(point.z, point.y);
        float ZX = Mathf.PerlinNoise(point.z, point.x);

        float XYZ = XY + YZ + XZ + YX + ZY + ZX;
        return XYZ / 6;
    }
}
