using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Extenders;
using Assets.Scripts.Models;
using Assets.Scripts.UI;
using UnityEngine;
using UnityEngine.UI;

public class DeckPanelController : BaseCanvasController
{
    public InputField DeckNameText;
	public ShipManager ShipManager;

	public GameObject PrefabBluprint;

	void OnEnable()
	{
		UpdateDeckName ();
	}

	public void UpdateDeckName()
	{
		DeckNameText.text = ShipManager.GetDeckName();
		var blueprints = ShipManager.GetBlueprintList ();
		ClearPrefabItems ();
		foreach (var blueprint in blueprints) {
			CreateBlueprintPrefab (blueprint);
		}
	}

	public void GetDeckName()
	{
		ShipManager.UpdateDeckName (DeckNameText.text);
	}

    public void AddModel()
    {
        var model = ShipManager.AddModel();
        var vertexModels = ShipManager.ShowVerticesAsSpheres(model.GetComponent<MeshFilter>().mesh.vertices);
        var rend = model.GetComponent<Renderer>();
        rend.material.color = Color.green;
        model.transform.SetParent(this.transform, true);
        foreach (var vertexModel in vertexModels)
        {
            vertexModel.transform.SetParent(model.transform, true);
        }
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
		StartCoroutine(scriptReference.SetBlueprintItem(item));
	}

	void ClearPrefabItems()
	{
		var items = GetComponentsInChildren<BlueprintPlane>();
		foreach (var item in items)
		{
			Destroy(item.gameObject);
		}
	}

}
