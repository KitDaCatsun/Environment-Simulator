using System.Collections.Generic;
using UnityEngine;

public class SphereCreator {

    public Triangle[] triangles;
    private List<Vector3> vertices;

    SphereSettings settings;

    public SphereCreator(SphereSettings aSettings) {
        settings = aSettings;
    }

    public int GetMidPointIndex(Dictionary<int, int> cache, int indexA, int indexB, List<Vector3> vertices) {

        // Checks if vertice has already been made and creates it if it hasn't
        int smallerIndex = Mathf.Min(indexA, indexB);
        int greaterIndex = Mathf.Max(indexA, indexB);
        int key = (smallerIndex << 16) + greaterIndex;

        if (cache.TryGetValue(key, out int ret))
            return ret;

        Vector3 p1 = vertices[indexA];
        Vector3 p2 = vertices[indexB];
        Vector3 middle = Vector3.Lerp(p1, p2, 0.5f).normalized;

        ret = vertices.Count;

        vertices.Add(middle);

        cache.Add(key, ret);
        return ret;
    }

    public Sphere CreateSphere() {

        float t = (1.0f + Mathf.Sqrt(5.0f)) / 2.0f;

        // Initial Vertices and Triangles of Icosahedron
        vertices = new List<Vector3>
        {
            new Vector3(-1, t, 0).normalized,
            new Vector3(1, t, 0).normalized,
            new Vector3(-1, -t, 0).normalized,
            new Vector3(1, -t, 0).normalized,
            new Vector3(0, -1, t).normalized,
            new Vector3(0, 1, t).normalized,
            new Vector3(0, -1, -t).normalized,
            new Vector3(0, 1, -t).normalized,
            new Vector3(t, 0, -1).normalized,
            new Vector3(t, 0, 1).normalized,
            new Vector3(-t, 0, -1).normalized,
            new Vector3(-t, 0, 1).normalized
        };

        triangles = new Triangle[]
        {
            new Triangle(0, 11, 5),
            new Triangle(0, 1, 7),
            new Triangle(0, 5, 1),
            new Triangle(0, 7, 10),
            new Triangle(0, 10, 11),
            new Triangle(1, 5, 9),
            new Triangle(5, 11, 4),
            new Triangle(11, 10, 2),
            new Triangle(10, 7, 6),
            new Triangle(7, 1, 8),
            new Triangle(3, 9, 4),
            new Triangle(3, 4, 2),
            new Triangle(3, 2, 6),
            new Triangle(3, 6, 8),
            new Triangle(3, 8, 9),
            new Triangle(4, 9, 5),
            new Triangle(2, 4, 11),
            new Triangle(6, 2, 10),
            new Triangle(8, 6, 7),
            new Triangle(9, 8, 1)
        };

        // Divide faces
        var midPointCache = new Dictionary<int, int>();
        for (int d = 0; d < settings.divisions; d++) {

            List<Triangle> fTriangles = new List<Triangle>();

            for (int f = 0; f < triangles.Length; f++) {
                // Get current triangle
                Triangle triangle = triangles[f];

                // Get current triangle vertices
                int a = triangle.vertices[0];
                int b = triangle.vertices[1];
                int c = triangle.vertices[2];

                // Find vertice at centre of each edge
                int ab = GetMidPointIndex(midPointCache, a, b, vertices);
                int bc = GetMidPointIndex(midPointCache, b, c, vertices);
                int ca = GetMidPointIndex(midPointCache, c, a, vertices);

                // Create new Triangles
                fTriangles.Add(new Triangle(a, ab, ca));
                fTriangles.Add(new Triangle(b, bc, ab));
                fTriangles.Add(new Triangle(c, ca, bc));
                fTriangles.Add(new Triangle(ab, bc, ca));
            }

            triangles = fTriangles.ToArray();
        }

        Vertice[] fVertices = new Vertice[vertices.Count];
        for (int i = 0; i < vertices.Count; i++) {
            fVertices[i] = new Vertice(vertices[i], 0);
        }

        Sphere sphere = new Sphere(triangles, fVertices, settings.radius, settings.name);

        return sphere;
    }
}
