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
        BuildBoundingBox();
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

    private void BuildBoundingBox()
    {
        //if (ObjectModelManager.SelectVertexMode) Gizmos.DrawWireCube(Vector3.zero, Vector3.one);
    }

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
        if (isActiveAndEnabled) StartCoroutine(scriptReference.SetBlueprintItem(item, IsBlueprint));
	}

    void CreateMeshPrefab(ModelMesh item)
    {
        var path = string.Format("Assets/Meshes/{0}.prefab", item.MeshId);
        var model = AssetDatabase.LoadAssetAtPath<GameObject>(path);
        if (model == null) return;
        var mesh = Instantiate(model);
        mesh.transform.SetParent(this.transform, true);
        if (item.IsSelected) AddVertexModels(mesh, item.Scale.x);
		var scriptReference = mesh.GetComponent<MeshObject>();
		if (scriptReference == null) return;
		scriptReference.SetItem(item, !IsBlueprint && item.IsSelected && ObjectModelManager.CanZoomAndPanObject);
    }

    void AddVertexModels(GameObject mesh, float scale)
    {
        var vertexModels = ObjectModelManager.ShowVerticesAsSpheres(mesh.GetComponent<MeshFilter>().sharedMesh.vertices, scale);
        foreach (var vertexModel in vertexModels)
        {
            vertexModel.name = "VertexSphere";
            vertexModel.transform.SetParent(mesh.transform, true);
        }
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
        var cuboids = GetComponentsInChildren<MeshObject>();
        foreach (var item in cuboids)
        {
            Destroy(item.gameObject);
        }
    }

}
