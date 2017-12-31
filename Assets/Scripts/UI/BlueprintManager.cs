using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(DeckManager))]
public class BlueprintManager : MonoBehaviour {

    public ScreenManager ScreenManager;
    public FileSystemCanvasController LoadBlueprintController;

    private DeckManager _deckManager;

    void Start()
    {
        _deckManager = GetComponent<DeckManager>();
    }

    void LoadBlueprintAction()
    {
        var filepath = LoadBlueprintController.PathText.text;
        var item = new Blueprint { FileName = LoadBlueprintController.FileText.text, FilePath = filepath };
		if (!_deckManager.CurrentDeck.Blueprints.Contains(item))
			_deckManager.CurrentDeck.Blueprints.Add(item);
        ScreenManager.OpenPanel(ScreenManager.previouslyOpen);
    }

    public void BindFileController(FileSystemCanvasController fileController, UnityAction action)
    {
        _deckManager.BindFileController(fileController, action);
    }

    public IEnumerable<Blueprint> GetBlueprintList()
    {
		if (_deckManager.CurrentDeck == null)
            return new List<Blueprint>();
		return _deckManager.CurrentDeck.Blueprints;
    }

    public IEnumerable<Blueprint> AddBlueprint()
    {
        BindFileController(LoadBlueprintController, LoadBlueprintAction);
        return GetBlueprintList();
    }

    public IEnumerable<Blueprint> RemoveBlueprint(Blueprint item)
    {
		if (_deckManager.CurrentDeck.Blueprints.Contains(item))
			_deckManager.CurrentDeck.Blueprints.Remove(item);
        return GetBlueprintList();
    }

}
