using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VertexSphere : MonoBehaviour {
    public bool IsHighlighted;
    public Color Color = Color.yellow;
    public Color HighlightColor = Color.red;
    public ObjectModelManager ObjectModelManager;
    public int VertexIndex;

    public void SetVertexColor(bool highlight)
    {
        var rend = GetComponent<Renderer>();
        rend.material.color = highlight ? HighlightColor : Color;
        IsHighlighted = highlight;
        if(highlight) ObjectModelManager.AddSelectedVertex(rend.gameObject);
    }
}
