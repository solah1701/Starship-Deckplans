using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Models;

public class Deck
{
    public string DeckName { get; set; }
    public List<Blueprint> Blueprints { get; set; }
    public List<ModelMesh> Meshes { get; set; } 
    public int NextMeshID { get; set; }

    public Deck()
    {
        Blueprints = new List<Blueprint>();
        Meshes = new List<ModelMesh>();
    }

    public string CreateMeshPathName()
    {
        NextMeshID += 1;
        return string.Format("{0}_{1}", DeckName, NextMeshID);
    }
}

