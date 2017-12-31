using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Models;
using UnityEngine;

public class DeckButtonListManager : ButtonListManager {

    public DeckManager DeckManager;
	public Animator DestinationPanel;

    public override ObjectItemList InitButtons()
    {
        return DeckManager.GetDecks();
    }

    public override ObjectItemList AddButton()
    {
        DeckManager.AddDeck(DeckManager.DeckCount + 1);
        return DeckManager.GetDecks();
    }

    public override ObjectItemList RemoveButton(string value)
    {
        DeckManager.RemoveDeck();
        return DeckManager.GetDecks();
    }

	public override void ButtonListClicked (string value)
	{
		DeckManager.SelectDeck (value, DestinationPanel);
	}
}
