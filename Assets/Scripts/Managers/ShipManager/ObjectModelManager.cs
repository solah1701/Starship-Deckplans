using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Models;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(DeckManager))]
public class ObjectModelManager : MonoBehaviour {
    public GameObject CylinderPrefab;
    public GameObject CubePrefab;

    private DeckManager _deckManager;
    private string _currentMeshType;

    void Start()
    {
        _deckManager = GetComponent<DeckManager>();
    }

    public void SetMeshType(string value)
    {
        _currentMeshType = value;
    }

    public GameObject AddModel()
    {
        if (_currentMeshType == null) return null;
		var meshName = _deckManager.CurrentDeck.CreateMeshPathName();
        GameObject mesh;
        if (!_deckManager.CurrentDeck.Meshes.Exists(m => m.MeshId == meshName))
        {
            _deckManager.CurrentDeck.Meshes.Add(new ModelMesh {MeshId = meshName, Position = new Vect3()});
            var path = "Assets/Meshes/" + meshName + ".prefab";

            if (_currentMeshType == "Cylinder") mesh = AddCylinder();
            else
            //if (_currentMeshType == "Cuboid")
                mesh = AddCuboid();
            //gameObject.AddComponent<MeshFilter>();
            //gameObject.AddComponent<MeshRenderer>();
            //GetComponent<MeshRenderer>().material.color = Color.white;
            mesh.name = meshName;

            //TODO: Asset creation should be managed when the json file is being saved, otherwise we will be left
            //with a whole bunch of zombie assets which are not bound to anything
            //however there will be a requirement for temporary storage prior to json serialization
            var asset = mesh;
			PrefabUtility.CreatePrefab (path, mesh);
            //AssetDatabase.CreateAsset(asset, path);
            //AssetDatabase.SaveAssets();
        }
        else
        {
			var path = "Assets/Meshes/" + meshName + ".prefab";
            mesh = AssetDatabase.LoadAssetAtPath<GameObject>(path);
        }
        return mesh;
    }

    GameObject AddCylinder()
    {
        var mesh = Instantiate(CylinderPrefab);
        return mesh;
    }

    GameObject AddCuboid()
    {
        var mesh = Instantiate(CubePrefab);
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

    public IEnumerable<ModelMesh> GetModelMeshList()
    {
        return _deckManager.CurrentDeck == null ? new List<ModelMesh>() : _deckManager.CurrentDeck.Meshes;
    }

    public void RemoveModel()
    {

    }

    public void SelectMesh(string value)
    {

    }
}
