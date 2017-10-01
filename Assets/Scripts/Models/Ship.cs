using System.Collections;
using System.Collections.Generic;

public class Ship  {
    public string ShipName { get; set; }
    public List<Deck> Decks { get; set; }

    public Ship()
    {
        Decks = new List<Deck>();
    } 
}
