using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Models;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(DeckManager))]
public class ObjectModelManager : MonoBehaviour {
    public GameObject CylinderPrefab;
    public GameObject CubePrefab;
	public UnityEvent OnMeshAdded;

    private DeckManager _deckManager;
    private string _currentMeshType;
    private ModelMesh _activeMesh;
    public bool SelectVertexMode;
    void Start()
    {
        _deckManager = GetComponent<DeckManager>();
		if (OnMeshAdded == null)
			OnMeshAdded = new UnityEvent();
    }

    public void SetMeshType(string value)
    {
        _currentMeshType = value;
        SelectVertexMode = false;
    }

    public IEnumerable<ModelMesh> AddModel()
    {
        if (_currentMeshType == null) return null;
		var meshName = _deckManager.CurrentDeck.CreateMeshPathName();
		var path = "Assets/Meshes/" + meshName + ".prefab";
        GameObject mesh;
        ModelMesh newModelMesh = null;
        if (!_deckManager.CurrentDeck.Meshes.Exists(m => m.MeshId == meshName))
        {
            newModelMesh = new ModelMesh {MeshId = meshName, Position = new Vect3(), MeshType = _currentMeshType};
            _deckManager.CurrentDeck.Meshes.Add(newModelMesh);

            if (_currentMeshType == "Cylinder") mesh = AddCylinder();
            else
                mesh = AddCuboid();
            mesh.name = meshName;

            //TODO: Asset creation should be managed when the json file is being saved, otherwise we will be left
            //with a whole bunch of zombie assets which are not bound to anything
            //however there will be a requirement for temporary storage prior to json serialization
			PrefabUtility.CreatePrefab (path, mesh);
        }
        else
        {
            mesh = AssetDatabase.LoadAssetAtPath<GameObject>(path);
        }
        if (newModelMesh != null) SelectMesh(newModelMesh);
        OnMeshAdded.Invoke();
        return _deckManager.CurrentDeck.Meshes;
    }

    GameObject AddCylinder()
    {
		var mesh = CylinderPrefab;
        return mesh;
    }

    GameObject AddCuboid()
    {
		var mesh = CubePrefab;
        return mesh;
    }

    public IEnumerable<GameObject> ShowVerticesAsSpheres(Vector3[] vertices, float parentScale)
    {
        var spheres = new List<GameObject>();
        if (parentScale == 0.0) return spheres;
        var threshold = 0.1f;
        var scale = 0.1f/parentScale;
        foreach (var vertex in vertices)
        {
            if (spheres.Any(sph => (sph.transform.position - vertex).magnitude < threshold)) continue;
            var sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            sphere.transform.position = vertex;
            sphere.transform.localScale = new Vector3(scale, scale, scale);
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

    public IEnumerable<ModelMesh> RemoveModel(ModelMesh item)
    {
        if (!_deckManager.CurrentDeck.Meshes.Contains(item)) return GetModelMeshList();
        var deck = _deckManager.CurrentDeck;
        var index = deck.Meshes.FindIndex(mesh => mesh == item);
        deck.Meshes.Remove(item);
        var nextSelectedMesh = deck.Meshes.Count <= index ? deck.Meshes.Last() : deck.Meshes[index];
        SelectMesh(nextSelectedMesh);
        return GetModelMeshList();
    }

    public void SelectMesh(ModelMesh item)
    {
		if (_activeMesh == null)
			_activeMesh = _deckManager.CurrentDeck.Meshes.First (m => m.IsSelected);
		_activeMesh.IsSelected = false;
        _activeMesh = item;
        _activeMesh.IsSelected = true;
        _deckManager.UpdateDeck();
    }

    public void SelectVertices()
    {
        SelectVertexMode = true;
        _currentMeshType = string.Empty;
        _deckManager.UpdateDeck();
    }
}
