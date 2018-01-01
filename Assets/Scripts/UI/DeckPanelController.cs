using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Extenders;
using Assets.Scripts.Models;
using Assets.Scripts.UI;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class DeckPanelController : BaseCanvasController
{
    public InputField DeckNameText;
	public ObjectModelManager ObjectModelManager;
    public DeckManager DeckManager;
    public BlueprintManager BlueprintManager;
    public bool IsBlueprint;
	public GameObject PrefabBluprint;

	void OnEnable()
	{
		UpdateDeckName ();
	}

	public void UpdateDeckName()
	{
		DeckNameText.text = DeckManager.GetDeckName();
        RebuildPrefabs();
	}

	public void GetDeckName()
	{
        DeckManager.UpdateDeckName (DeckNameText.text);
	}

    private void RebuildPrefabs()
    {
        RebuildBlueprintPrefabs();
		RebuildMeshPrefabs ();
    }

    private void RebuildBlueprintPrefabs()
    {
        var blueprints = BlueprintManager.GetBlueprintList();
        ClearBlueprintPrefabItems();
        foreach (var blueprint in blueprints)
        {
            CreateBlueprintPrefab(blueprint);
        }
    }

    private void RebuildMeshPrefabs()
    {
        var meshObjects = ObjectModelManager.GetModelMeshList();
        ClearMeshPrefabItems();
        foreach (var meshObject in meshObjects)
        {
            CreateMeshPrefab(meshObject);
        }
    }

    //TODO: This is in the wrong place - Should be ModelListController
    //public void AddModel()
    //{
    //    var model = ObjectModelManager.AddModel();
    //    var vertexModels = ObjectModelManager.ShowVerticesAsSpheres(model.GetComponent<MeshFilter>().mesh.vertices);
    //    var rend = model.GetComponent<Renderer>();
    //    rend.material.color = Color.green;
    //    model.transform.SetParent(this.transform, true);
    //    foreach (var vertexModel in vertexModels)
    //    {
    //        vertexModel.transform.SetParent(model.transform, true);
    //    }
    //}

    /// <summary>
    /// Create the Blueprint Item on a plane from the jpg image file
    /// </summary>
    /// <param name="item"></param>
    void CreateBlueprintPrefab(Blueprint item)
	{
		var plane = Instantiate(PrefabBluprint);
		plane.transform.SetParent(this.transform, true);
		var scriptReference = plane.GetComponent<BlueprintPlane>();
		if (scriptReference == null) return;
		StartCoroutine(scriptReference.SetBlueprintItem(item, IsBlueprint));
	}

    void CreateMeshPrefab(ModelMesh item)
    {
		var empty = new GameObject ();
		empty.transform.SetParent (this.transform, true);
        var path = string.Format("Assets/Meshes/{0}.prefab", item.MeshId);
		//var guids = AssetDatabase.FindAssets (item.MeshId);
		//var assetPath = AssetDatabase.GUIDToAssetPath (guids [0]);
		//var thing = AssetDatabase.LoadAssetAtPath (assetPath, typeof(Mesh));
        var model = AssetDatabase.LoadAssetAtPath<GameObject>(path);
        var mesh = Instantiate(model);
        mesh.transform.SetParent(this.transform, true);
		var vertexModels = ObjectModelManager.ShowVerticesAsSpheres(model.GetComponent<MeshFilter>().sharedMesh.vertices);
		var rend = model.GetComponent<Renderer>();
		rend.sharedMaterial.color = Color.green;
		model.transform.SetParent(empty.transform, true);
		foreach (var vertexModel in vertexModels)
		{
			vertexModel.transform.SetParent(empty.transform, true);
		}
		var scriptReference = mesh.GetComponent<Cylinder>();
		if (scriptReference == null) return;
		scriptReference.SetItem(item, !IsBlueprint);
    }

	void ClearBlueprintPrefabItems()
	{
		var items = GetComponentsInChildren<BlueprintPlane>();
		foreach (var item in items)
		{
			Destroy(item.gameObject);
		}
	}

    void ClearMeshPrefabItems()
    {
        var cuboids = GetComponentsInChildren<Cuboid>();
        foreach (var item in cuboids)
        {
            Destroy(item.gameObject);
        }

        var cylinders = GetComponentsInChildren<Cylinder>();
        foreach (var item in cylinders)
        {
            Destroy(item.gameObject);
        }
    }

}
