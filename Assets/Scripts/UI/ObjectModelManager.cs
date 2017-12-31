using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(DeckManager))]
public class ObjectModelManager : MonoBehaviour {
    public GameObject CylinderPrefab;
    public GameObject CubePrefab;

    private DeckManager _deckManager;
    private Deck _currentDeck;
    private string _currentMeshType;

    void Awake()
    {
        _deckManager = GetComponent<DeckManager>();
        _currentDeck = _deckManager.GetCurrentDeck();
    }

    public void SetMeshType(string value)
    {
        _currentMeshType = value;
    }

    public GameObject AddModel()
    {
        if (_currentMeshType == null) return null;
        var meshName = _currentDeck.CreateMeshPathName();
        var path = "Assets/Meshes/" + meshName + ".asset";

        GameObject mesh;
        if (_currentMeshType == "Cylinder") mesh = Instantiate(CylinderPrefab);
        else
            //if (_currentMeshType == "Cuboid")
            mesh = Instantiate(CubePrefab);
        //gameObject.AddComponent<MeshFilter>();
        //gameObject.AddComponent<MeshRenderer>();
        //GetComponent<MeshRenderer>().material.color = Color.white;
        mesh.name = meshName;

        //TODO: Asset creation should be managed when the json file is being saved, otherwise we will be left
        //with a whole bunch of zombie assets which are not bound to anything
        var asset = mesh.GetComponent<MeshFilter>().mesh;
        AssetDatabase.CreateAsset(asset, path);
        AssetDatabase.SaveAssets();
        return mesh;
    }

    public List<GameObject> ShowVerticesAsSpheres(Vector3[] vertices)
    {
        var spheres = new List<GameObject>();
        var threshold = 0.1f;
        foreach (var vertex in vertices)
        {
            if (spheres.Any(sph => (sph.transform.position - vertex).magnitude < threshold)) continue;
            var sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            sphere.transform.position = vertex;
            sphere.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
            sphere.tag = "vertexHelper";
            var rend = sphere.GetComponent<Renderer>();
            rend.material.color = Color.red;

            spheres.Add(sphere);
        }
        return spheres;
    }

    public void RemoveModel()
    {

    }

    public void SelectMesh(string value)
    {

    }
}
