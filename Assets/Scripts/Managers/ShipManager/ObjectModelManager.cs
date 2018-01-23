﻿using System.Collections;
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
    public Color VertexColor = Color.yellow;
    public bool SelectVertexMode;
    public bool EditVertexMode;
    public bool LockXMode;
    public bool LockYMode;

    public bool CanZoomAndPanObject { get { return !SelectVertexMode && !EditVertexMode; } }
    public bool CanShowBoundingBox { get { return SelectVertexMode; } }
    public List<GameObject> SelectedVertices { get { return _selectedVertices; } } 

    private DeckManager _deckManager;
    private string _currentMeshType;
    private ModelMesh _activeMesh;
    private List<GameObject> _selectedVertices;

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
        EditVertexMode = false;
        LockXMode = false;
        LockYMode = false;
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
        _selectedVertices = new List<GameObject>();
        if (parentScale <= 0.001) return spheres;
        var threshold = 0.1f;
        var scale = 0.1f/parentScale;
        var vertexIndex = 0;
        foreach (var vertex in vertices)
        {
            if (spheres.Any(sph => (sph.transform.position - vertex).magnitude < threshold)) continue;
            var sphere = CreateSphere(vertexIndex++);
            sphere.transform.position = vertex;
            sphere.transform.localScale = new Vector3(scale, scale, scale);
            sphere.tag = "vertexHelper";
            var rend = sphere.GetComponent<Renderer>();
            rend.material.color = VertexColor;

            spheres.Add(sphere);
        }
        return spheres;
    }

    GameObject CreateSphere(int vertexIndex)
    {
        var sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        var sc = sphere.GetComponent<Collider>();
        sc.enabled = true;
        sphere.AddComponent<VertexSphere>();
        var vs = sphere.GetComponent<VertexSphere>();
        vs.ObjectModelManager = this;
        vs.VertexIndex = vertexIndex;
        return sphere;
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
        if (deck.Meshes.Count <= 0)
        {
            _deckManager.UpdateDeck();
            return GetModelMeshList();
        }
        var nextSelectedMesh = deck.Meshes.Count <= index ? deck.Meshes.Last() : deck.Meshes[index];
        SelectMesh(nextSelectedMesh);
        return GetModelMeshList();
    }

    public void SelectMesh(ModelMesh item)
    {
		if (_activeMesh != null) _activeMesh.IsSelected = false;
        _activeMesh = item;
        _activeMesh.IsSelected = true;
        _deckManager.UpdateDeck();
    }

    public void SelectVertices(bool select)
    {
        SelectVertexMode = select;
        EditVertexMode = false;
        LockXMode = false;
        LockYMode = false;
        _currentMeshType = string.Empty;
        _deckManager.UpdateDeck();
    }

    public void EditVertices(bool select)
    {
        EditVertexMode = select;
        _currentMeshType = string.Empty;
        SelectVertexMode = false;
    }

    public void LockX(bool select)
    {
        _currentMeshType = string.Empty;
        SelectVertexMode = false;
        LockXMode = select;
    }

    public void LockY(bool select)
    {
        _currentMeshType = string.Empty;
        SelectVertexMode = false;
        LockYMode = select;
    }

    public void ResetVertices()
    {
        _selectedVertices = new List<GameObject>();
        _deckManager.UpdateDeck();
    }

    public void AddSelectedVertex(GameObject sphere)
    {
        _selectedVertices.Add(sphere);
    }
}
