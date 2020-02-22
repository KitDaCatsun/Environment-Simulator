using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Terrain Material", menuName = "Terrain Material")]
public class TerrainMaterial : ScriptableObject {
    new public string name;
    public float heatCapacity;
    public float density;
    public float verticeVolume = 1;
}
