using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tri : MonoBehaviour
{

	// Use this for initialization
	void Start () {
        RenderMesh();
    }

    private void RenderMesh()
    {
        var width = transform.localScale.x/2;
        var height = transform.localScale.y/2;
        
        var mesh = CreateMesh(width, height);
        var meshFilter = (MeshFilter)GetComponent(typeof (MeshFilter));
        meshFilter.mesh = mesh;

        var meshCollider = (MeshCollider) GetComponent(typeof (MeshCollider));
        meshCollider.sharedMesh = mesh;

    }

    private Mesh CreateMesh(float width, float height)
    {
        var m = new Mesh
        {
            name = "ScriptedMesh",
            vertices = new[]
            {
                new Vector3(width, height, 0.01f),
                new Vector3(width, -height, 0.01f),
                new Vector3(-width, -height, 0.01f),
            },
            uv = new[]
            {
                new Vector2(0, 0),
                new Vector2(0, 1),
                new Vector2(1, 1)
            },
            triangles = new[] { 0, 1, 2}
        };
        m.RecalculateNormals();
        return m;
    }

}
