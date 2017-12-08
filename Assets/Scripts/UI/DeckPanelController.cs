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
	}

	public void GetDeckName()
	{
		ShipManager.UpdateDeckName (DeckNameText.text);
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
