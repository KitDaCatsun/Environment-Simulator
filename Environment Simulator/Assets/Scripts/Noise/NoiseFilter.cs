using UnityEngine;

public class NoiseFilter {
    PerlinNoise3D noise = new PerlinNoise3D();
    NoiseSettings settings;

    public NoiseFilter(NoiseSettings aSettings) {
        settings = aSettings;

        if (settings.randomCentre) {
            settings.centre = Random.insideUnitSphere * 100;
        }
    }

    public float Evaluate(Vector3 point) {
        float noiseValue = 0;
        float frequency = settings.baseRoughness;
        float amplitude = 1;

        for (int i = 0; i < settings.layers; i++) {
            float v = noise.Evaluate(point * frequency + settings.centre) * 2 - 1;
            noiseValue += v * amplitude;
            frequency *= settings.roughness;
            amplitude *= settings.persistence;
        }

        return noiseValue * settings.strength;
    }
}
