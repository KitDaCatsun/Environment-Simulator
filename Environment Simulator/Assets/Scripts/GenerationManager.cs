using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerationManager : MonoBehaviour {

    public World world;
    public Sun sun;

    public WorldSettings worldSettings;
    public SunSettings sunSettings;
    public SystemSettings systemSettings;

    public void GenerateWorld() {
        world.worldSettings = worldSettings;
        world.Generate(worldSettings, systemSettings);
    }

    public void GenerateSun() {
        sun.sunSettings = sunSettings;
        sun.Generate(sunSettings, systemSettings);
    }

    void Awake() {
        GenerateWorld();
        GenerateSun();
    }
}
