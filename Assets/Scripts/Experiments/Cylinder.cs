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
        for (int x = 0; x < Segments; x++)
        {
            
        }
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
