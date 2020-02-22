using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class Sun : MonoBehaviour {
    public Sphere mesh;

    [HideInInspector]
    public SunSettings sunSettings;

    SystemSettings systemSettings;
    SphereCreator sphereCreator;
    State state;

    Vector3 sunInitialPos;
    Light sunLight;

    public void Generate(SunSettings aSunSettings, SystemSettings aSystemSettings) {
        sunSettings = aSunSettings;
        systemSettings = aSystemSettings;

        // Create Objects
        sphereCreator = new SphereCreator(sunSettings);
        state = sunSettings.stateManager.state;

        // Generate World
        mesh = sphereCreator.CreateSphere();

        // Apply Mesh
        GetComponent<MeshFilter>().mesh = mesh.Render();

        // Setup Light
        sunLight = sunSettings.sunLight;
        sunLight.GetComponent<Light>().color = sunSettings.sunLightSettings.colour;
        sunLight.GetComponent<Light>().intensity = sunSettings.sunLightSettings.intensity;

        // Position Objects
        transform.position = new Vector3(sunSettings.distanceToWorld, 0, 0);
        sunInitialPos = transform.position;
        sunLight.transform.position = Vector3.zero;
    }

    void Update() {
        // Reset Positions
        transform.position = sunInitialPos;

        // Calculate Rotations
        float zRotation = Mathf.Sin(state.tick * Mathf.Deg2Rad) * sunSettings.tropicLatitude;
        float yRotation = state.decimalTick * 360;

        // Rotate Sun
        transform.RotateAround(Vector3.zero, Vector3.forward, zRotation);
        transform.RotateAround(Vector3.zero, Vector3.up, yRotation);
        transform.rotation = Quaternion.Euler(0,0,0);

        // Rotate Light
        Vector3 direction = (-transform.position).normalized;
        Quaternion rotation = Quaternion.LookRotation(direction);
        sunLight.transform.rotation = rotation;

        // Raycasting
        foreach (Vertice vertice in mesh.vertices) {
            // Check distance to world
            if (Vector3.Distance((vertice.position * sunSettings.radius) + transform.position, Vector3.zero) <= sunSettings.maxDistanceForRay) {
                // Repeat for each vertice adjustment
                for (var i = 0; i < sunSettings.raysPerVertice; i++) {
                    // Find Adjustment
                    Vector3 adjustment = Random.insideUnitSphere * sunSettings.maxAdjustment;
                    // Cast Ray
                    if (Physics.Raycast(transform.position + adjustment, transform.TransformDirection(vertice.position.normalized), out RaycastHit hit, Mathf.Infinity)) {
                        if (systemSettings.debug) {
                            Debug.DrawRay(transform.position + adjustment, transform.TransformDirection(vertice.position.normalized) * hit.distance, Color.red);
                            Debug.Log("Hit");
                        }
                    } else {
                        if (systemSettings.debug) {
                            Debug.DrawRay(transform.position + adjustment, transform.TransformDirection(vertice.position.normalized) * 100, Color.green);
                        }
                    }
                }
            }
        }

        GetComponent<MeshFilter>().mesh = mesh.Render();
    }
}
