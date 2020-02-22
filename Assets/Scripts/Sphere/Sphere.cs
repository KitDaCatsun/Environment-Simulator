using UnityEngine;

[RequireComponent(typeof(MeshFilter))]

public class Sphere {

    public Triangle[] triangles;
    public Vertice[] vertices;
    public float radius;
    public string name;

    public Sphere(Triangle[] aTriangles, Vertice[] aVertices, float aRadius, string aName) {
        triangles = aTriangles;
        vertices = aVertices;
        radius = aRadius;
        name = aName;
    }

    public int[] GetTriangles() {
        // Turn Triangle array into int array of vertice indexes
        int[] fTriangles = new int[triangles.Length * 3];

        int i = 0;
        foreach (Triangle triangle in triangles) {
            fTriangles[i + 0] = triangle.vertices[0];
            fTriangles[i + 1] = triangle.vertices[1];
            fTriangles[i + 2] = triangle.vertices[2];
            i += 3;
        }

        return fTriangles;
    }

    public Vector3[] GetVertices() {
        // Turn Vertice array into Vector3 array of positions
        Vector3[] fVertices = new Vector3[vertices.Length];

        int i = 0;
        foreach (Vertice vertice in vertices) {
            fVertices[i] = vertice.position.normalized * (radius + vertice.height);
            i++;
        }

        return fVertices;
    }

    public Mesh Render() {
        int[] fTriangles = GetTriangles();
        Vector3[] fVertices = GetVertices();
        Vector3[] normals = new Vector3[fVertices.Length];
        Color32[] colors = new Color32[fVertices.Length];

        for (int i = 0; i < vertices.Length; i++) {
            normals[i] = fVertices[i].normalized;
            colors[i] = vertices[i].color;
        }

        // Create the Mesh
        Mesh mesh = new Mesh {
            name = name,
            vertices = fVertices,
            triangles = fTriangles,
            normals = normals,
            colors32 = colors
        };

        return mesh;
    }
}

public class Triangle {

    public int[] vertices;
    public string biome = "None";
    public int hitCount = 0;

    public Triangle(int a, int b, int c) {
        vertices = new int[] {
            a, b, c
        };
    }
}

public class Vertice {

    public Vector3 position;
    public float height;
    public Color32 color;

    public float temperature;
    public float Temperature {
        set {
            float temperatureChange = temperature - value;
            temperature = value;
            energy += material.heatCapacity * temperatureChange * (material.density * material.verticeVolume);
        }
    }
    public float energy;
    public float Energy {
        set {
            float energyChange = energy - value;
            energy = value;
            temperature += energyChange / (material.heatCapacity * (material.density * material.verticeVolume));
        }
    }
    public TerrainMaterial material;

    public Vertice(Vector3 aPosition, float aHeight) {
        position = aPosition;
        height = aHeight;
    }
}
