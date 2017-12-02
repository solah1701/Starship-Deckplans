using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Assets.Scripts.Extenders;
using UnityEngine;
using Assets.Scripts.Models;
using UnityEngine.Events;

public class ShipManager : MonoBehaviour {

    public FileSystemCanvasController LoadShipController;
    public FileSystemCanvasController SaveShipController;
	public ModalDialogCanvasController DialogBox;
    public ScreenManager ScreenManager;

    private Ship _ship = new Ship();
    private Deck _currentDeck;
    private bool _isSaving;

    public int DeckCount { get { return _ship.Decks.Count; } }

	void Start()
	{
		Init ();
	}

	public void Init()
	{
		PopulateShip (LoadShipController.GetStartingFile ());
	}

    public void Load()
    {
        BindFileController(LoadShipController, LoadShipAction);
        Debug.Log("Load screen");
    }

    public void Save()
    {
        var startingFile = SaveShipController.GetStartingFile();
        var fileInfo = new FileInfo(startingFile);
        SaveShipController.FileText.text = fileInfo.Name;

        BindFileController(SaveShipController, SaveShipAction);
        Debug.Log("Save screen");
    }

	public void New()
	{
		DialogBox.Choice("Creating a new ship will lose any unsaved changes. Do you wish to Save?", DoYesAction, DoNoAction, DoCancelAction);
	}

    void LoadShipAction()
    {
        var filepath = FileHelper.GetFilePath(LoadShipController.PathText.text, LoadShipController.FileText.text);
        PopulateShip(filepath);
		ScreenManager.OpenPanel (LoadShipController.FileSystemAnimator);
		ScreenManager.OpenPanel (ScreenManager.initiallyOpen);
        //LoadShipController.ShowGameObject(false);
        Debug.Log("Load Ship");
    }

    void PopulateShip(string filepath)
    {
        _ship = FileHelper.Load<Ship>(filepath);
    }

    void SaveShipAction()
    {
        var filepath = FileHelper.GetFilePath(SaveShipController.PathText.text, SaveShipController.FileText.text);
        FileHelper.Save(filepath, _ship);
        if (_isSaving) DoNoAction();
        Debug.Log("Save Ship");
    }

    void DoYesAction()
    {
        BindFileController(SaveShipController, SaveShipAction);
        _isSaving = true;
    }

    void DoNoAction()
    {
        _ship = new Ship();
    }

	void DoCancelAction()
	{
		//Debug.Log("Cancel");
	}

    protected void BindFileController(FileSystemCanvasController fileController, UnityAction action)
    {
        fileController.OnFileAction(action);
        fileController.OnCancelAction(CancelFileAction);
        ScreenManager.OpenPanel(fileController.FileSystemAnimator);
    }

    protected virtual void CancelFileAction()
    {
		ScreenManager.OpenPanel (ScreenManager.initiallyOpen);
    }

	public void UpdateShipName(string value)
	{
		if(_ship == null) return;
		_ship.ShipName = value;
	}

	public string GetShipName()
	{
		return _ship == null ? "" : _ship.ShipName;
	}

    public ObjectItemList GetDecks()
	{
		return _ship == null ? new ObjectItemList() : _ship.Decks.Select(deck => new ButtonItem { Item = new ConfigClass.KeyValue { Key = deck.DeckName, Value = deck.DeckName }}).Cast<ObjectItem>().ConvertList();
	}

    public ObjectItemList RemoveDeck(string value)
    {
        _ship.Decks.Remove(GetDeck(value));
        _currentDeck = null;
        return GetDecks();
    }
    
    public void AddDeck(int i)
    {
		while (true) {
			var deckName = string.Format ("Deck {0}", i++);
			if (_ship.Decks.Exists (d => d.DeckName == deckName))
				continue;
			_ship.Decks.Add (new Deck { DeckName = deckName });
			return;
		}
    }

    public Deck GetDeck(string value)
    {
        _currentDeck = _ship.Decks.Find(deck => deck.DeckName == value);
        return _currentDeck;
    }

	public void SelectDeck(string value)
	{

	}
}
