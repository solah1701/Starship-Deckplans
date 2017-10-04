using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.UI;
using UnityEngine;
using UnityEngine.UI;

public class DeckPanelController : BaseCanvasController
{
    public InputField DeckName;
    public FileSystemCanvasController BlueprintFileSystemCanvasController;
    public BlueprintListController BlueprintListController;

    private Deck _deck;

    void Start()
    {
    }

    public void PopulateDeck(Deck deck)
    {
        _deck = deck;
        DeckName.text = deck.DeckName;
        BlueprintListController.InitItems();
        //TODO: there is an issue here with the blueprint controller
        // not being instansiated at this point for populateion
        // it works visually, however when removing an item, it is
        // not populated on the first pass
        foreach (var blueprint in _deck.Blueprints)
        {
            BlueprintListController.AddItem(blueprint);
        }
        //BlueprintListController.SetBlueprints(_deck.Blueprints);
        ShowGameObject(true);
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
        BlueprintListController.AddItem(blueprint);
        //TODO: we can add a blueprint here, but deleting is a challenge
        _deck.Blueprints = BlueprintListController.GetBlueprints();
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
}
