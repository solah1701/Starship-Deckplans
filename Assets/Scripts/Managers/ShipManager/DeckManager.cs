using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Extenders;
using Assets.Scripts.Models;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(ShipManager))]
public class DeckManager : MonoBehaviour
{
    public ScreenManager ScreenManager;

    private ShipManager _shipManager;
	public Deck CurrentDeck { get; set; }
    public UnityEvent OnDeckChanged;

    void Start()
    {
        _shipManager = GetComponent<ShipManager>();
        if (OnDeckChanged == null)
            OnDeckChanged = new UnityEvent();
    }

    public int DeckCount
    {
		get { return _shipManager.GetShip().Decks.Count; }
    }

    public void BindFileController(FileSystemCanvasController fileController, UnityAction action)
    {
        _shipManager.BindFileController(fileController, action);
    }

    public ObjectItemList GetDecks()
    {
		if (_shipManager == null)
			return new ObjectItemList();
		return _shipManager.GetShip ().Decks.Select(
                deck => new ButtonItem { Item = new ConfigClass.KeyValue { Key = deck.DeckName, Value = deck.DeckName } })
                .Cast<ObjectItem>()
                .ConvertList();
    }

    public ObjectItemList RemoveDeck()
    {
        var ship = _shipManager.GetShip();
        var index = ship.Decks.FindIndex(deck => deck.DeckName == CurrentDeck.DeckName);
        ship.Decks.Remove(CurrentDeck);
        if (ship.Decks.Count <= index)
            CurrentDeck = ship.Decks.Last();
        else
            CurrentDeck = ship.Decks[index];
        UpdateDeck();
        return GetDecks();
    }

    public void AddDeck(int i)
    {
		var ship = _shipManager.GetShip ();
        while (true)
        {
            var deckName = string.Format("Deck {0}", i++);
            if (ship.Decks.Exists(d => d.DeckName == deckName))
                continue;
            ship.Decks.Add(new Deck { DeckName = deckName });
            GetDeck(deckName);
            UpdateDeck();
            return;
        }
    }

    public Deck GetDeck(string value)
    {
		CurrentDeck = _shipManager.GetShip().Decks.Find(deck => deck.DeckName == value);
        return CurrentDeck;
    }

    public void SelectDeck(string value, Animator panel)
    {
        GetDeck(value);
        ScreenManager.OpenPanel(panel);
        UpdateDeck();
    }

    public string GetDeckName()
    {
        return CurrentDeck == null ? "" : CurrentDeck.DeckName;
    }

    public void UpdateDeckName(string value)
    {
        if (CurrentDeck == null || value == "")
            return;
        CurrentDeck.DeckName = value;
        OnDeckChanged.Invoke();
    }

    public void UpdateDeck()
    {
        OnDeckChanged.Invoke();
    }
}
