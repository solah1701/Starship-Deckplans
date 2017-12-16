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

    //public FileSystemCanvasController BlueprintFileSystemCanvasController;
    //public BlueprintListController BlueprintListController;

    private Deck _deck;

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


/*    void Start()
    {
        Init();
    }

    public void Init()
    {
        if (BlueprintListController != null) return;
        //BlueprintListController = Instantiate(BlueprintListControllerPrefab);
        Debug.Log("Deck Panel Started");
    }

    public void PopulateDeck(Deck deck)
    {
        _deck = deck;
        DeckName.text = deck.DeckName;
		ShowGameObject(true);
        BlueprintListController.InitItems();
        Debug.Log(string.Format("blueprint count = {0}", _deck.Blueprints.Count));
        //ShowGameObject(true);
    }

    public override ObjectItemList InitButtons()
    {
        return GetBlueprints();
    }

    public override ObjectItemList AddButton()
    {
        AddBlueprint();
        return base.AddButton();
    }
    
    public void AddBlueprint()
    {
        BindFileController(BlueprintFileSystemCanvasController, LoadAction);
        ParentController.ShowGameObject(false);
    }

    void LoadAction()
    {
        var filename = BlueprintFileSystemCanvasController.FileText.text;
        var filepath = BlueprintFileSystemCanvasController.PathText.text;
        var blueprint = new Blueprint {FileName = filename, FilePath = filepath};
        _deck.Blueprints = BlueprintListController.AddItem(blueprint);
        BlueprintFileSystemCanvasController.ShowGameObject(false);
        ShowGameObject(true);
        ParentController.ShowGameObject(true);
    }

    protected override void CancelFileAction()
    {
        BlueprintFileSystemCanvasController.ShowGameObject(false);
        ParentController.ShowGameObject(true);
        base.CancelFileAction();
    }

    ObjectItemList GetBlueprints()
    {
        return _deck == null ? null : _deck.Blueprints.Cast<ObjectItem>().ConvertList();
    }*/
}
