using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(DeckManager))]
public class BlueprintManager : MonoBehaviour {

    public ScreenManager ScreenManager;
    public FileSystemCanvasController LoadBlueprintController;

    private DeckManager _deckManager;
    private Deck _currentDeck;

    void Awake()
    {
        _deckManager = GetComponent<DeckManager>();
        _currentDeck = _deckManager.GetCurrentDeck();
    }

    void LoadBlueprintAction()
    {
        var filepath = LoadBlueprintController.PathText.text;
        var item = new Blueprint { FileName = LoadBlueprintController.FileText.text, FilePath = filepath };
        if (!_currentDeck.Blueprints.Contains(item))
            _currentDeck.Blueprints.Add(item);
        ScreenManager.OpenPanel(ScreenManager.previouslyOpen);
    }

    public void BindFileController(FileSystemCanvasController fileController, UnityAction action)
    {
        _deckManager.BindFileController(fileController, action);
    }

    public IEnumerable<Blueprint> GetBlueprintList()
    {
        if (_currentDeck == null)
            return new List<Blueprint>();
        return _currentDeck.Blueprints;
    }

    public IEnumerable<Blueprint> AddBlueprint()
    {
        BindFileController(LoadBlueprintController, LoadBlueprintAction);
        return GetBlueprintList();
    }

    public IEnumerable<Blueprint> RemoveBlueprint(Blueprint item)
    {
        if (_currentDeck.Blueprints.Contains(item))
            _currentDeck.Blueprints.Remove(item);
        return GetBlueprintList();
    }

}
