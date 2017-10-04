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
        BlueprintListController.SetBlueprints(_deck.Blueprints);
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
