using UnityEngine;

[System.Serializable]
public class SunSettings : SphereSettings {
    public World world;
    public StateManager stateManager;
    public Light sunLight;
    public float distanceToWorld;
    public float tropicLatitude;
    public int raysPerVertice;
    public float maxAdjustment;
    public float maxDistanceForRay;
    public SunLightSettings sunLightSettings;
}
