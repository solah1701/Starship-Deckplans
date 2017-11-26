using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Models;
using UnityEngine;

public class Blueprint : ObjectItem
{
    public string FileName { get; set; }
    public string FilePath { get; set; }
    public Vect3 Position { get; set; }
    //public Vector3 LocalOrigin { get; set; }
    public Vect3 Scale { get; set; }
}

public struct Vect3
{
    public float x { get; set; }
    public float y { get; set; }
    public float z { get; set; }
}
