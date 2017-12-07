using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Models;
using UnityEngine;

public class DeckButtonListManager : ButtonListManager {

    public ShipManager MyShipManager;
	public Animator DestinationPanel;

    public override ObjectItemList InitButtons()
    {
        return MyShipManager.GetDecks();
    }

    public override ObjectItemList AddButton()
    {
        MyShipManager.AddDeck(MyShipManager.DeckCount + 1);
        return MyShipManager.GetDecks();
    }

    public override ObjectItemList RemoveButton(string value)
    {
        MyShipManager.RemoveDeck();
        return MyShipManager.GetDecks();
    }

	public override void ButtonListClicked (string value)
	{
		MyShipManager.SelectDeck (value, DestinationPanel);
	}
}
