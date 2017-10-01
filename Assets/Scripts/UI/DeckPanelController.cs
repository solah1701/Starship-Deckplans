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
        BlueprintFileSystemCanvasController.ShowGameObject(false);
        ShowGameObject(true);
        ParentController.ShowGameObject(true);
    }

    public override void ShowGameObject(bool show)
    {
        base.ShowGameObject(show);
    }

    protected override void CancelFileAction()
    {
        BlueprintFileSystemCanvasController.ShowGameObject(false);
        ParentController.ShowGameObject(true);
        base.CancelFileAction();
    }
}
