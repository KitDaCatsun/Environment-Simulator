using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class World : MonoBehaviour {
    public Sphere mesh;

    [HideInInspector]
    public WorldSettings worldSettings;

    SystemSettings systemSettings;
    SphereCreator sphereCreator;
    NoiseFilter noiseFilter;

    public void Generate(WorldSettings aWorldSettings, SystemSettings aSystemSettings) {
        // Initialise
        transform.position = Vector3.zero;
        worldSettings = aWorldSettings;
        systemSettings = aSystemSettings;
        sphereCreator = new SphereCreator(worldSettings);
        noiseFilter = new NoiseFilter(worldSettings.noiseSettings);

        // Generate World
        mesh = sphereCreator.CreateSphere();

        foreach (Vertice vertice in mesh.vertices) {
            float height = noiseFilter.Evaluate(vertice.position);
            vertice.height = height;
        }

        GetComponent<MeshFilter>().mesh = mesh.Render();
    }
}
