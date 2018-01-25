using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeformCubeScript : MonoBehaviour
{

    public int VertexCount;
    public float Rate = 0.5f;

    void Update()
    {
        Mesh mesh = GetComponent<MeshFilter>().mesh;
        Vector3[] vertices = mesh.vertices;
        int i = 0;
        if (VertexCount > vertices.Length) VertexCount = vertices.Length;
        while (i < VertexCount)
        {
            vertices[i] += Vector3.up * Time.deltaTime * Rate;
            i++;
        }
        Debug.Log(string.Format("Vertices Length {0}", VertexCount));
        mesh.vertices = vertices;
        mesh.RecalculateNormals();
    }
}
