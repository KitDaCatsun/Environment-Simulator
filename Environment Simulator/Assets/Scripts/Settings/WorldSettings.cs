using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WorldSettings : SphereSettings {
    public TerrainMaterial material;
    public TerrainMaterial[] terrainMaterials;
    public StateManager stateManager;
    public NoiseSettings noiseSettings;
}