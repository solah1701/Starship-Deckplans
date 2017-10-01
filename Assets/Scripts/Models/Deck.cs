using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Deck
{
    public string DeckName { get; set; }
    public List<Blueprint> Blueprints { get; set; }

    public Deck()
    {
        Blueprints = new List<Blueprint>();
    }
}

