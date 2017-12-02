using System.Collections.Generic;
using System.IO;
using System.Linq;
using Assets.Scripts.Extenders;
using Assets.Scripts.Models;
using Assets.Scripts.UI;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ShipCanvasController : BaseCanvasController
{
    public InputField ShipNameText;
    public ModalDialogCanvasController DialogBox;
    public FileSystemCanvasController FileSystemCanvas;
    public FileSystemCanvasController LoadShipController;
    public FileSystemCanvasController SaveShipController;
    public ButtonListController ButtonListController;
    public DeckPanelController DeckController;

	private Ship _ship = new Ship();
    private bool _isSaving;
    private bool _hasChanged;
    private Deck _currentDeck;

    private void Start()
	{
		DeckController.SetParentController (this);
		//var startingFile = LoadShipController.GetStartingFile ();
		DeckController.ShowGameObject (false);
		//PopulateShip (startingFile);
	}

    public void NewShip()
    {
        DialogBox.Choice("Creating a new ship will lose any unsaved changes. Do you wish to Save?", DoYesAction, DoNoAction, DoCancelAction);
    }

    public void OpenFileCanvas()
    {
        FileSystemCanvas.ShowGameObject(true);
    }

    public void LoadShip()
    {
        BindFileController(LoadShipController, LoadShipAction);
        Debug.Log("Load screen");
    }

    public void SaveShip()
    {
        var startingFile = SaveShipController.GetStartingFile();
        var fileInfo = new FileInfo(startingFile);
        SaveShipController.FileText.text = fileInfo.Name;

        BindFileController(SaveShipController, SaveShipAction);
        Debug.Log("Save screen");
    }

    public void SetShipName()
    {
        _ship.ShipName = ShipNameText.text;
    }

    #region "Button List Controller"
    public override ObjectItemList InitButtons()
    {
        return GetDecks();
    }

	public void InitButtons(ObjectItemList list)
	{
	}

	public ObjectItemList GetList()
	{
		return new ObjectItemList ();
	}

    public override ObjectItemList AddButton()
    {
        _ship.Decks.Add(AddDeck(_ship.Decks.Count + 1));
        return GetDecks();
    }

    public override ObjectItemList RemoveButton(string value)
    {
        DeckController.ShowGameObject(false);
        return RemoveDeck(value);
    }

    public override void ButtonListClicked(string value)
    {
        DeckController.Init();
        DeckController.PopulateDeck(GetDeck(value));
    }
    #endregion

    public void UpdateDeckName()
    {
        var index = _ship.Decks.FindIndex(d => d.DeckName == _currentDeck.DeckName);
        _ship.Decks[index].DeckName = DeckController.DeckName.text;
        GetDeck(DeckController.DeckName.text);
        Debug.Log(_currentDeck.DeckName);
        ButtonListController.InitButtons();
    }

    ObjectItemList GetDecks()
    {
        return _ship.Decks.Select(deck => new ButtonItem { Item = new ConfigClass.KeyValue { Key = deck.DeckName, Value = deck.DeckName }}).Cast<ObjectItem>().ConvertList();
    }

    Deck AddDeck(int i)
    {
        while (true)
        {
            var deckName = string.Format("Deck {0}", i++);
            if (_ship.Decks.Exists(d => d.DeckName == deckName)) continue;
            return new Deck {DeckName = deckName};
        }
    }

    ObjectItemList RemoveDeck(string value)
    {
        _ship.Decks.Remove(GetDeck(value));
        _currentDeck = null;
        return GetDecks();
    }

    Deck GetDeck(string value)
    {
        _currentDeck = _ship.Decks.Find(deck => deck.DeckName == value);
        return _currentDeck;
    }

    void DoYesAction()
    {
        BindFileController(SaveShipController, SaveShipAction);
        _isSaving = true;
    }

    void DoNoAction()
    {
        _ship = new Ship();
        ShipNameText.text = "";
        DeckController.ShowGameObject(false);
        ButtonListController.InitButtons();
    }

    void DoCancelAction()
    {
        //Debug.Log("Cancel");
    }

    void LoadShipAction()
    {
        var filepath = FileHelper.GetFilePath(LoadShipController.PathText.text, LoadShipController.FileText.text);
        PopulateShip(filepath);
        LoadShipController.ShowGameObject(false);
        DeckController.ShowGameObject(false);
        ShowGameObject(true);
        Debug.Log("Load Ship");
    }

    void PopulateShip(string filepath)
    {
        _ship = FileHelper.Load<Ship>(filepath);
        ShipNameText.text = _ship.ShipName;
        ButtonListController.InitButtons();
    }

    void SaveShipAction()
    {
        var filepath = FileHelper.GetFilePath(SaveShipController.PathText.text, SaveShipController.FileText.text);
        FileHelper.Save(filepath, _ship);
        if (_isSaving) DoNoAction();
        SaveShipController.ShowGameObject(false);
        ShowGameObject(true);
        Debug.Log("Save Ship");
    }

    protected override void CancelFileAction()
    {
        _isSaving = false;
        LoadShipController.ShowGameObject(false);
        SaveShipController.ShowGameObject(false);
        base.CancelFileAction();
    }
}
