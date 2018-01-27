using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class Cylinder : MonoBehaviour
{

    public int Segments = 12;
    public float Radius = 1.0f;

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
        vertices = new Vector3[Segments*2];
        normals = new Vector3[vertices.Length];

        for (int i = 0; i < Segments; i++, v++)
        {
            var it = i * step;
            if (it <= 1) CreateCirclePoint(v, Segments, it, 1f);
            if (it > 1 && it <= 3) CreateCirclePoint(v, Segments,1f, 2f - it);
            if (it > 3 && it <= 5) CreateCirclePoint(v, Segments, 4f - it, -1f);
            if (it >= 7) CreateCirclePoint(v, Segments, it - 8f, 1f);
            if (Segments % 2 == 0)
            {
                if (it > 5 && it < 7) CreateCirclePoint(v, Segments, -1f, 6f - it);
            }
            else
            {
                if (it > 5 && it < 7) CreateCirclePoint(v, Segments, -1f, it - 6f);
            }
            normals[v] = vertices[v].normalized;
            normals[v + Segments] = vertices[v + Segments].normalized;
        }
        mesh.vertices = vertices;
        mesh.normals = normals;
    }

    void CreateCirclePoint(int v, int vDash, float x, float y)
    {
        Vector2 circle;
        circle.x = x * Mathf.Sqrt(1f - y * y * 0.5f);
        circle.y = y * Mathf.Sqrt(1f - x * x * 0.5f);

        vertices[v] = new Vector3(Radius*circle.x, 0f, Radius*circle.y);
        vertices[v + vDash] = new Vector3(Radius*circle.x, 1f, Radius*circle.y);
        Debug.Log(string.Format("vertex {0}, x {1}, y {2}, z {3}", v, vertices[v].x, vertices[v].y, vertices[v].z));
    }

    void CreateTriangles()
    {
        
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
