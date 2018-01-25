using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSphere : MonoBehaviour {

    public int gridSize;
    public float radius = 1;
    // No more roundness.

    private Mesh mesh;
    private Vector3[] vertices;
    private Vector3[] normals;
    private Color32[] cubeUV;

    private void Awake()
    {
        Generate();
    }

	private void Generate()
    {
        GetComponent<MeshFilter>().mesh = mesh = new Mesh();
        mesh.name = "Procedural Sphere";
        CreateVertices();
        CreateTriangles();
        CreateColliders();
    }

    private void CreateVertices()
    {
        mesh.vertices = vertices;
    }

    private void CreateTriangles()
    {
        int quads = (gridSize*gridSize + gridSize*gridSize + gridSize*gridSize)*2;
        int[] triangles=new int[quads*6];
        int ring = (gridSize + gridSize)*2;
        int t = 0, v = 0;
        for (int q = 0; q < gridSize; q++, v++)
        {
            t = SetQuad(triangles, t, v, v + 1, v + ring, v + ring + 1);
        }
        mesh.triangles = triangles;
    }

    private static int SetQuad(int[] triangles, int i, int v00, int v10, int v01, int v11)
    {
        triangles[i] = v00;
        triangles[i + 1] = triangles[i + 4] = v01;
        triangles[i + 2] = triangles[i + 3] = v10;
        triangles[i + 5] = v11;
        return i + 6;
    }

    private void CreateColliders()
    {
        gameObject.AddComponent<SphereCollider>();
    }

    private void SetVertex(int i, int x, int y, int z)
    {
        Vector3 v = new Vector3(x, y, z)*2f/gridSize - Vector3.one;
        normals[i] = v.normalized;
        vertices[i] = normals[i]*radius;
        cubeUV[i] = new Color32((byte) x, (byte) y, (byte) z, 0);
    }
}
