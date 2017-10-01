using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyGameObject : MonoBehaviour {

    public float MyHeight;
    public float MyWidth;

    private Mesh CreateMesh(float width, float height)
    {
        var m = new Mesh
        {
            name = "ScriptedMesh",
            vertices = new[]
            {
                new Vector3(-width, -height, 0.01f),
                new Vector3(width, -height, 0.01f),
                new Vector3(width, height, 0.01f),
                new Vector3(-width, height, 0.01f)
            },
            uv = new[]
            {
                new Vector2(0, 0),
                new Vector2(0, 1),
                new Vector2(1, 1),
                new Vector2(1, 0)
            },
            triangles = new[] {0, 1, 2, 0, 2, 3}
        };
        m.RecalculateNormals();
        return m;
    }

    void Awake()
    {
        GameObject plane = new GameObject("Plane");
        MeshFilter meshFilter = (MeshFilter) plane.AddComponent(typeof (MeshFilter));
        meshFilter.mesh = CreateMesh(MyWidth, MyHeight);
        MeshRenderer renderer = plane.AddComponent(typeof (MeshRenderer)) as MeshRenderer;
        renderer.material.shader = Shader.Find("Particles/Additive");
        Texture2D tex = new Texture2D(1, 1);
        tex.SetPixel(0, 0, Color.green);
        tex.Apply();
        renderer.material.mainTexture = tex;
        renderer.material.color = Color.green;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
