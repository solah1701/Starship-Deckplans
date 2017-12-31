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
    private Deck _currentDeck;
    private Ship _ship;

    public UnityEvent OnDeckChanged;

    void Start()
    {
        _shipManager = GetComponent<ShipManager>();
        _ship = _shipManager.GetShip();
        if (OnDeckChanged == null)
            OnDeckChanged = new UnityEvent();
    }

    public int DeckCount
    {
        get { return _ship.Decks.Count; }
    }

    public void BindFileController(FileSystemCanvasController fileController, UnityAction action)
    {
        _shipManager.BindFileController(fileController, action);
    }

    public Deck GetCurrentDeck()
    {
        return _currentDeck;
    }

    public ObjectItemList GetDecks()
    {
        return _ship == null
            ? new ObjectItemList()
            : _ship.Decks.Select(
                deck => new ButtonItem { Item = new ConfigClass.KeyValue { Key = deck.DeckName, Value = deck.DeckName } })
                .Cast<ObjectItem>()
                .ConvertList();
    }

    public ObjectItemList RemoveDeck()
    {
        var index = _ship.Decks.FindIndex(deck => deck.DeckName == _currentDeck.DeckName);
        _ship.Decks.Remove(_currentDeck);
        if (_ship.Decks.Count <= index)
            _currentDeck = _ship.Decks.Last();
        else
            _currentDeck = _ship.Decks[index];
        OnDeckChanged.Invoke();
        return GetDecks();
    }

    public void AddDeck(int i)
    {
        while (true)
        {
            var deckName = string.Format("Deck {0}", i++);
            if (_ship.Decks.Exists(d => d.DeckName == deckName))
                continue;
            _ship.Decks.Add(new Deck { DeckName = deckName });
            GetDeck(deckName);
            OnDeckChanged.Invoke();
            return;
        }
    }

    public Deck GetDeck(string value)
    {
        _currentDeck = _ship.Decks.Find(deck => deck.DeckName == value);
        return _currentDeck;
    }

    public void SelectDeck(string value, Animator panel)
    {
        GetDeck(value);
        ScreenManager.OpenPanel(panel);
        OnDeckChanged.Invoke();
    }

    public string GetDeckName()
    {
        if (_currentDeck == null)
            return "";
        return _currentDeck.DeckName;
    }

    public void UpdateDeckName(string value)
    {
        if (_currentDeck == null || value == "")
            return;
        _currentDeck.DeckName = value;
        OnDeckChanged.Invoke();
    }
}
