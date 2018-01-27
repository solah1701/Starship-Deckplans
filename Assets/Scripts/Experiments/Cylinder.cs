using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class Cylinder : MonoBehaviour
{

    public int Segments = 12;
    public float Radius = 1.0f;
    public float Height = 1.0f;
    public bool TopCap = true;
    public bool BottomCap = true;

    private Mesh mesh;
    private Vector3[] vertices;
    private Vector3[] normals;

    void Awake()
    {
        Generate();
    }

    public void Generate()
    {
        GetComponent<MeshFilter>().mesh = mesh = new Mesh();
        mesh.name = "Procedural Cylinder";
        CreateVertices();
        CreateTriangles();
    }

    void CreateVertices()
    {
        int v = 0;
        float step = 8f / Segments;
        var vertexCount = 2*Segments + (TopCap ? 1 : 0) + (BottomCap ? 1 : 0);
        vertices = new Vector3[vertexCount];
        normals = new Vector3[vertices.Length];

        for (int i = 0; i < Segments; i++, v++)
        {
            var it = i * step;
            if (it <= 1) CreateCirclePoint(v, Segments, it, 1f);
            if (it > 1 && it <= 3) CreateCirclePoint(v, Segments,1f, 2f - it);
            if (it > 3 && it <= 5) CreateCirclePoint(v, Segments, 4f - it, -1f);
            if (it > 5 && it < 7) CreateCirclePoint(v, Segments, -1f, it - 6f);
            if (it >= 7) CreateCirclePoint(v, Segments, it - 8f, 1f);
            normals[v] = vertices[v].normalized;
            normals[v + Segments] = vertices[v + Segments].normalized;
        }
        if (TopCap)
        {
            vertices[v] = new Vector3(0f, Height, 0f);
            normals[v] = vertices[v++].normalized;
        }
        if (BottomCap)
        {
            vertices[v] = new Vector3(0f, 0f, 0f);
            normals[v] = vertices[v].normalized;
        }
        mesh.vertices = vertices;
        mesh.normals = normals;
    }

    void CreateCirclePoint(int v, int vDash, float x, float y)
    {
        Vector2 circle;
        circle.x = x * Mathf.Sqrt(1f - y * y * 0.5f);
        circle.y = y * Mathf.Sqrt(1f - x * x * 0.5f);

        vertices[v] = new Vector3(Radius*circle.x, Height, Radius*circle.y);
        vertices[v + vDash] = new Vector3(Radius*circle.x, 0f, Radius*circle.y);
        Debug.Log(string.Format("vertex {0}, x {1}, y {2}, z {3}", v, vertices[v].x, vertices[v].y, vertices[v].z));
    }

    void CreateTriangles()
    {
        int quads = Segments;
        int[] triangles = new int[quads*6];
        int ring = Segments;
        int t = 0, v = 0;
		for (int i = 0; i < Segments - 1; i++, v++)
        {
            t = MeshHelper.SetQuad(triangles, t, v, v + 1, v + ring, v + ring + 1);
        }
        t = MeshHelper.SetQuad(triangles, t, v, 1, v + ring, ring + 1);
        mesh.triangles = triangles;
    }

    private void OnDrawGizmos()
    {
        if (vertices == null) return;
        for (int i = 0; i < vertices.Length; i++)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(vertices[i], 0.1f);
            Gizmos.color = Color.yellow;
            Gizmos.DrawRay(vertices[i], normals[i]);
        }
    }

}
